using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureADWebClient.Cache;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureADWebClient
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Registro de serviço de cache
            services.AddScoped<ITokenCacheFactory, TokenCacheFactory>();

            services.Configure<AzureAdOptions>(Configuration.GetSection("AzureAd"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddAuthentication(
                options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    })
                .AddAzureAd(options =>
                {                    
                    Configuration.Bind("AzureAd", options);
                    options.OnAuthorizationCodeReceived = OnAuthorizationCodeReceived;
                    options.OnAuthenticationFailed = OnAuthenticationFailed;
                })
                .AddCookie();

           

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        protected async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            //We need to also specify the redirect URL used
            string currentUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path);
            //Credentials for app itself
            var credential = new ClientCredential(context.Options.ClientId, context.Options.ClientSecret);

            //Construct token cache
            ITokenCacheFactory cacheFactory = context.HttpContext.RequestServices.GetRequiredService<ITokenCacheFactory>();
            TokenCache cache = cacheFactory.CreateForUser(context.Principal);
            var authContext = new AuthenticationContext(context.Options.Authority, cache);

            AuthenticationResult authResult = await authContext.AcquireTokenByAuthorizationCodeAsync(
                            context.ProtocolMessage.Code, new Uri(currentUri), credential, context.Options.Resource);


            // Notify the OIDC middleware that we already took care of code redemption.
            context.HandleCodeRedemption(authResult.AccessToken, context.ProtocolMessage.IdToken);
        }

        protected async Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            //context.HandleResponse();
            //context.Response.Redirect("/Home/Error?message=" + context.Exception.Message);
            //return Task.FromResult(0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

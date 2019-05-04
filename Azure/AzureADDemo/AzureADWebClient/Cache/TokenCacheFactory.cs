using AzureADWebClient.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AzureADWebClient.Cache
{
    /// <summary>
    /// https://github.com/juunas11/aspnetcore2aadauth
    /// </summary>
    public class TokenCacheFactory : ITokenCacheFactory

    {

        private readonly IDistributedCache _distributedCache;

        private readonly IDataProtectionProvider _dataProtectionProvider;

        //Token cache is cached in-memory in this instance to avoid loading data multiple times during the request

        //For this reason this factory should always be registered as Scoped

        private TokenCache _cachedTokenCache;

        private string _cachedTokenCacheUserId;



        public TokenCacheFactory(IDistributedCache distributedCache, IDataProtectionProvider dataProtectionProvider)

        {

            _distributedCache = distributedCache;

            _dataProtectionProvider = dataProtectionProvider;

        }



        public TokenCache CreateForUser(ClaimsPrincipal user)

        {

            string userId = user.GetObjectId();



            if (_cachedTokenCache != null)

            {

                // Guard for accidental re-use across requests

                if (userId != _cachedTokenCacheUserId)

                {

                    throw new Exception("The cached token cache is for a different user! Make sure the token cache factory is registered as Scoped!");

                }



                return _cachedTokenCache;

            }



            _cachedTokenCache = new AdalDistributedTokenCache(

                _distributedCache, _dataProtectionProvider, userId);

            _cachedTokenCacheUserId = userId;

            return _cachedTokenCache;

        }

    }
}

#Demonstração - Web APP com Azure AD

1 - Passos.
- Criar o web app usando o template .net core com autenticação azure ad (work or school accounts)
- Para restrição de recurso por grupo de segurança:
   - No manifesto do app alterre a tag "groupMembershipClaims" para "SecurityGroup"
   - Crie um grupo de segurança para o app e copie o id gerado para ele.
   - Crie classes para uso conforme exemplo AuthorizeVendas.cs e VendasAuthorizationPolicy.cs
   - Em ConfigureServices adicione a politica conforme exemplo 
   - No controler ou action a ter acesso restrito para o grupo adicione o atributo de restrição ex: [AuthorizeVendas]

- Para restrição por roles
 - Edite o manifesto do app e crie as roles do app na tag appRoles
 - Em Enterprise Application adicione usuarios e respectivas roles
 - No controler ou action a ter acesso restrito para roles adicione o atributo de restrição ex:   [Authorize(Roles = "Consulta.Clientes")]
    -> Opcionamente pode criar Classes de politica como no exemplo  

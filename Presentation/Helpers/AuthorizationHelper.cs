using Presentation.Attributes;
using System.Web;
namespace Presentation.Helpers
{
    public class AuthorizationHelper
    {
        private readonly IHttpContextAccessor contextAccesor;
        private readonly JwtAuthenticationManager jwt;

        public AuthorizationHelper(IHttpContextAccessor contextAccesor,JwtAuthenticationManager jwMng) {
            this.jwt = jwMng;
            this.contextAccesor = contextAccesor;
        }

        public string GetJwtTokenUser() {

            var bearer =  contextAccesor?.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ","");

            return jwt.getUserName(bearer);
            
        }
    }
}

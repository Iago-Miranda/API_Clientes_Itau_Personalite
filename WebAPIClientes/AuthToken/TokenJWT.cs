using System;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPIClientes.AuthToken
{
    public class TokenJwt
    {
        private readonly JwtSecurityToken token;

        internal TokenJwt(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidoAte => token.ValidTo;

        public string valor => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}

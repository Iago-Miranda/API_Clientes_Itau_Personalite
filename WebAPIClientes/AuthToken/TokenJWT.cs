using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

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

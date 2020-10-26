using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PraksaWebAPI.Helpers
{
    public class JwtHelper
    {
        private static readonly JwtHelper singleton;
        private IConfiguration configuration;
        
        static JwtHelper()
        {
            singleton = new JwtHelper();
          
        }

        public static JwtHelper Singletion
        {
            get { return singleton; }
        }

        public object SingingCredentials { get; private set; }

        public void SetConfig(IConfiguration configuration)
        {
            singleton.configuration = configuration;
        }

      

        internal object BuildToken(Employee user, string roleName)
        {
            Claim[] claims = new[]
            {
                
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Trim()),
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString() ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, roleName.Trim())

            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

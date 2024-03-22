using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EEC_ICT.Api.Services
{
    public class JwtServices
    {
        private readonly string _secret;

        public JwtServices()
        {
            _secret = ConfigurationManager.AppSettings["secretKey"];
        }

        public string GenerateSecurityToken(string id, string roles ,string permissions)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sid, id),
                    new Claim(JwtRegisteredClaimNames.Jti, roles),
                    new Claim(JwtRegisteredClaimNames.Prn, permissions)
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateSecurityTokenFixLength(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sid, id)                 
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public bool ValidateToken(string authToken)
        {
            var secretKeyRefresh = ConfigurationManager.AppSettings["secretKeyRefresh"];
            var accessToken = authToken.Remove(authToken.Length - secretKeyRefresh.Length);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret)),
                ClockSkew = TimeSpan.Zero,
            };
            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(accessToken, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                return false;
            }
            return validatedToken != null;
        }
        public bool CheckToken(string bearerToken)
        {
            //var bearerToken = Request.Headers.GetValues("Authorization").First();
            var token = bearerToken.Split(' ')[1];
            if (token == null || !ValidateToken(token))
            {
                return false;
            }
            return true;
        }

        public UserClaim GetInfoClaimToken(string authHeader)
        {
            var handler = new JwtSecurityTokenHandler();
            //string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var secretKeyRefresh = ConfigurationManager.AppSettings["secretKeyRefresh"];
            var accessToken = authHeader.Remove(authHeader.Length - secretKeyRefresh.Length);

            var jsonToken = handler.ReadToken(accessToken);
            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;

            var id = tokenS.Claims.First(claim => claim.Type == "sid").Value;

            var roles = RoleServices.SelectByUserId(id);
            var functions = FunctionServices.SelectAllActivated();
            List<Function> functionGen = functions.Where(o => String.IsNullOrEmpty(o.ParentId)).OrderBy(x => x.SortOrder).ToList();
            for (var i = 0; i < functionGen.Count; i++)
            {
                functionGen[i].Children.AddRange(functions.Where(o => o.ParentId == functionGen[i].Id).OrderBy(x => x.SortOrder).ToList());
            }
            var rolesToString = "";
            var permissionsToString = "";
            foreach (var role in roles)
            {
                rolesToString = rolesToString + role.Name + "_";
            }
            List<Permission> permissions = new List<Permission>();

            if (roles.FindIndex(x => x.Name == "Admin") == -1)
            {
                permissions = PermissionServices.SelectByUserId(id);
                foreach (var permission in permissions)
                {
                    permissionsToString = permissionsToString + permission.FunctionId + "_" + permission.CommandId + ",";
                }
            }

  
            return new UserClaim()
            {
                Id = id,
                Roles = rolesToString,
                Permissions = permissionsToString
            };
        }
    }
}
using System.Globalization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using MiniShop.Application.Common.Models;

namespace MiniShop.Application.Common.Helpers
{
   public static class Generator
    {


        public static async Task<string> GenerateTokenAsync(AppSettings appSettings, string id,
          string username, string fullName, IEnumerable<string> claims,
           IEnumerable<string> roles, string rank)
        {
            return await Task.Run(() =>
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
                var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256);
                var tokenClaims = new List<Claim>()
                {
                    new Claim("Identifier", id),
                    new Claim("UserName", username),
                    new Claim("FullName", fullName),
                    new Claim("Rank", rank)
                };


                //Coding Claims
                var codedClaims = "";
                if (claims.Any())
                {
                    codedClaims = JsonConvert.SerializeObject(claims).Replace("\"", "'").EncodeToBase64();
                }

                tokenClaims.Add(new Claim("Claims", codedClaims));


                //Coding Roles
                var codedRoles = "";
                if (roles.Any())
                    codedRoles = JsonConvert.SerializeObject(roles).Replace("\"", "'").EncodeToBase64();
                tokenClaims.Add(new Claim("Roles", codedRoles));


                var tokenOptions = new JwtSecurityToken(
                    claims: tokenClaims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: credentials
                );


                var token = tokenHandler.WriteToken(tokenOptions);

                return token;

                //  return "aaaa";

            });
        }

    }
}

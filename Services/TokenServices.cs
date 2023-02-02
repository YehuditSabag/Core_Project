
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace user.Services{
public  class TokenService 
    {
        private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dsDdS43qsKdgfknjhjke2NCq8YtrE423YTRFDF4dZ"));
        private static string issuer = "https://user-demo.com";
        public static SecurityToken GetToken(List<Claim> claims) =>
            new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

        public static TokenValidationParameters GetTokenValidationParameters() =>
            new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dsDdS43qsKdgfknjhjke2NCq8YtrE423YTRFDF4dZ")),
                ClockSkew = TimeSpan.Zero 
            };

        public static string WriteToken(SecurityToken token) =>
            new JwtSecurityTokenHandler().WriteToken(token);
    
// פונקציה שמפענחת ID מתוקן
                   
      
        public static int decode(String st)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(st) as JwtSecurityToken;
            var id = decodedValue.Claims.First(claim => claim.Type == "Id").Value;
           int ID=int.Parse(id);
            return ID;
          
        }
            
    }}
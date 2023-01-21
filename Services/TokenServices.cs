
namespace user.services;
public static class TaskTokenService
    {
        // private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));
        // private static string issuer = "https://task-list.com";
        // public static SecurityToken GetToken(List<Claim> claims) =>
        //     new JwtSecurityToken(
        //         issuer,
        //         issuer,
        //         claims,
        //         expires: DateTime.Now.AddDays(30.0),
        //         signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        //     );

        // public static TokenValidationParameters GetTokenValidationParameters() =>
        //     new TokenValidationParameters
        //     {
        //         ValidIssuer = issuer,
        //         ValidAudience = issuer,
        //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ")),
        //         ClockSkew = TimeSpan.Zero // remove delay of token when expire
        //     };

        // public static string WriteToken(SecurityToken token) =>
        //     new JwtSecurityTokenHandler().WriteToken(token);
    }

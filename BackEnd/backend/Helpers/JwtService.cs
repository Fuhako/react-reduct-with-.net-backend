using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace backend.Helpers
{
    public class JwtService
    {
        private readonly string secureKey = "thisisaverysecurekey123456789012"; // 256-bit key (32 bytes) for AES
        private readonly AesEncryptionHelper aesHelper = new AesEncryptionHelper();

        public string Generate(int id)
        {
            // Create JWT token
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            // Encrypt payload
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.UtcNow.AddDays(1)); // 1 day
            var jwtPayload = new JwtSecurityToken(header, payload);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtPayload);

            // Encrypt JWT token
            return aesHelper.Encrypt(token);
        }

        public JwtSecurityToken Verify(string encryptedJwt)
        {
            // Decrypt JWT token
            var decryptedToken = aesHelper.Decrypt(encryptedJwt);

            // Validate and read JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secureKey);
            var token = tokenHandler.ValidateToken(decryptedToken, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}

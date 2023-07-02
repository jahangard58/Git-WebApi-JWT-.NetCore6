using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecendWebAppi.Models
{
    public class TokenUser
    {
        //////private  readonly IConfiguration configuration;

        ////public TokenUser(IConfiguration _configuration)
        ////{
        ////    this.configuration = _configuration;
            
        ////}

        public static string GetToken(User user)
        {
            #region Get Config Data From appsetting
            using IHost host = Host.CreateDefaultBuilder().Build();
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>(); 
            #endregion

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.uid.ToString()),
                new Claim(ClaimTypes.Name,user.FullName.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim("HarchiSample","HarchiSample") // yek Claim delkhah Ghara Midim
            };

            //var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CBC72347-A9D1-4083-88CE-2FB59CA4EEC4"));
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JsonWebTokenConfigTokenUser:Key"]));
            var SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //Mohkam kari Token v Amnyat Bishtar
            #region Strong Token By Encript Key 
            /*var EncriptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1ali256Zop102fh4"));*/ // >=16 karkter == Bishtar as 16 Charecter;
            var EncriptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JsonWebTokenConfigTokenUser:EncriptionKey"]));
            var encriptionCredentials = new EncryptingCredentials(EncriptionKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            #endregion

            var Descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = "JwtClientGet",
                Issuer = "JwtServerSend",
                IssuedAt = DateTime.Now, // kiy Token Tolid beshe migim Alan
                Expires = DateTime.Now.AddHours(10), // kiy Token Monghazi beshe migim baed az 10 saet
                NotBefore = DateTime.Now.AddSeconds(5), // Kiy In Token Active Beshe baed az AddSeconds(5) seconds
                SigningCredentials = SigningCredentials, // emza Srever
                EncryptingCredentials = encriptionCredentials,
                CompressionAlgorithm = CompressionAlgorithms.Deflate
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var SecurityToken = TokenHandler.CreateToken(Descriptor);
            return TokenHandler.WriteToken(SecurityToken);

        }
    }
}

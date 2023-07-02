using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecendWebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public TokenController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

       

        // POST api/<TokenController>
        //Test Url https://localhost:7033/api/Token/Post1
        [HttpPost]
        public IActionResult Post1()
        {
             //Step5 filde hay User ke khily mohem nistan ro vard mikonim
            var claims = new List<Claim>
            {
                new Claim("FirstName","Ali"),
                new Claim("LastName","Jahangard")
            };

        //Step6
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JsonWebTokenConfig:Key"]));
        var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var Token = new JwtSecurityToken(
            issuer: configuration["JsonWebTokenConfig:Issuer"],
            audience: configuration["JsonWebTokenConfig:Audience"],
            expires: DateTime.Now.AddMinutes(int.Parse(configuration["JsonWebTokenConfig:ExpireTime"])),
            notBefore: DateTime.Now, //Az Kei Motabar beshe  --- Az Alan Ta 30 Minutes dige  
            claims: claims,
            signingCredentials: Credentials);
        var JsonWebToken = new JwtSecurityTokenHandler().WriteToken(Token); 

            return Ok(JsonWebToken);

            /*My Token
             * eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJGaXJzdE5hbWUiOiJBbGkiLCJMYXN0TmFtZSI6IkphaGFuZ2FyZCIsIm5iZiI6MTY2ODAxNjA0MywiZXhwIjoxNjY4MDE3ODQzLCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.bK1skavJX7kAbKQD8JkTVoTEWXhO1GWtPDYLelR99Xg
             */

        }

        // PUT api/<TokenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TokenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

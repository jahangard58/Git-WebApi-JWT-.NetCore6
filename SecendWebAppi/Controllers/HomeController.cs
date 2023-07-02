using Microsoft.AspNetCore.Mvc;

using SecendWebAppi.DataBaseContextModel;
using SecendWebAppi.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace SecendWebAppi.Controllers
{
    [ApiController]
    /*[Route("api/v{version:apiVersion}/[controller]")] */// set version in Url  . ست میکنیم url ورژن را در 
    [Route("api/[controller]")]
    //[ApiVersion("1")]

    //Step7       Use JWT
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly dbContextEF _dbContextEF;

        private readonly IConfiguration configuration;
        public HomeController(dbContextEF dbContextEF)
        {
            _dbContextEF = dbContextEF;
        }
      
        /// <summary>
        /// نمایش همه سطرها
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMessageAll")]
        [ApiExplorerSettings(IgnoreApi = false)]
        [SwaggerOperation(Summary = "آپلود فایل", Description = "فایل را در قالب Request ارسال کنید", Tags = new string[] { "آپلود فایل" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMessageAll()
        {
            var lst = _dbContextEF.Messages.ToList();
            return Ok(lst);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ایدی</param>
        /// <returns></returns>
        [HttpGet("GetMessageFind")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMessage(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var q = _dbContextEF.Messages.FirstOrDefault(x => x.Id == id);
            if (q != null)
            {
                return Ok(q);
            }
            else
            {
                return NotFound();
            }



        }


        /// <summary>
        /// ثبت 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        // POST: api/SaveMessage
        [HttpPost("SaveMessage")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Message>> SaveMessage(Message message)
        {
            var m = new Message
            {
                Text = message.Text,
            };
            _dbContextEF.Messages.Add(m);
            await _dbContextEF.SaveChangesAsync();

            return Ok(m);

            //var url = Url.Action(nameof(GetMessageAll), "Message", new { id = m.Id }, Request.Scheme);
            //return Created(url, m);


            //return CreatedAtAction(
            //    nameof(GetTodoItem),
            //    new { id = todoItem.Id },
            //    ItemToDTO(todoItem));



        }





        //Create Token JWT  _ Json Web Token Base 

        //Step1 Copy under Text in Appsettings.json
        /*
         * "JsonWebTokenConfig": {
    "Key": "{CBC72347-A9D1-4083-88CE-2FB59CA4EEC4}",
    "ExpireTime":"30"
    "Audience": "localhost",
    "Issuer": "localhost"
    
  }
         */

        // Step2 Install Pakeag=> Install-Package Microsoft.AspNetCore.Authentication.JwtBearer

        //Step3  Install Pakeag=> Install-Package  Microsoft.AspNetCore.Authentication.OpenIdConnect

        //Step4 private readonly IConfiguration configuration; Configuration Moeghyer private dat balla Tarif mikonim bary residan be appSetting

        [HttpPost("CreateToken")]
        public IActionResult CreateTokenJWT()
        {
            //Step5 filde hay User ke khily mohem nistan ro vard mikonim
            var claims = new List<Claim>
            {
                new Claim("FirstName","Ali"),
                new Claim("LastName","Jahangard")
            };

            //Step6
            var Key =new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JsonWebTokenConfig:Key"]));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: configuration["JsonWebTokenConfig:Issuer"],
                audience: configuration["JsonWebTokenConfig:Audience"],
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["JsonWebTokenConfig:ExpireTime"])),
                notBefore: DateTime.Now, //Az Kei Motabar beshe  --- Az Alan Ta 30 Minutes dige  
                claims: claims,
                signingCredentials: Credentials);
            var JsonWebToken = new JwtSecurityTokenHandler().WriteToken(Token); 

            return Ok(JsonWebToken);
        }

    }
}

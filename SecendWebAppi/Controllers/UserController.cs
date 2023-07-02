using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecendWebAppi.DataBaseContextModel;
using SecendWebAppi.Models;
using System.Security.Cryptography;
using System.Text;

namespace SecendWebAppi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
   
    public class UserController : ControllerBase
    {

        private readonly dbContextEF _ContextEF;

        public UserController(dbContextEF contextEF)
        {
            this._ContextEF = contextEF;
        }

        //Hash Text
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        [Authorize(Roles="Admin")] //Check Json Web Token // Ehraz Howyat
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserViewModel userViewModel)
        {
            if (_ContextEF.Users.LongCount() > 0)
            {
                var ExistUser = _ContextEF.Users.FirstOrDefault(f => f.UserName.Contains(userViewModel.UserName.ToString()));
                if (ExistUser != null)
                    return Conflict();
            }


            var userReg = new User
            {
                uid = Guid.NewGuid(),
                UserName = userViewModel.UserName,
                FullName = userViewModel.FullName,
                PasswordHash = ComputeSha256Hash(userViewModel.PasswordHash),
                Role = userViewModel.Role,
                IsActive=true
            };
            _ContextEF.Users.Add(userReg);
            await _ContextEF.SaveChangesAsync();
            return Ok(true);

        }

        [HttpGet("LoginUser")]
        public async Task<IActionResult> LoginUser(string userName,string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }
            var user =await _ContextEF.Users.FirstOrDefaultAsync(f=> f.UserName==userName);
            if (user == null)
                return NotFound();
            if (user.PasswordHash != ComputeSha256Hash(password))
                return Conflict();

            //////return Ok(user.Id);
           
            return Ok(TokenUser.GetToken(user));
        }

        [Authorize(Roles = "Public")] //Check Json Web Token  // Ehraz Howyat
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var category = await _ContextEF.Categories.ToListAsync();
            if (category ==null)
            {
                return NotFound();
            }
            return Ok(category);    
        }

    }
}

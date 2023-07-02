using Microsoft.AspNetCore.Mvc;
using SecendWebAppi.Models;
using SecendWebAppi.Repository;
using SecendWebAppi.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecendWebAppi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[Route("[controller]")]
    
    public class MessageRepoController : ControllerBase
    {
        private readonly MeesagesRepository _MeesagesRepository;


        //Use Repository undelin Code Copy in Programs.cs file Class
        //////Step 5
        //builder.Services.AddScoped<MeesagesRepository, MeesagesRepository>();
        
        public MessageRepoController(MeesagesRepository meesagesRepository)
        {
            this._MeesagesRepository = meesagesRepository;
        }

        // GET: api/<MessageRepoController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result =await _MeesagesRepository.GetAllMessage();
            return Ok(result);
        }

        // GET api/<MessageRepoController>/5
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result =await  _MeesagesRepository.GetMessage(id);
            return Ok(result);
        }

        // POST api/<MessageRepoController>
        [HttpPost("CreateMessage")]
        public async Task<IActionResult> CreateMessage(MessageViewModel message)
        {

            var result =await  _MeesagesRepository.Add(message);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT api/<MessageRepoController>/5
        [HttpPut("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage(Message message)
        {

            var result =await  _MeesagesRepository.Edit(message);
            return Ok(result);
        }

        // DELETE api/<MessageRepoController>/5
        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var Result= await _MeesagesRepository.Delete(id);
            return Ok(Result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SecendWebAppi.Models;
using SecendWebAppi.Repository;
using SecendWebAppi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecendWebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {

        private readonly TruckRepository _truckRepository;
        public TruckController(TruckRepository truckRepository)
        {
            this._truckRepository = truckRepository;

        }

        // GET: api/<TruckController>
        [HttpGet("GetAllTruck")]
        public async Task<IActionResult> GetAllTruck()
        {
            var Result = await _truckRepository.GetAllTruck();
            return Ok(Result);
        }

        // GET api/<TruckController>/5
        [HttpGet("GetTruck")]
        public async Task<IActionResult> GetTruck(int id)
        {
            var Result = await _truckRepository.GetTruck(id);
            return Ok(Result);
        }

        // POST api/<TruckController>
        [HttpPost("AddTruck")]
        public async Task<IActionResult> AddTruck(TruckViewModel truckViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var Resualt = await _truckRepository.Add(truckViewModel);
            return CreatedAtAction(nameof(GetTruck), new { id = Resualt.ID }, Resualt);
        }

        // PUT api/<TruckController>/5
        [HttpPut("EditTruck")]
        public async Task<IActionResult> EditTruck(Truck truck)
        {
            var Result= await _truckRepository.Edit(truck); 
            return Ok(Result);
        }

        // DELETE api/<TruckController>/5
        [HttpDelete("DeleteTruck")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var Result=await _truckRepository.Delete(id);
            return Ok(Result);
        }
    }
}

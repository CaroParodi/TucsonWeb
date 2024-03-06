using Microsoft.AspNetCore.Mvc;
using Tucson.Models.DTO;
using Tucson.Models.Entities;
using Tucson.Services.Services;

namespace Tucson.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }   

        [HttpGet]
        public IActionResult ListReservations()
        {
            try
            {
                
                var result = _reservationService.GetAllReservations();
                return Ok(result.Result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddReservation([FromBody] ReservationDTO reservation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _reservationService.AddReservation(reservation);
                    return Ok(result.Message);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("add-pendingreservation")]
        public IActionResult AddPendingReservation([FromBody] ReservationDTO reservation)
        {
            try
            {
                var result = _reservationService.AddPendingReservation(reservation);
                return Ok(result.Message);
            }
            catch(Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{reservationId}")]
        public IActionResult CancelReservation(int reservationId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _reservationService.CancelReservation(reservationId);
                    return Ok(result.Message);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

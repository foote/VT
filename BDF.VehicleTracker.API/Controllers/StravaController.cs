using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDF.VehicleTracker.BL.Models;
using System.Text;
using System.Net.Http;
using BDF.VehicleTracker.BL;
//using Newtonsoft.Json;

namespace BDF.VehicleTracker.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StravaController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Dictionary<string, string>>> Get([FromQuery(Name = "hub.verify_token")] string hub_token,
                                                                    [FromQuery(Name = "hub.challenge")] string hub_challenge,
                                                                    [FromQuery(Name = "hub.mode")] string hub_mode)
        {   
            try
            {
                var result = new Dictionary<string, string>();
                result.Add("hub.challenge", hub_challenge);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StravaEvent stravaNewEvent)
        {
            try
            {
                return Ok(await StravaManager.Insert(stravaNewEvent));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

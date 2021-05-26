﻿using BDF.VehicleTracker.BL;
using BDF.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BDF.VehicleTracker.API.Controllers
{


    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }


        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Get()
        {
            // Return all the vehicles
            try
            {
                _logger.LogWarning("{UserId} logged in.", "bfoote");
                
                _logger.LogWarning("Index running at: {time}", DateTimeOffset.Now);

                return Ok(await VehicleManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{colorName}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Get(string colorName)
        {
            // Return all the vehicles
            try
            {
                return Ok(await VehicleManager.Load(colorName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Vehicle>> Get(Guid id)
        {
            try
            {
                return Ok(await VehicleManager.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vehicle vehicle)
        {
            try
            {
                return Ok(await VehicleManager.Insert(vehicle));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Vehicle vehicle)
        {
            try
            {
                return Ok(await VehicleManager.Update(vehicle));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await VehicleManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

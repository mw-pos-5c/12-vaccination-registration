using System;

using Microsoft.AspNetCore.Mvc;

using VaccReg.Dtos;
using VaccReg.Services;

using VaccRegDb;

namespace VaccReg.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VaccRegController : ControllerBase
    {
        private VaccRegService service;

        public VaccRegController(VaccRegService service)
        {
            this.service = service;
        }

        [HttpGet("GetTimeSlots")]
        public IActionResult GetTimeSlots(string day)
        {
            if (!DateTime.TryParse(day, out DateTime dayDate))
            {
                return BadRequest("Invalid/Missing date");
            }

            return Ok(service.GetTimeSlots(dayDate));
        }

        [HttpGet("CheckSsn")]
        public IActionResult CheckSsn(long ssn, long pin)
        {
            Registration reg = service.CheckSsn(ssn, pin);

            if (reg == null)
            {
                return Ok(false);
            }

            return Ok(new PatientDto
            {
                Firstname = reg.FirstName,
                Lastname = reg.LastName
            });
        }

        [HttpGet("RegisterTimeSlot")]
        public IActionResult RegisterTimeSlot(long ssn, long pin, string day, int slotId)
        {
            if (!DateTime.TryParse(day, out DateTime slotDate))
            {
                return BadRequest("Invalid/Missing date");
            }
            
            return Ok(service.RegisterTimeSlot(ssn, pin, slotDate, slotId));
        }
    }
}

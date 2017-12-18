using LasmartTest.Data.Entities;
using LasmartTest.Helpers;
using LasmartTest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasmartTest.Controllers.ApiControllers
{   
    [Route("api/equipment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EquipmentController : Controller
    {
        private IEquipmentRepository _equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetEquipmentsAsync()
        {
            var equipmentsFromRepo = await _equipmentRepository.GetEquipmentsAsync();
            return Ok(equipmentsFromRepo);
         }

        [HttpGet("{id}", Name = "GetEquipment")]
        public async Task<IActionResult> GetAuthorAsync(Guid id)
        {
            var equipmentFromRepo = await _equipmentRepository.GetEquipmentAsync(id);

            if (equipmentFromRepo == null)
                return NotFound();
            
            return Ok(equipmentFromRepo);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment(Guid id,
            [FromBody] Equipment equipment)
        {
            if (equipment == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            if (!await (_equipmentRepository.EquipmentExists(id)))
                return NotFound();

            _equipmentRepository.UpdateEquipment(equipment);
            if (!(await _equipmentRepository.SaveAsync()))
                throw new Exception($"Updating equipment {id} failed on save");

            return NoContent();
        }

    }
}

using Domain.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.Identity.Client;
namespace HealWell.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _service;

        public AppointmentsController(AppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _service.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _service.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(int doctorId)
        {
            var appointments = await _service.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(appointments);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(int patientId)
        {
            var appointments = await _service.GetAppointmentsByPatientIdAsync(patientId);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest("Appointment ID mismatch");

            await _service.UpdateAppointmentAsync(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAppointmentAsync(id);
            return NoContent();
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Appointment>>> GetAllAppointments()
        {
            return await _service.GetAllAppointmentsAsync();
        }
    }
}

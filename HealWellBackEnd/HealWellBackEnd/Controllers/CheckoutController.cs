using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Components.Forms;
using Application.Interfaces;
using Domain.Models;
using Application.Services;

namespace HealWellBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _service;

        public CheckoutController(CheckoutService service)
        {
            _service = service;
        }
        [HttpGet("getbyId/{id}")]
        public async Task<ActionResult<PaymentInfo>> GetById(int id)
        {
            return await _service.GetPaymentById(id);
        }
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentInfo model)
        {
           var created =  await _service.ProcessPayment(model);
           return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        

        [HttpPost("MarkAsPaidAndConfirm/{appointmentId}")]
        public async Task<IActionResult> MarkAsPaidAndConfirm(int appointmentId)
        {
            var appointment = await _service.GetAppointmentByIdAsync(appointmentId);

            if (appointment == null)
                return NotFound("Appointment not found.");

            if (appointment.Status == "Confirmed")
                return BadRequest("Appointment is already confirmed.");

            await _service.UpdateAppointmentStatusAsync(appointment, "Paid");

            // Simulate processing
            await Task.Delay(500);

            await _service.UpdateAppointmentStatusAsync(appointment, "Confirmed");

            return Ok(new { message = "Appointment marked as paid and confirmed." });
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PaymentInfo>>> GetAllPayments()
        {
            return Ok(await _service.GetAllPayments());
        }
    }
}

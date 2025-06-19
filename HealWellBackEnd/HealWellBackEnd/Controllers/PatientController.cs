using Domain.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
namespace HealWellBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _service;

        public PatientsController(PatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _service.GetAllAsync();
            return Ok(patients);
        }
        [HttpPost("getId")]
        public async Task<ActionResult<int>> GetPatientId(LoginModel model)
        {
            int id = await _service.GetPatientId(model);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }
        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<Patient>> GetPatientByMail(string email)
        {
            var patient = await _service.GetByEmailAsync(email);
            if (patient == null) return NotFound();
            return Ok(patient);
        }
        [HttpPost("addPatient")]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.AddAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = created.Id }, created);
        }
        [HttpPost("check-login")]
        public async Task<IActionResult> CheckLogin([FromBody] LoginModel request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and password are required.");

            var isValid = await _service.CheckLoginAsync(request.Email, request.Password);
            return isValid ? Ok("Yes") : Unauthorized("Invalid credentials.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.Id) return BadRequest();
            var result = await _service.UpdateAsync(patient);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        [HttpGet("GetPrescriptions/{patientId}")]
        public async Task<ActionResult<List<Prescription>>> GetPatientsPrescription(int patientId)
        {
            return Ok(await _service.GetAllPrescriptionListAsync(patientId));
        }
        [HttpGet("GetHealthRecords/{patientId}")]
        public async Task<ActionResult<List<HealthRecord>>> GetPatientsRecords(int patientId)
        {
            return Ok(await _service.GetAllHealthRecordListAsync(patientId));
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<bool>> ResetPassword(ResetPasswordModel model)
        {
            
            return Ok(await _service.ResetPassword(model.Email, model.OldPassword, model.NewPassword)); 
        }
        [HttpGet("TotalPrescriptions")]
        public async Task<ActionResult<int>> TotalPrescriptions()
        {
            return Ok(await _service.TotalPrescriptions());
        }
        [HttpGet("TotalHealthRecords")]
        public async Task<ActionResult<int>> TotalHealthRecords()
        {
            return Ok(await _service.TotalHealthRecords());
        }
        [HttpGet("AllTestimonials")]
        public async Task<ActionResult<List<Testimonial>>> GetAllTestimonials()
        {
            return Ok(await _service.GetAllTestimonials());
        }
        //public class LoginRequest
        //{
        //    // [Required(ErrorMessage = "Email is required")]
        //    // [EmailAddress(ErrorMessage = "Invalid email format")]
        //    public string Email { get; set; } = string.Empty;

        //    // [Required(ErrorMessage = "Password is required")]
        //    // [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        //    public string Password { get; set; } = string.Empty;

        //    public bool RememberMe { get; set; }
        //}
    }
}

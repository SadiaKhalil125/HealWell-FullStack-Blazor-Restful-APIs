using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;
using Application.Services;
using Microsoft.AspNetCore.OData.Query;

namespace HealWellBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _service;
        //private readonly IDoctorRepository _repository;
      //  private readonly IHostEnvironment _env;
        private readonly IWebHostEnvironment _env;

        public DoctorsController(DoctorService service, IWebHostEnvironment env)
        {

            _service = service;
            _env = env;
            //_repository = repository;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            //var doctors = await _repository.GetAllAsync();
            var doctors = await _service.GetAllAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            var created = await _service.AddAsync(doctor);
            return CreatedAtAction(nameof(GetDoctor), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id) return BadRequest();
            var result = await _service.UpdateAsync(doctor);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        [HttpPost("AddPrescription")]
        public async Task<IActionResult> AddPrescription(Prescription prescription)
        {
            await _service.AddPrescription(prescription);
            return Ok();
        }
        [HttpPost("AddHealthRecord")]
        public async Task<IActionResult> AddHealthRecord(HealthRecord healthrecord)
        {
            await _service.AddHealthRecord(healthrecord);
            return Ok();
        }
    
        [HttpPost("register")]
        public async Task<IActionResult> RegisterDoctor([FromForm] DoctorRegisterDto dto, IFormFile? image)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Specialty = dto.Specialty,
                Email = dto.Email,
                Hospital = dto.Hospital,
                Bio = dto.Bio,
                Experience = dto.Experience,
                Education = dto.Education ?? new(),
                AvailableDays = dto.AvailableDays ?? new(),
                AvailableDateTimes = dto.AvailableDateTimes ?? new(),
                IsActive = dto.IsActive,
                AppointmentCount = 0,
                ReviewCount = 0
            };

            if (image != null)
            {
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                if (!new[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(extension))
                    return BadRequest("Invalid image format.");

                var imageFileName = Guid.NewGuid() + extension;
                var imagesDirectory = Path.Combine(_env.WebRootPath, "images");

                // Check if the directory exists, and create it if not
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                var savePath = Path.Combine(imagesDirectory, imageFileName);

                using var stream = new FileStream(savePath, FileMode.Create);
                await image.CopyToAsync(stream);

                doctor.ImageUrl = imageFileName;
            }

            await _service.AddAsync(doctor);
            return Ok(doctor);
        }
        [HttpPost("edit")]
        public async Task<IActionResult> EditDoctor([FromForm] DoctorRegisterDto dto, IFormFile? image)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Specialty = dto.Specialty,
                Email = dto.Email,
                Hospital = dto.Hospital,
                Bio = dto.Bio,
                Experience = dto.Experience,
                Education = dto.Education ?? new(),
                AvailableDays = dto.AvailableDays ?? new(),
                AvailableDateTimes = dto.AvailableDateTimes ?? new(),
                IsActive = dto.IsActive,
                AppointmentCount = 0,
                ReviewCount = 0
            };
            doctor.Id = await _service.GetDoctorId(dto.Email);
            if (image != null)
            {
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                if (!new[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(extension))
                    return BadRequest("Invalid image format.");

                var imageFileName = Guid.NewGuid() + extension;
                var imagesDirectory = Path.Combine(_env.WebRootPath, "images");

                // Check if the directory exists, and create it if not
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                var savePath = Path.Combine(imagesDirectory, imageFileName);

                using var stream = new FileStream(savePath, FileMode.Create);
                await image.CopyToAsync(stream);

                doctor.ImageUrl = imageFileName;
            }

            await _service.UpdateAsync(doctor);
            return Ok(doctor);
        }

        [HttpPost("GetId")]
        public async Task<ActionResult<int>> GetDoctorIdByEmail(DoctorLoginModel request)
        {
            return Ok(await _service.GetDoctorId(request.Email));
        }
        [HttpPost("check-login")]
        public async Task<IActionResult> CheckLogin([FromBody] DoctorLoginModel request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name))
                return BadRequest("Email and password are required.");

            var isValid = await _service.CheckLoginAsync(request.Name, request.Email);
            return isValid ? Ok("Yes") : Unauthorized("Invalid credentials.");
        }
        [HttpGet("GetCount")]
        public async Task<int> GetTotalNoOfDoctors()
        {
            return await _service.GetTotalNoOfDoctors();  
        }
        [HttpGet("GetAllServices")]
        public async Task<ActionResult<List<Service>>>GetAllServices()
        {
            return await _service.GetAllServices();
        }
        [HttpGet("GetService/{id}")]
        public async Task<Service> GetService(int id)
        {
            return await _service.GetService(id);
        }

    }
}

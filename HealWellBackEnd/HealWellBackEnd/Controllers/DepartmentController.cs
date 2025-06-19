using Domain.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace HealWellBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _service;

        public DepartmentsController(DepartmentService service)
        {
            _service = service;
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name))
                return BadRequest("Department name is required");

            await _service.AddDepartment(department);
            return Ok(department);
        }

        // GET: api/departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _service.GetAll();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _service.GetById(id);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (id != department.Id)
                return BadRequest("ID mismatch");

            var existing = await _service.GetById(id);
            if (existing == null)
                return NotFound();

            existing.Name = department.Name; // update fields if needed
            await _service.UpdateDepartment(existing);

            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var existing = await _service.GetById(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteDepartment(id);
            return NoContent();
        }
    }
}

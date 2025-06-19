using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace HealWellBackEnd.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class ODataDoctorController : ODataController
    {
        private readonly DoctorService _service;
        public ODataDoctorController(DoctorService service)
        {

            _service = service;
        
        }
        [EnableQuery]
        [HttpGet("getodata")]
        public IActionResult Get()
        {
            var doctors = _service.GetAll(); // returns IQueryable<Doctor>
            return Ok(doctors);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealWell.Models
{
    public class DoctorRegisterDto
    {
        //public int Id { get; set; } = 0;
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string? Hospital { get; set; }
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public List<string> Education { get; set; } = new();
        public List<string> AvailableDays { get; set; } = new();
        public List<DateTime> AvailableDateTimes { get; set; } = new();
        public bool IsActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
  
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }

        [MaxLength(100)]
        public string Specialty { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public string ImageUrl { get; set; }

        [Range(0.0, 5.0)]
        public double Rating { get; set; }
        public bool IsActive { get; set; }
        public int AppointmentCount { get; set; } = 0;
        public int ReviewCount { get; set; }

        public string Hospital { get; set; }

        public string Bio { get; set; }
        //[JsonIgnore]
        public List<string> Education { get; set; }

        public string Experience { get; set; }
        //[JsonIgnore]
        public List<DateTime> AvailableDateTimes { get; set; }
        //[JsonIgnore]
        public List<string> AvailableDays { get; set; }




    }


}

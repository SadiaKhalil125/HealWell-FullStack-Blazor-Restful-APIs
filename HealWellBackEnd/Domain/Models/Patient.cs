using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DBO {  get; set; }    
        [Required]
        public string Password { get; set; }

        public string PreviousPrescriptions { get; set; }

        [MaxLength(50)]
        public string LastBloodPressure { get; set; }

        public float Weight { get; set; }

        public float AIC { get; set; }

        public bool ImmunizationsUpToDate { get; set; }

        [JsonIgnore]
        public HealthRecord? HealthRecord { get; set; }
        [JsonIgnore]
        public List<PortalMessage> Messages { get; set; } = new();
        [JsonIgnore]
        public List<Appointment> Appointments { get; set; } = new();
        //public List<PortalMessage> RecentMessages { get; set; } = new();
        //public List<PortalActivity> RecentActivity { get; set; } = new();
    }

}

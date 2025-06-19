using System.Text.Json.Serialization;

namespace HealWell.Models
{
        public class Appointment
        {
            public int Id { get; set; }

            //[Required, MaxLength(100)]
            public string Name { get; set; }

            //[Required, EmailAddress]
            public string Email { get; set; }

            public DateTime Date { get; set; }

            //[Required, MaxLength(100)]
            public string Department { get; set; }

            public int DoctorId { get; set; }

            [JsonIgnore]
            public Doctor? Doctor { get; set; }

            //[Phone]
            public string Phone { get; set; }

            public bool IsTeleHealth { get; set; }
            public string Status { get; set; } = "unpaid"; //paid unpaid confirmed


            //[MaxLength(1000)]
            public string AdditionalMessage { get; set; }

            // Optional: Foreign key (if linking to patient)
            public int PatientId { get; set; }
            [JsonIgnore]
            public Patient? Patient { get; set; }
        }

    

}

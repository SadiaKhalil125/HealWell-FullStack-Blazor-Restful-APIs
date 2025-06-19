using System.Text.Json.Serialization;

namespace HealWell.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public int RefillsRemaining { get; set; }
        public string Prescriber { get; set; } = "";
        public string Status { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}

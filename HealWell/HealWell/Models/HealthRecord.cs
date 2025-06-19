using System.Text.Json.Serialization;

namespace HealWell.Models
{

    public class HealthRecord
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}

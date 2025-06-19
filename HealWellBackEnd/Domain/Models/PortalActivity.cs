namespace Domain.Models
{
    public class PortalActivity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Icon { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }
    }

}

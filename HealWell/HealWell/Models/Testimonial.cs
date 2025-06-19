namespace HealWell.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string ServiceUsed { get; set; }
        public string ImageUrl { get; set; }
    }
}

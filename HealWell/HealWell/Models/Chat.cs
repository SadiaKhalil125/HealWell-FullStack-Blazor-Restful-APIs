namespace HealWell.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialty { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public List<PortalMessage> MessageList { get; set; }

    }
}

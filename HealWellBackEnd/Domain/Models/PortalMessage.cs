using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PortalMessage
    {
        public int Id { get; set; } // Add primary key for EF

        [MaxLength(100)]
        public string SenderName { get; set; }

        [MaxLength(100)]
        public string SenderTitle { get; set; }

        [MaxLength(200)]
        public string Subject { get; set; }

        public string PreviewText { get; set; }

        public DateTime SentTime { get; set; }

        public bool IsUnread { get; set; }

        public bool IsDoctor { get; set; }

        public bool HasAttachment { get; set; }

        // Optional: Foreign key to patient
        public int? PatientId { get; set; }
        public Patient Patient { get; set; }
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}

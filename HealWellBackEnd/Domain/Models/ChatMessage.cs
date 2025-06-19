using Domain.Models;
using System.Reflection.Metadata;

public class ChatMessage
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    //public Patient? Patient { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsDoctor { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

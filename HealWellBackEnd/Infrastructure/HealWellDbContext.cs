using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Infrastructure
{
    public class HealWellDbContext:DbContext
    {
        public HealWellDbContext(DbContextOptions<HealWellDbContext> options) : base(options) { }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PortalActivity> PortalActivities { get; set; }
        public DbSet<PortalMessage> PortalMessages { get; set; }
        public DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}

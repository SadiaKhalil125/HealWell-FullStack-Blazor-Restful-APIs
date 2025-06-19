using Domain.Models;
namespace Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> AddAsync(Patient patient);
        Task<bool> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
        Task<bool> CheckLoginAsync(string email, string password);
        public bool CheckLogin(string email, string password);
        Task<int> GetPatientId(LoginModel model);
        Task<Patient> GetByEmailAsync(string email);
        Task<List<Prescription>> GetAllPrescriptionListAsync(int patientId);
        Task<List<HealthRecord>> GetAllHealthRecordListAsync(int patientId);
        Task <bool> ResetPassword(string email, string oldpassword, string newpassword);
        Task<int> TotalHealthRecords();
        Task<int> TotalPrescriptions();

        Task<List<Testimonial>> GetAllTestimonials();
    }
}

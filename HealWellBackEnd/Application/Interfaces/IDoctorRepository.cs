using Domain.Models;
namespace Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        IEnumerable<Doctor> GetAll();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task<bool> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int id);
        Task AddPrescription(Prescription prescription);
        Task AddHealthRecord(HealthRecord healthrecord);
        Task<int> GetDoctorId(string email);
        Task<bool> CheckLoginAsync(string name, string email);
        Task<int> GetTotalNoOfDoctors();
        Task<List<Service>> GetAllServices();
        Task<Service> GetService(int id);
        Task<List<string>> getEducation(int docId);

        Task<List<DateTime>> getAvailableDates(int docId);

        Task<List<string>> getAvailableDays(int docId);
       
    }
}

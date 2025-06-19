using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
           return await _repo.AddAsync(patient);
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            return await _repo.UpdateAsync(patient);
        }

        public async Task<bool> DeleteAsync(int id)
        {
           return await _repo.DeleteAsync(id);
        }
        public async Task<bool> CheckLoginAsync(string email, string password)
        {
            return await _repo.CheckLoginAsync(email, password);    
        }
        public async Task<int> GetPatientId(LoginModel model)
        {
           return await _repo.GetPatientId(model);  
            
        }
        public async Task<Patient> GetByEmailAsync(string email)
        {
            return await _repo.GetByEmailAsync(email);
        }
        public async Task<List<Prescription>> GetAllPrescriptionListAsync(int patientId)
        {
            return await _repo.GetAllPrescriptionListAsync(patientId);
        }
        public async Task<List<HealthRecord>> GetAllHealthRecordListAsync(int patientId)
        {
            return await _repo.GetAllHealthRecordListAsync(patientId);
        }
        public async Task<bool> ResetPassword(string email, string oldpassword, string newpassword)
        {
            return await _repo.ResetPassword(email, oldpassword, newpassword);
        }
        public bool CheckLogin(string email, string password)
        {
            return _repo.CheckLogin(email, password);
        }
        public async Task<int> TotalHealthRecords()
        {
            return await (_repo.TotalHealthRecords());
        }
        public async Task<int> TotalPrescriptions()
        {
            return await(_repo.TotalPrescriptions());
        }
        public async Task<List<Testimonial>> GetAllTestimonials()
        {
            return await _repo.GetAllTestimonials();
        }
    }
}

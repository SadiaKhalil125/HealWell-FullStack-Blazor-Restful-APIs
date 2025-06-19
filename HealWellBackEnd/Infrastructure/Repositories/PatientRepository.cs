using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HealWellDbContext _context;

        public PatientRepository(HealWellDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.Messages)
                .Include(p => p.Appointments)
                .ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Messages)
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            // patient.HealthRecord = new HealthRecord() { Patient = patient, Date = DateTime.Now, Title = "none", Description = "none" };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            var existing = await _context.Patients.FindAsync(patient.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckLoginAsync(string email, string password)
        {
            return await _context.Patients.AnyAsync(p => p.Email == email && p.Password == password);
        }
        public bool CheckLogin(string email, string password)
        {
            return  _context.Patients.Any(p => p.Email == email && p.Password == password);
        }
        public async Task<int> GetPatientId(LoginModel model)
        {
            Patient patient = await _context.Patients.Where(s => s.Email == model.Email).FirstOrDefaultAsync();
            return patient.Id;
        }
        public async Task<Patient> GetByEmailAsync(string email)
        {
            Patient patient = await _context.Patients.Where(s => s.Email == email).FirstOrDefaultAsync();
            return patient;
        }
        public async Task<List<Prescription>> GetAllPrescriptionListAsync(int patientId)
        {
            return _context.Prescriptions.Include(s => s.Doctor).Include(s => s.Patient).Where(s => s.PatientId == patientId).ToList();
        }
        public async Task<List<HealthRecord>> GetAllHealthRecordListAsync(int patientId)
        {
            return _context.HealthRecords.Include(s => s.Patient).Where(s => s.PatientId == patientId).ToList();
        }
        public async Task<bool> ResetPassword(string email, string oldpassword, string newpassword)
        {
            Patient user = await _context.Patients.Where(p => p.Email == email).FirstOrDefaultAsync();
            
            if (user != null && user.Password == oldpassword)
            {
                user.Password = newpassword;
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<int> TotalHealthRecords()
        {
            return _context.HealthRecords.Count();  
        }
        public async Task<int> TotalPrescriptions()
        {
            return _context.Prescriptions.Count();
        }
        public async Task< List<Testimonial>> GetAllTestimonials()
        {
            return await _context.Testimonials.ToListAsync();
        }
    }
}

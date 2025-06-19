using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Infrastructure.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly HealWellDbContext _context;

        public CheckoutRepository(HealWellDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        public async Task<bool> UpdateAppointmentStatusAsync(Appointment appointment, string status)
        {
            appointment.Status = status;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<PaymentInfo> ProcessPayment(PaymentInfo paymentInfo)
        {
            await _context.PaymentInfo.AddAsync(paymentInfo);
            await _context.SaveChangesAsync();
            return paymentInfo;
        }
        public async Task<PaymentInfo> GetPaymentById(int id)
        {
            return await _context.PaymentInfo.Include(s=>s.Patient).Where(s=>s.Id ==id).FirstOrDefaultAsync();
        }
        public async Task<List<PaymentInfo>> GetAllPayments()
        {
            return await _context.PaymentInfo.ToListAsync();
        }
    }
}

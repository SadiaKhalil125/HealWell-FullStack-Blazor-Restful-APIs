using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CheckoutService
    {
        private readonly ICheckoutRepository _repo;

        public CheckoutService(ICheckoutRepository repo)
        {
            _repo = repo;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _repo.GetAppointmentByIdAsync(appointmentId);
        }

        public async Task<bool> UpdateAppointmentStatusAsync(Appointment appointment, string status)
        {
            return await _repo.UpdateAppointmentStatusAsync(appointment, status);   
        }
        public async Task<PaymentInfo> ProcessPayment(PaymentInfo paymentInfo)
        {
            return await _repo.ProcessPayment(paymentInfo);
        }
        public async Task<PaymentInfo> GetPaymentById(int id)
        {
            return await _repo.GetPaymentById(id);  
        }
        public async Task<List<PaymentInfo>> GetAllPayments()
        {
            return await _repo.GetAllPayments();    
        }
    }
}

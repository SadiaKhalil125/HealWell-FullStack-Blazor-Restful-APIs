using Domain.Models;
namespace Application.Interfaces
{
    public interface ICheckoutRepository
    {
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<PaymentInfo> ProcessPayment(PaymentInfo paymentInfo);
        Task<bool> UpdateAppointmentStatusAsync(Appointment appointment, string status);
        Task<PaymentInfo> GetPaymentById(int id);
        Task<List<PaymentInfo>> GetAllPayments();

    }
}

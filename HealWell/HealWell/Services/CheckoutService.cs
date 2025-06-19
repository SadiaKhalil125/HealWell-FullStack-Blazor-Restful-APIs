
using HealWell.Models;
//using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealWell.Pages.PatientPortal;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
namespace HealWell.Services
{
    public class CheckoutService
    {
        private readonly HttpClient _httpClient;

        public CheckoutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> ProcessPayment(PaymentInfo paymentInfo,int appointmentId)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Checkout/ProcessPayment", paymentInfo);
            var payment = await response.Content.ReadFromJsonAsync<PaymentInfo>();

            if (response.IsSuccessStatusCode)
            {
                var responsemsg = await _httpClient.PostAsync($"https://localhost:7047/api/Checkout/MarkAsPaidAndConfirm/{appointmentId}", null);

                if (responsemsg.IsSuccessStatusCode)
                {
                    return payment.Id;
                   
                }
                else
                {
                    // Optional: Show error alert
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Payment failed: " + error);
                    return -1;
                }

            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Payment failed: " + error);
                return -1;
            }
        }
        public async Task<PaymentInfo> GetPaymentById(int PaymentId)
        {
            try
            {
                var paymentInfo = await _httpClient.GetFromJsonAsync<PaymentInfo>($"https://localhost:7047/api/Checkout/getbyId/{PaymentId}");
                return paymentInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<PaymentInfo>> GetPayments()
        {
            try
            {
                var paymentInfo = await _httpClient.GetFromJsonAsync<List<PaymentInfo>>($"https://localhost:7047/api/Checkout/GetAll");
                return paymentInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        //{
        //    return await _repo.GetAppointmentByIdAsync(appointmentId);
        //}

        //public async Task<bool> UpdateAppointmentStatusAsync(Appointment appointment, string status)
        //{
        //    return await _repo.UpdateAppointmentStatusAsync(appointment, status);   
        //}
        //public async Task<PaymentInfo> ProcessPayment(PaymentInfo paymentInfo)
        //{
        //    return await _repo.ProcessPayment(paymentInfo);
        //}
        //public async Task<PaymentInfo> GetPaymentById(int id)
        //{
        //    return await _repo.GetPaymentById(id);  
        //}
    }
}

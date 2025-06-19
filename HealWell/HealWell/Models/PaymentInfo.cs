using System.Text.Json.Serialization;

namespace HealWell.Models
{

    public class PaymentInfo
    {
        // User Info Summary
        public int Id { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        // Card Details
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        // Confirmation Fields
        public string EmailConfirm { get; set; }
        public string PhoneConfirm { get; set; }

        // Save Card Option
        public bool SaveCard { get; set; }

        // Payment Amount (optional, inferred from UI)
        public decimal Amount { get; set; }
    }


}

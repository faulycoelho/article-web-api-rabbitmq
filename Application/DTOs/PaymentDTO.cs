using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The CardHolderName is required")]
        [MaxLength(50, ErrorMessage = "The CardHolderName must be at most 50 characters long")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "The CardNumber is required")]
        [CreditCard(ErrorMessage = "Invalid credit card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "The ExpiryMonth is required")]
        [Range(1, 12, ErrorMessage = "Expiry month must be between 1 and 12")]
        public int ExpiryMonth { get; set; }

        [Required(ErrorMessage = "The ExpiryYear is required")]
        [Range(2024, 2100, ErrorMessage = "Expiry year must be between 2024 and 2100")]
        public int ExpiryYear { get; set; }

        [Required(ErrorMessage = "The CVV is required")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be between 3 and 4 characters")]
        public string CVV { get; set; }
        public bool Success { get; set; }
        public DateTime Expiry => new DateTime(ExpiryYear, ExpiryMonth, 1);
    }
}

using Domain.Exceptions;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime Expiry { get; set; }
        public string CVV { get; set; }
        public bool Success { get; set; }

        public Payment(string CardHolderName, string CardNumber, DateTime Expiry, string CVV)
           => ValidateDomain(CardHolderName, CardNumber, Expiry, CVV);

        private void ValidateDomain(string CardHolderName, string CardNumber, DateTime Expiry, string CVV)
        {
            DomainPaymentExceptionValidation.When(string.IsNullOrWhiteSpace(CardHolderName), "CardHolderName is required.");
            DomainPaymentExceptionValidation.When(string.IsNullOrWhiteSpace(CardNumber), "CardNumber is required.");
            DomainPaymentExceptionValidation.When(string.IsNullOrWhiteSpace(CVV), "CVV is required.");
            DomainPaymentExceptionValidation.When(CVV.Length < 3, "CVV minimum length is 3.");
            DomainPaymentExceptionValidation.When(Expiry == DateTime.MinValue, "Expiry is required.");
            DomainPaymentExceptionValidation.When(Expiry < DateTime.Now, "Card expired.");

            this.CardHolderName = CardHolderName;
            this.CardNumber = CardNumber;
            this.Expiry = Expiry;
            this.CVV = CVV;
        }
    }
}

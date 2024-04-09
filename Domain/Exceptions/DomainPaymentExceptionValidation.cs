namespace Domain.Exceptions
{
    public class DomainPaymentExceptionValidation : Exception
    {
        public DomainPaymentExceptionValidation(string message) : base(message)
        {
        }
        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DomainPaymentExceptionValidation(error);
            }
        }
    }
}

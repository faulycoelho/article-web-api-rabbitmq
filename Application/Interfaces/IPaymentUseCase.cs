using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPaymentUseCase
    {
        Task GenerateAsync(PaymentDTO dto);
        Task ProcessPaymentAsync(int paymentId);
    }
}

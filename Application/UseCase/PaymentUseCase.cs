using Application.DTOs;
using Application.Interfaces;

namespace Application.UseCase
{
    public class PaymentUseCase(IPaymentService paymentService, IQueueService queueService) : IPaymentUseCase
    {
        public async Task GenerateAsync(PaymentDTO dto)
        {
            int id = await paymentService.AddAsync(dto);
            await queueService.SendMessage(id.ToString());
        }

        public async Task ProcessPaymentAsync(int paymentId)
        {
            var dto = await paymentService.GetAsync(paymentId);

            //simulating external api call
            await Task.Delay(1000);

            dto.Success = true;
            await paymentService.UpdateAsync(dto);            
        }
    }
}

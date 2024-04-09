using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetAllAsync();
        Task<PaymentDTO> GetAsync(int id);
        Task<int> AddAsync(PaymentDTO Dto);
        Task UpdateAsync(PaymentDTO Dto);
        Task DeleteAsync(int id);
    }
}

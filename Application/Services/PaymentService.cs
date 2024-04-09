using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PaymentService(IPaymentRepository _repository, IMapper _mapper) : IPaymentService
    {
        public async Task<int> AddAsync(PaymentDTO Dto)
        {
            var entity = _mapper.Map<Payment>(Dto);
            await _repository.CreateAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDTO>>(entities);
        }

        public async Task<PaymentDTO> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            return _mapper.Map<PaymentDTO>(entity);
        }

        public async Task UpdateAsync(PaymentDTO Dto)
        {
            var entity = _mapper.Map<Payment>(Dto);
            await _repository.UpdateAsync(entity);
        }
    }
}

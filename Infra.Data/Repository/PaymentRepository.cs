using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class PaymentRepository(StoreDbContext _context) : IPaymentRepository
    {
        public async Task<Payment> CreateAsync(Payment obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<Payment> DeleteAsync(Payment obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment?> GetAsync(int id)
        {
            return await _context.Payments.AsNoTracking().FirstAsync(o => o.Id == id);
        }

        public async Task<Payment> UpdateAsync(Payment obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}

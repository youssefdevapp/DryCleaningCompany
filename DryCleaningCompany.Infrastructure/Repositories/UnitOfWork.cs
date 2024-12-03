using DryCleaningCompany.Domain.Entities;
using DryCleaningCompany.Infrastructure.Data;

namespace DryCleaningCompany.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DryCleaningDbContext _context;

        public IGenericRepository<ShopSchedule> ShopSchedules { get; }

        public IGenericRepository<Schedule> Schedules { get; }

        public UnitOfWork(DryCleaningDbContext context)
        {
            _context = context;
            ShopSchedules = new GenericRepository<ShopSchedule>(_context);
            Schedules = new GenericRepository<Schedule>(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ShopSchedule> ShopSchedules { get; }
        IGenericRepository<Schedule> Schedules { get; }

        Task SaveAsync();
    }
}
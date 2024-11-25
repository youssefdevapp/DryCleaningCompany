using DryCleaningCompany.Domain.Entities;
using DryCleaningCompany.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleaningCompany.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DryCleaningDbContext _context;

        public GenericRepository<ShopSchedule> ShopSchedules { get; }

        public GenericRepository<Schedule> Schedules { get; }

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
        GenericRepository<ShopSchedule> ShopSchedules { get; }
        GenericRepository<Schedule> Schedules { get; }

        Task SaveAsync();
    }
}
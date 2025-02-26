using Koneshgar.DataLayer.Contexts;
using Koneshgar.Domain.Interfaces;

namespace Koneshgar.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskContext _context;

        public UnitOfWork(TaskContext context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

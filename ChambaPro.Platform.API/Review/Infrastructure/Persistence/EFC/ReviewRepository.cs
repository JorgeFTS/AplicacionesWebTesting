using ChambaPro.Platform.API.Review.Domain.Models.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.Review.Infrastructure.Persistence.EFC
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DbContext _context;

        public ReviewRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reviews review)
        {
            await _context.Set<Reviews>().AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public void Remove(Reviews review)
        {
            _context.Set<Reviews>().Remove(review);
            _context.SaveChanges();
        }

        public async Task<Reviews> FindByIdAsync(int id)
        {
            return await _context.Set<Reviews>().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reviews>> FindByTechnicianIdAsync(int technicianId)
        {
            return await _context.Set<Reviews>()
                .Where(r => r.TechnicianId == technicianId)
                .ToListAsync();
        }
    }
}
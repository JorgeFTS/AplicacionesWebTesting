using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Domain.Models.Repositories
{
    public interface IReviewRepository
    {
        Task AddAsync(Reviews review);
        Task<Reviews> FindByIdAsync(int id);
        Task<IEnumerable<Reviews>> FindByTechnicianIdAsync(int technicianId);
        void Remove(Reviews review);

    }
}
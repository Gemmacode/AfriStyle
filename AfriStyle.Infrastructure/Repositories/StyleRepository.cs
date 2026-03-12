using AfriStyle.Application.Interfaces;
using AfriStyle.Domain.Entities;
using AfriStyle.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Infrastructure.Repositories
{
    public class StyleRepository : IStyleRepository
    {
        private static readonly IReadOnlyList<HairstyleRecommendation> _all
            = AfriStyleSeedData.GetAll().AsReadOnly();

        public Task<IReadOnlyList<HairstyleRecommendation>> GetAllAsync(
            bool forMen,
            CancellationToken ct = default)
        {
            var filtered = _all
                .Where(s => forMen ? s.SuitableForMen : s.SuitableForWomen)
                .ToList()
                .AsReadOnly() as IReadOnlyList<HairstyleRecommendation>;

            return Task.FromResult(filtered);
        }
    }
}

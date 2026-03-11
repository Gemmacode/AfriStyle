using AfriStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application.Interfaces
{
    public interface IStyleRepository
    {
        Task<IReadOnlyList<HairstyleRecommendation>> GetAllAsync(
            bool forMen,
            CancellationToken ct = default);
    }
}

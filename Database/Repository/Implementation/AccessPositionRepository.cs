using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    public class AccessPositionRepository(Context context) : IAccessPositionRepository
    {
        public async Task<List<AccessPosition>> GetAllAsync()
        {
            return await context.AccessPositions
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace Database.Services
{
    public class RefreshTokenRepositoryService : RepositoryService<RefreshToken>, IRefreshTokenRepositoryService
    {
        public RefreshTokenRepositoryService(DbContext dbContext) : base(dbContext)
        {

        }

        public Task<RefreshToken> GetAsync(string id)
        {
            return Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveRefreshTokenForUserAsync(string userId)
        {
            var userToken = await Queryable().Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (userToken != null)
            {
                Delete(userToken);
            }
        }
    }
}

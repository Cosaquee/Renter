using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;

namespace Database.Services
{
    public class RefreshTokenRepositoryService : RepositoryService<RefreshToken>, IRefreshTokenRepositoryService
    {
        public RefreshTokenRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public RefreshToken Get(string id)
        {
            return Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public void RemoveRefreshTokenForUser(string userId)
        {
            var userToken = Queryable().Where(x => x.UserId == userId).FirstOrDefault();
            if (userToken != null)
            {
                Delete(userToken);
            }
        }
    }
}
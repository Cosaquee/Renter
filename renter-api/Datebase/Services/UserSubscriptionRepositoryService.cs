using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class UserSubscriptionRepositoryService : RepositoryService<UserSubscription>, IUserSubscriptionRepositoryService
    {
        public UserSubscriptionRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
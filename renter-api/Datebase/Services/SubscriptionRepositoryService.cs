using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class SubscriptionRepositoryService : RepositoryService<Subscription>, ISubscriptionRepositoryService
    {
        public SubscriptionRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
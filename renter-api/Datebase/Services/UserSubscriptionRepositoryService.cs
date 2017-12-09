using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class UserSubscriptionRepositoryService : RepositoryService<UserSubscription>, IUserSubscriptionRepositoryService
	{
		public UserSubscriptionRepositoryService(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
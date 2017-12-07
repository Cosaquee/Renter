using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IRefreshTokenRepositoryService : IRepositoryService<RefreshToken>
    {
        void RemoveRefreshTokenForUser(string userId);
        RefreshToken Get(string id);
    }
}

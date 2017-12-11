using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IDbInitializer
    {
        Task Seed();
    }
}

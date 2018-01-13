using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IDbInitializer
    {
        Task Seed();
    }
}
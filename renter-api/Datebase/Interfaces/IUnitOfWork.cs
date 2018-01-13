using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();

        Task SaveAsync();
    }
}
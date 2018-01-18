using Models.Models;

namespace Database.Interfaces
{
    public interface IBookRepositoryService : IRepositoryService<Book>
    {
         Book ConfirmReturn(Book book);
    }
}
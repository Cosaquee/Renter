using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class BookRepositoryService : RepositoryService<Book>, IBookRepositoryService
    {
        public BookRepositoryService(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Book ConfirmReturn(Book book)
        {
            book.Rented = false;
            dbContext.Update(book);
            dbContext.SaveChanges();
            return book;
        }
    }
}
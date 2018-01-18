using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Services
{
    public class RentBookRepositoryService : RepositoryService<RentBook>, IRentBookRepositoryService
    {
        public RentBookRepositoryService(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Book getBookById(long bookID)
        {
            RepositoryService<Book> rs = new BookRepositoryService(dbContext);
            return rs.Queryable().Where(x => x.Id == bookID).FirstOrDefault();
        }

        public IEnumerable<Book> GetAvaiableBooksByIsbn(string isbn)
        {
            RepositoryService<Book> rs = new BookRepositoryService(dbContext);
            return rs.Queryable().Where(x => x.ISBN == isbn && x.Rented == false).Include(x => x.Author);
        }

        public List<Book> GetAvaiableBooksByTitle(string title)
        {
            var books = Queryable().Include(x => x.Book).Where(x => x.Book.Title == title).GroupBy(x => x.Book);
            var result = new List<Book>();

            foreach (var book in books)
            {
                if (!book.Where(x => x.To > DateTime.Now).Any())
                {
                    result.Add(book.Key);
                }
            }

            return result;
        }

        public bool IsBookAvailable(long bookId)
        {
            return !Queryable().Where(x => x.BookId == bookId && x.To >= DateTime.Now).Any();
        }

        public RentBook Rent(long bookId, string userId, TimeSpan time)
        {
            if (IsBookAvailable(bookId) == false)
                return null;

            var now = DateTime.Now;

            var rentBook = new RentBook
            {
                BookId = bookId,
                UserId = userId,
                From = now,
                To = now.Add(time),
                Received = false
            };

            var book = getBookById(bookId);
            book.Rented = true;

            dbContext.Update(book);

            this.Insert(rentBook);
            dbContext.SaveChanges();

            return rentBook;
        }

        public List<RentBook> GetUserRentHisotry(string userId)
        {
            return Queryable().Where(x => x.UserId == userId).Include(x => x.Book).ToList();
        }

        public List<RentBook> GetBookRentHisotry(long bookId)
        {
            return Queryable().Where(x => x.BookId == bookId).ToList();
        }

        public RentBook Confirm(long bookID)
        {
            var book = Queryable().Where(x => x.BookId == bookID).FirstOrDefault();
            book.Received = true;
            dbContext.Update(book);
            dbContext.SaveChanges();

            return book;
        }
        public RentBook ConfirmReturn(long bookID)
        {
            var rentBook = Queryable().Where(x => x.BookId == bookID).FirstOrDefault();
            dbContext.Remove(rentBook);
            dbContext.SaveChanges();

            return rentBook;
        }
    }
}

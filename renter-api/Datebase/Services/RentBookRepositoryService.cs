using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.Services
{
    public class RentBookRepositoryService : RepositoryService<RentBook>, IRentBookRepositoryService
    {
        public RentBookRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public List<Book> GetAvaiableBooksByIsbn(string isbn)
        {
            var books = Queryable().Include(x => x.Book).Where(x => x.Book.ISBN == isbn).GroupBy(x => x.Book);
            var result = new List<Book>();

            foreach (var book in books)
            {
                if(!book.Where(x => x.To > DateTime.Now).Any())
                {
                    result.Add(book.Key);
                }
            }

            return result;
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
                To = now.Add(time)
            };

            this.Insert(rentBook);

            return rentBook;
        }

        public List<RentBook> GetUserRentHisotry(string userId)
        {
            return Queryable().Where(x => x.UserId == userId).ToList();
        }

        public List<RentBook> GetBookRentHisotry(long bookId)
        {
            return Queryable().Where(x => x.BookId == bookId).ToList();
        }

    }
}

using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IRentBookRepositoryService : IRepositoryService<RentBook>
    {
        RentBook Rent(int bookId, string userId, TimeSpan time);
        bool IsBookAvailable(int bookId);
        List<Book> GetAvaiableBooksByTitle(string title);
        List<Book> GetAvaiableBooksByIsbn(string isbn);
        List<RentBook> GetUserRentHisotry(string userId);
        List<RentBook> GetBookRentHisotry(int bookId);
    }
}

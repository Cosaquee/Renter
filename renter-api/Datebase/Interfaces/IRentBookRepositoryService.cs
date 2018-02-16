using Models.Models;
using System;
using System.Collections.Generic;

namespace Database.Interfaces
{
    public interface IRentBookRepositoryService : IRepositoryService<RentBook>
    {
        RentBook Rent(long bookId, string userId, TimeSpan time, String ISBN);

        bool IsBookAvailable(long bookId);

        List<Book> GetAvaiableBooksByTitle(string title);

        IEnumerable<Book> GetAvaiableBooksByIsbn(string isbn);

        List<RentBook> GetUserRentHisotry(string userId);

        List<RentBook> GetBookRentHisotry(long bookId);
        RentBook Confirm(long bookID);

        RentBook ConfirmReturn(long bookID);
    }
}
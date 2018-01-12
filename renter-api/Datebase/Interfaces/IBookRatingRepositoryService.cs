using System;
using Models.Models;
namespace Database.Interfaces
{
    public interface IBookRatingRepositoryService : IRepositoryService<BookRating>
    {
        float GetRate(string bookTitle);
        BookRating GetRateByUser(string ISBN, string userId);
        void RateBook(string ISBN, string userId, int rate);
    }
}

using System;
using Models.Models;
namespace Database.Interfaces
{
    public interface IBookRatingRepositoryService : IRepositoryService<BookRating>
    {
        float GetRate(string bookTitle);
        BookRating GetRateByUser(string bookTitle, string userId);
        void RateBook(string bookTitle, string userId, int rate);
    }
}

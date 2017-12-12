using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.Services
{
    public class BookRatingRepositoryService: RepositoryService<BookRating>, IBookRatingRepositoryService
    {
		public BookRatingRepositoryService(DbContext dbContext) : base(dbContext)
        {
		}

        public float GetRate(string bookTitle)
        {
            var rates = Queryable().Where(x => x.BookTitle == bookTitle);
            var ratesCount = rates.Count();

            if (ratesCount <= 0)
                return 0;

            float sumRates = (float)rates.Sum(x => x.Rate);
            return sumRates / ratesCount;
        }

        public BookRating GetRateByUser(string bookTitle, string userId)
        {
            return Queryable().Where(x => x.BookTitle == bookTitle && x.UserId == userId).FirstOrDefault();
        }

        public void RateBook(string bookTitle, string userId, int rate)
        {
            if (this.IsRateValid(rate))
                throw new Exception("Invalid rate");

            var rateItem = GetRateByUser(bookTitle, userId);
            if(rateItem != null)
            {
                rateItem.Rate = rate;
                this.Update(rateItem);
            }
            else
            {
                rateItem = new BookRating
                {
                    BookTitle = bookTitle,
                    UserId = userId,
                    Rate = rate
                };
                this.Insert(rateItem);
            }
        }

		private const int minRate = 1;
		private const int maxRate = 10;
        private bool IsRateValid(int rate)
        {
            return rate >= minRate && rate <= maxRate;
        }
    }
}

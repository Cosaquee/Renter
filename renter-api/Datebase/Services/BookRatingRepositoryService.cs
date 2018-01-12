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

        public float GetRate(string ISBN)
        {
            var rates = Queryable().Where(x => x.ISBN == ISBN);
            var ratesCount = rates.Count();

            if (ratesCount <= 0)
                return 0;

            float sumRates = (float) rates.Sum(x => x.Rate);
            return sumRates / ratesCount;
        }

        public BookRating GetRateByUser(string ISBN, string userId)
        {
            return Queryable().Where(x => x.ISBN == ISBN && x.UserId == userId).FirstOrDefault();
        }

        // We use ISBN here to rate books to make sure that differnt copies of same books will always have same rate.
        public void RateBook(string ISBN, string userId, int rate)
        {


            if (!this.IsRateValid(rate))
                throw new Exception("Invalid rate");

            var rateItem = GetRateByUser(ISBN, userId);

            if(rateItem != null)
            {
                rateItem.Rate = rate;
                this.Update(rateItem);
                dbContext.SaveChanges();
            }
            else
            {
                rateItem = new BookRating { ISBN = ISBN, UserId = userId, Rate = rate };
                this.Insert(rateItem);
                dbContext.SaveChanges();
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

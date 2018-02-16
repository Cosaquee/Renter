using AutoMapper;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos.Book;
using Models.Dtos.BookRating;
using Models.Dtos.RentBook;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/book")]
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBookRepositoryService bookRepositoryService;
        private readonly IRentBookRepositoryService rentBookRepositoryService;
        private readonly IBookRatingRepositoryService bookRatingRepositoryService;

        public BooksController(IUnitOfWork unitOfWork, IBookRepositoryService bookRepositoryService,
                               IRentBookRepositoryService rentBookRepositoryService, IBookRatingRepositoryService bookRatingRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.bookRepositoryService = bookRepositoryService;
            this.rentBookRepositoryService = rentBookRepositoryService;
            this.bookRatingRepositoryService = bookRatingRepositoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Book> Get() => bookRepositoryService.Queryable().Include(x => x.Author).Include(x => x.Category).Include(x => x.BookRatings);

        [HttpGet("{id}")]
        public Book Get(long id) => bookRepositoryService.Queryable().Where(x => x.Id == id).Include(x => x.Author).FirstOrDefault();

        [HttpPost]
        public void CreateBook([FromBody]BookDto book)
        {
            bookRepositoryService.Insert(Mapper.Map<Book>(book));
            unitOfWork.Save();
        }

        [HttpPost("cover")]
        [Authorize(Roles="Administrator, Employee")]
        public void AddCover([FromBody] CoversDTO covers)
        {
            var book = bookRepositoryService.Queryable().Where(x => x.Id == covers.ID).FirstOrDefault();
            book.CoverURL = covers.CoverURL;
            book.ResizedCoverURL = covers.ResizedCoverURL;
            bookRepositoryService.Update(book);
            unitOfWork.Save();
        }

        [HttpGet("Avaiable/{id}")]
        public IActionResult IsBookAvaiable(long id)
        {
            var bookExists = bookRepositoryService.Get(id) != null;
            if (!bookExists)
            {
                return BadRequest("Book does not exist");
            }
            var isAvaiable = rentBookRepositoryService.IsBookAvailable(id);
            return Ok(isAvaiable);
        }

        [HttpGet("GetAvaiableByIsbn/{isbn}")]
        public IEnumerable<Book> GetAvaiableBooksByIsbn(string isbn) => rentBookRepositoryService.GetAvaiableBooksByIsbn(isbn);

        [HttpGet("GetAvaiableByTitle/{title}")]
        public IEnumerable<Book> GetAvaiableBooksByTitle(string title) => rentBookRepositoryService.GetAvaiableBooksByTitle(title);

        [HttpGet("Rent/{bookId}/{userId}/{days}")]
        public IActionResult Rent(long bookId, string userId, int days)
        {
            var book = this.bookRepositoryService.Get(bookId);
            if (book == null)
            {
                return BadRequest("Book is not avaiable for rent.");
            }
            var timeSpan = TimeSpan.FromDays(days);
            var rentBook = rentBookRepositoryService.Rent(bookId, userId, timeSpan, "");

            return Ok(rentBook);
        }

        [HttpPost("rent")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult Rent([FromBody] RentBookDTO rentBookDTO)
        {
            var timeSpan = TimeSpan.FromDays(rentBookDTO.RentDuration);
            var rentBook = rentBookRepositoryService.Rent(rentBookDTO.BookID, rentBookDTO.UserID, timeSpan, rentBookDTO.ISBN);
            if (rentBook == null)
            {
                return BadRequest("Book is not avaiable for rent.");
            }

            return Ok(rentBook);
        }

        [HttpGet("rate/{isbn}")]
        public IActionResult Rate(string isbn)
        {
            var rate = this.bookRatingRepositoryService.GetRate(isbn);
            return Ok(rate);
        }

        [HttpPost("rate")]
        public void Rate([FromBody] BookRatingDto bookRating) => this.bookRatingRepositoryService.RateBook(bookRating.ISBN, bookRating.UserID, bookRating.Rate);

        [HttpGet("Category/{categoryName}")]
        public IEnumerable<Book> GetBooksByCategory(string categoryName) => this.bookRepositoryService.Queryable().Include(x => x.Category).Where(x => string.Equals(x.Category.Name, categoryName, StringComparison.OrdinalIgnoreCase)).ToList();

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var book = this.bookRepositoryService.Get(id);
            this.bookRepositoryService.Delete(book);
            return Ok("Book deleted");
        }

        [HttpGet("rent")]
        [Authorize(Roles = "Administrator, Employee")]
        public IEnumerable<RentBook> GetAllRentedBooks() => rentBookRepositoryService.Queryable().Where(x => x.State != State.ARCHIVED).Include(x => x.User).Include(x => x.Book).Include(x => x.Book.Author).ToList();

        [HttpPost("confirm/{ID}")]
        [Authorize(Roles = "Administrator, Employee")]
        public void Confirm(long ID) => this.rentBookRepositoryService.Confirm(ID);

        [HttpPost("confirm-return/{ID}")]
        [Authorize(Roles = "Administrator, Employee")]
        public void ConfirmReturn(long ID)
        {
            this.rentBookRepositoryService.ConfirmReturn(ID);
            var book = bookRepositoryService.Get(ID);
            this.bookRepositoryService.ConfirmReturn(book);
        }

        [HttpGet("latest")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<Book> Latest() => this.bookRepositoryService.Queryable().Include(x => x.Author).Include(x => x.Category).Distinct().Take(5);

        [HttpGet("rent/current/{userID}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentBook> CurrentRent(string userID) => this.rentBookRepositoryService.Queryable().Where(x => x.UserId == userID).Where(x => x.State != State.ARCHIVED).Include(x => x.Book);

        [HttpGet("rent/history/{userID}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentBook> UserHistory(string userID) => this.rentBookRepositoryService.Queryable().Where(x => x.UserId == userID).Where(x => x.State == State.ARCHIVED).Include(x => x.Book);

        [HttpGet("history/{isbn}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentBook> BookHistory(string isbn) => this.rentBookRepositoryService.Queryable().Include(x => x.User).Include(x => x.Book).Where(x => x.ISBN == isbn).Where(x => x.State == State.ARCHIVED).ToList();
    }
}

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

        // GET api/values
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return bookRepositoryService.Queryable().Include(x => x.Author).Include(x => x.Category);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(long id)
        {
            return bookRepositoryService.Queryable().Where(x => x.Id == id).Include(x => x.Author).FirstOrDefault();
        }

        // POST api/values
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
        public IActionResult GetAvaiableBooksByIsbn(string isbn)
        {
            var avaiableBooks = rentBookRepositoryService.GetAvaiableBooksByIsbn(isbn);
            return Ok(avaiableBooks);
        }

        [HttpGet("GetAvaiableByTitle/{title}")]
        public IActionResult GetAvaiableBooksByTitle(string title)
        {
            var avaiableBooks = rentBookRepositoryService.GetAvaiableBooksByTitle(title);
            return Ok(avaiableBooks);
        }

        [HttpGet("Rent/{bookId}/{userId}/{days}")]
        public IActionResult Rent(long bookId, string userId, int days)
        {
            var timeSpan = TimeSpan.FromDays(days);
            var rentBook = rentBookRepositoryService.Rent(bookId, userId, timeSpan);
            if (rentBook == null)
            {
                return BadRequest("Book is not avaiable for rent.");
            }
            return Ok(rentBook);
        }


        [HttpPost("rent")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult Rent([FromBody] RentBookDTO rentBookDTO)
        {
            var timeSpan = TimeSpan.FromDays(rentBookDTO.RentDuration);
            var rentBook = rentBookRepositoryService.Rent(rentBookDTO.BookID, rentBookDTO.UserID, timeSpan);
            if (rentBook == null)
            {
                return BadRequest("Book is not avaiable for rent.");
            }
            return Ok(rentBook);
        }

        [HttpGet("RentHistory/{bookId}")]
        public IActionResult RentHistory(long bookId)
        {
            var rentBook = rentBookRepositoryService.GetBookRentHisotry(bookId);
            return Ok(rentBook);
        }

        [HttpGet("Rate/{title}")]
        public IActionResult Rate(string title)
        {
            var rate = this.bookRatingRepositoryService.GetRate(title);
            return Ok(rate);
        }

        [HttpPost("rate")]
        public IActionResult Rate([FromBody] BookRatingDto bookRating)
        {
            try
            {
                this.bookRatingRepositoryService.RateBook(bookRating.ISBN, bookRating.UserID, bookRating.Rate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Category/{categoryName}")]
        public IActionResult GetBooksByCategory(string categoryName)
        {
            var books = this.bookRepositoryService.Queryable().Include(x => x.Category).Where(x => string.Equals(x.Category.Name, categoryName, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(books);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var book = this.bookRepositoryService.Get(id);
            this.bookRepositoryService.Delete(book);
            return Ok("Book deleted");
        }

        [HttpGet("rent")]
        [Authorize(Roles="Administrator, Employee")]
        public IEnumerable<RentBook> GetAllRentedBooks()
        {
            return rentBookRepositoryService.Queryable().Include(x => x.User).Include(x => x.Book).ToList();
        }

        [HttpPost("confirm/{ID}")]
        [Authorize(Roles="Administrator, Employee")]
        public IActionResult Confirm(long ID)
        {
            this.rentBookRepositoryService.Confirm(ID);

            return Ok("Book is received");
        }


        [HttpPost("confirm-return/{ID}")]
        [Authorize(Roles="Administrator, Employee")]
        public IActionResult ConfirmReturn(long ID)
        {
            this.rentBookRepositoryService.ConfirmReturn(ID);
            var book = bookRepositoryService.Get(ID);
            this.bookRepositoryService.ConfirmReturn(book);
            return Ok("Book is returned");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Interfaces;
using Models.Models;
using Models.Dtos.Book;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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

        public BooksController(IUnitOfWork unitOfWork, IBookRepositoryService bookRepositoryService, IRentBookRepositoryService rentBookRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.bookRepositoryService = bookRepositoryService;
            this.rentBookRepositoryService = rentBookRepositoryService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return bookRepositoryService.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return bookRepositoryService.Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void CreateBook([FromBody]BookDto book)
        {
            bookRepositoryService.Insert(Mapper.Map<Book>(book));
            unitOfWork.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateBook(int id, [FromBody]BookDto book)
        {
            var bookDb = Mapper.Map<Book>(book);
            bookDb.Id = id;
            bookRepositoryService.Update(bookDb);
            unitOfWork.Save();
        }

        // DELETE api/values/5
        [HttpGet("Avaiable/{id}")]
        public IActionResult IsBookAvaiable(int id)
        {
            var bookExists = bookRepositoryService.Get(id) != null;
            if(!bookExists)
            {
                return BadRequest("Book does not exist");
            }
            var isAvaiable = rentBookRepositoryService.IsBookAvaiable(id);
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
        public IActionResult Rent(int bookId, string userId, int days)
        {
            var timeSpan = TimeSpan.FromDays(days);
            var rentBook = rentBookRepositoryService.Rent(bookId, userId, timeSpan);
            if(rentBook == null)
            {
                return BadRequest("Book is not avaiable for rent.");
            }
            return Ok(rentBook);
        }

        [HttpGet("RentHistory/{bookId}")]
        public IActionResult RentHistory(int bookId)
        {
            var rentBook = rentBookRepositoryService.GetBookRentHisotry(bookId);
            return Ok(rentBook);
        }
    }
}

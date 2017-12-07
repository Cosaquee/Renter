﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IRentBookRepositoryService : IRepositoryService<RentBook>
    {
        RentBook Rent(int bookId, int userId, TimeSpan time);
        bool IsBookAvaiable(int bookId);
        List<Book> GetAvaiableBooksByTitle(string title);
        List<Book> GetAvaiableBooksByIsbn(string isbn);
    }
}

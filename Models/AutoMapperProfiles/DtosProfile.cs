using AutoMapper;
using Models.Dtos.Book;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.AutoMapperProfiles
{
    public class DtosProfile : Profile
    {
        public DtosProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
        }
    }
}

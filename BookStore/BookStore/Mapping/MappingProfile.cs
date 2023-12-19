using AutoMapper;
using BookStore.BookOperation.CreateBook;
using BookStore.BookOperation.GetBooks;
using BookStore.BookOperation.UpdateBook;
using BookStore.Common;
using BookStore.Entities;

namespace BookStore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, GetBookViewModel>().ForMember(dest => dest.Genre,
            opt => opt.MapFrom(src => ((GenreType)src.GenreId).ToString())).ForMember(des => des.PublishDate,
            opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<Book, BooksViewModel>().ForMember(des => des.Genre,
            opt => opt.MapFrom(src => ((GenreType)src.GenreId).ToString())).ForMember(des => des.PublishDate,
            opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));

        CreateMap<UpdateBookModel, Book>();
    }
}
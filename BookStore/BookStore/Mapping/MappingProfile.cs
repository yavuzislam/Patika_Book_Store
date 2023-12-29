using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.BookOperation.Commands.CreateBook;
using BookStore.Application.BookOperation.Commands.UpdateBook;
using BookStore.Application.BookOperation.Queries.GetBooks;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Entities;

namespace BookStore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, GetBookViewModel>().ForMember(dest => dest.Genre,
            opt => opt.MapFrom(src => src.Genre.Name)).ForMember(des => des.PublishDate,
            opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(des => des.Genre,
                opt => opt.MapFrom(src => src.Genre.Name)).ForMember(des => des.PublishDate,
                opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));

        CreateMap<UpdateBookModel, Book>();

        CreateMap<Genre, GenreModel>();
        CreateMap<Genre, GetGenreDetailModel>();
        CreateMap<CreateGenreModel, Genre>();

        CreateMap<Author, AuthorModel>().ForMember(dest => dest.BirthDate,
            opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<Author, AuthorDetailModel>().ForMember(dest => dest.BirthDate,
            opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<CreateAuthorModel, Author>();
        CreateMap<UpdateAuthorModel, Author>();

        CreateMap<CreateUserModel, User>();
    }
}
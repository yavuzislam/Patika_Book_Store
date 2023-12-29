using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.UserOperations.Commands.CreateUser;

public class CreateUserCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserModel Model { get; set; }

    public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);
        if (user is not null)
            throw new InvalidOperationException("User already exists!");
        var result = _mapper.Map<User>(Model);
        _dbContext.Users.Add(result);
        _dbContext.SaveChanges();
    }
}

public class CreateUserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
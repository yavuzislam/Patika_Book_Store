using AutoMapper;
using BookStore.DbOperations;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Application.UserOperations.Commands.CreateToken;

public class CreateTokenCommnad
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CreateTokenModel Model { get; set; }

    public CreateTokenCommnad(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
        if (user is null)
            throw new InvalidOperationException("Invalid email or password");

        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();

        return token;
    }
}

public class CreateTokenModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
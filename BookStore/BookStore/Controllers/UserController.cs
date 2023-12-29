using AutoMapper;
using BookStore.Application.UserOperations.Commands.CreateToken;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Application.UserOperations.Commands.RefreshToken;
using BookStore.DbOperations;
using BookStore.TokenOperations.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IMapper mapper, IBookStoreDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel newUser)
        {
            var command = new CreateUserCommand(_context, _mapper) { Model = newUser };
            var validator = new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> Login([FromBody] CreateTokenModel login)
        {
            CreateTokenCommnad command = new CreateTokenCommnad(_context, _mapper, _configuration) { Model = login };
            var token = command.Handle();
            return token;
        }

        [HttpPost("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration)
            {
                RefreshToken = token
            };
            var resultToken = command.Handle();
            return resultToken;
        }
    }
}
﻿using BookStore.DbOperations;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Application.UserOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    private readonly IBookStoreDbContext _context;
    private readonly IConfiguration _configuration;

    public string RefreshToken { get; set; }

    public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(x =>
            x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if (user is null)
            throw new InvalidOperationException("Refresh Token not found!");
        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();

        return token;
    }
}
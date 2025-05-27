    using BookLib.Core.Interfaces;
    using BookLib.Core.Models;
    using BookLib.Data.Repositories;
    using BookLib.Data;
    using BookLib.Services;
    using Microsoft.EntityFrameworkCore;

    var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IRepository<Book>, BookRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<BookService>();

    var app = builder.Build();
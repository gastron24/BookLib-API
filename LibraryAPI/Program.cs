using LibraryAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using LibraryAPI.Data;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddDbContext<BookContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



app.MapGet("/", () => "Добро пожаловать в Library.Api!");

var books = new List<Book>
{
    new Book {Id = 1, Title = "Меня не сломать", Author = "David Goggins", Year = 2021},
    new Book {Id = 2, Title = "Выносливость", Author = "Alex Hatchinson", Year = 2024}
};


app.UseHttpsRedirection();

app.MapGet("/books", () =>
{
    return Results.Ok(books);
});

app.MapGet("books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    if(book == null)
    {
        return Results.NotFound("Книга не найденна");
    }
    return Results.Ok(book);
});

app.MapPost("/books", (Book newBook) =>
{
    var maxId = books.Count > 0 ? books.Max(b => b.Id) : 0;
    newBook.Id = maxId + 1;
    books.Add(newBook);
    return Results.Created($"/books/{newBook.Id}", newBook);
});

app.MapDelete("/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    if(book == null)
    {
        return Results.NotFound("Книга не найдена"); 
    }
    books.Remove(book);
    return Results.Ok(book);
});

app.MapGet("/books", (string? author, int? year, string? title) =>
{
    var results = books.AsQueryable();

    if(!string.IsNullOrWhiteSpace(author))
    {
        results = results.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
    }

    if (year.HasValue && year > 0)
    {
        results = results.Where(b => b.Year == year.Value);
    }
    if(!string.IsNullOrWhiteSpace(title))
    {
        results = results.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }

    return Results.Ok(results.ToList());
});

app.Run();



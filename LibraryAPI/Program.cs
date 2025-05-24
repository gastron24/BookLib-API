using LibraryAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;





var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.Run();



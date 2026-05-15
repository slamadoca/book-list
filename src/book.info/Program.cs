using Microsoft.EntityFrameworkCore;
using book.info.Model;

namespace book.info;
public class Program
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Prepare a database- either Sqlite or SQL Server.
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<BookInfo>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("BookInfo") ?? throw new InvalidOperationException("Connection string 'BookInfo' not found.")));
        }
        else
        {
            builder.Services.AddDbContext<BookInfo>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("BookInfo") ?? throw new InvalidOperationException("Connection string 'BookInfo' not found.")));
        }

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "BookInfoAPI";
            config.Title = "BookInfoAPI v1";
            config.Version = "v1";
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {          
            app.UseOpenApi();
            app.UseSwaggerUi(config =>
            {
                config.DocumentTitle = "BookInfoAPI";
                config.Path = "/swagger";
                config.DocumentPath = "/swagger/{documentName}/swagger.json";
                config.DocExpansion = "list";
            });
        }

        app.MapGet("/bookinfo", async (BookInfo db) => await db.Books.ToListAsync());

        app.MapGet("/bookinfo/{id}",
            async (int id, BookInfo db) => await db.Books.FindAsync(id) is { } item ? Results.Ok(item) : Results.NotFound());

        app.MapPost("/bookinfo", async (Book book, BookInfo db) =>
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return Results.Created($"/bookinfo/{book.Id}", book);
        });

        app.MapPut("/bookinfo/{id}", async (int id, Book inputBook, BookInfo db) =>
        {
            var book = await db.Books.FindAsync(id);
            if (book is null) return Results.NotFound();

            book.Title = inputBook.Title;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/bookinfo/{id}", async (int id, BookInfo db) =>
        {
            if (await db.Books.FindAsync(id) is not { } book) return Results.NotFound();

            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Results.NoContent();

        });

        app.Run();
    }
}

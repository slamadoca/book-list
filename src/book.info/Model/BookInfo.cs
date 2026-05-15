using Microsoft.EntityFrameworkCore;

namespace book.info.Model;

public class BookInfo(DbContextOptions<BookInfo> options) : DbContext(options)
{
    private const string DbName = "BookInfoData.db";
    public string DbPath { get; }
    public DbSet<Book> Books => Set<Book>();

    // public BookInfo()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = Path.Join(path, DbName);
    // }

}
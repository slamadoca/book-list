namespace book.info.Model;
public class Shelf
{
    public int ShelfId {get; set;}
    public string ShelfName {get; set;}

    public ICollection<Book> ShelvedBooks {get;} = new List<Book>();
}
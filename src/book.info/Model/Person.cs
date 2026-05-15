namespace book.info.Model;

public class Person
{
    public int PersonId {get; private set;}
    public string Name {get; set;}

    public ICollection<Book> Books {get;} = new List<Book>();

}
namespace book.info.Model;

public class Book
{
    public int Id { get; set; }
    public string? ISBN { get; set; } //TODO- what format is an ISBN anyway?j
    public string? ISBN13 { get; set; }
    public string Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
}
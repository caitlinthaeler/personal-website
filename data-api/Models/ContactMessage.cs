namespace data_api.Models;

public class ContactMessage
{
    public int Id { get; set;}
    public string FirstName { get; set;} = null!;
    public string LastName { get; set;} = null!;
    public string Email { get; set;} = null!;
    public string Subject { get; set;} = null!;
    public string Message { get; set;} = null!;
    public DateTime? SentAt { get; set; } = DateTime.UtcNow; // convert to local time on front end
}
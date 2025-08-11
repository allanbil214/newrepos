using System;
using System.Collections.Generic;
using System.Linq;

#region Classes
public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; } = true;
}

public class BorrowRecord
{
    public string BookId { get; set; }
    public string UserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; } = false;
}
#endregion

#region User interfaces and it's classes
public interface IUser
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int BorrowingLimit { get; }
    public int BorrowingDays { get; }
}

public class Student : IUser
{
    private string _id;
    private string _name;
    private string _email;

    public int BorrowingLimit => 3;
    public int BorrowingDays => 14;

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Email { get => _email; set => _email = value; }
}

public class Faculty : IUser
{
    private string _id;
    private string _name;
    private string _email;

    public int BorrowingLimit => 10;
    public int BorrowingDays => 30;

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Email { get => _email; set => _email = value; }
}

public class Guest : IUser
{
    private string _id;
    private string _name;
    private string _email;

    public int BorrowingLimit => 1;
    public int BorrowingDays => 7;

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Email { get => _email; set => _email = value; }
}
#endregion

#region System interfaces
public interface INotificationSender
{
    void SendNotification(string recipient, string message);
}

public interface IReportGenerator
{
    void GenerateOverdueReport(List<BorrowRecord> overdueRecords, List<IUser> users, List<Book> books);
}

public interface ILibraryRepository
{
    void AddBook(Book book);
    Book GetBook(string bookId);
    void AddUser(IUser user);
    IUser GetUser(string userId);
    void AddBorrowRecord(BorrowRecord record);
    List<BorrowRecord> GetActiveBorrowRecords();
    List<BorrowRecord> GetOverdueRecords();
    List<BorrowRecord> GetUserBorrowRecords(string userId);
}
#endregion

#region System classes
public class EmailNotificationSender : INotificationSender
{
    public void SendNotification(string recipient, string message)
    {
        Console.WriteLine($"📧 EMAIL to {recipient}: {message}");
    }
}

public class SmsNotificationSender : INotificationSender
{
    public void SendNotification(string recipient, string message)
    {
        Console.WriteLine($"📱 SMS to {recipient}: {message}");
    }
}

public class ConsoleReportGenerator : IReportGenerator
{
    public void GenerateOverdueReport(List<BorrowRecord> overdueRecords, List<IUser> users, List<Book> books)
    {
        Console.WriteLine("\n📊 OVERDUE BOOKS REPORT");
        Console.WriteLine("========================");

        if (!overdueRecords.Any())
        {
            Console.WriteLine("No overdue books found.");
            return;
        }

        foreach (var record in overdueRecords)
        {
            var user = users.FirstOrDefault(u => u.Id == record.UserId);
            var book = books.FirstOrDefault(b => b.Id == record.BookId);
            var daysOverdue = (DateTime.Now - record.DueDate).Days;

            Console.WriteLine($"• {book?.Title} borrowed by {user?.Name}");
            Console.WriteLine($"  Due: {record.DueDate:yyyy-MM-dd} ({daysOverdue} days overdue)");
        }
    }
}

public class InMemoryLibraryRepository : ILibraryRepository
{
    private readonly List<Book> _books = new();
    private readonly List<IUser> _users = new();
    private readonly List<BorrowRecord> _borrowRecords = new();

    public void AddBook(Book book) => _books.Add(book);
    public Book GetBook(string bookId) => _books.FirstOrDefault(b => b.Id == bookId);
    public void AddUser(IUser user) => _users.Add(user);
    public IUser GetUser(string userId) => _users.FirstOrDefault(u => u.Id == userId);
    public void AddBorrowRecord(BorrowRecord record) => _borrowRecords.Add(record);
    public List<BorrowRecord> GetActiveBorrowRecords() => _borrowRecords.Where(r => !r.IsReturned).ToList();
    public List<BorrowRecord> GetOverdueRecords() => _borrowRecords.Where(r => !r.IsReturned && r.DueDate < DateTime.Now).ToList();
    public List<BorrowRecord> GetUserBorrowRecords(string userId) => _borrowRecords.Where(r => r.UserId == userId && !r.IsReturned).ToList();
}

#endregion

#region Main system
public class LibraryService
{
    private readonly ILibraryRepository _repository;
    private readonly INotificationSender _notificationSender;
    private readonly IReportGenerator _reportGenerator;

    public LibraryService(ILibraryRepository repository, INotificationSender notificationSender, IReportGenerator reportGenerator)
    {
        _repository = repository;
        _notificationSender = notificationSender;
        _reportGenerator = reportGenerator;
    }

    public bool BorrowBook(string userId, string bookId)
    {
        var user = _repository.GetUser(userId);
        var book = _repository.GetBook(bookId);

        if (user == null || book == null || !book.IsAvailable)
        {
            Console.WriteLine("❌ Cannot borrow book: User not found, book not found, or book unavailable.");
            return false;
        }

        var userBorrowRecords = _repository.GetUserBorrowRecords(userId);
        if (userBorrowRecords.Count >= user.BorrowingLimit)
        {
            Console.WriteLine($"❌ Cannot borrow book: {user.Name} has reached borrowing limit of {user.BorrowingLimit} books.");
            return false;
        }

        var borrowRecord = new BorrowRecord
        {
            BookId = bookId,
            UserId = userId,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(user.BorrowingDays)
        };

        _repository.AddBorrowRecord(borrowRecord);
        book.IsAvailable = false;

        Console.WriteLine($"✅ {user.Name} borrowed '{book.Title}'. Due date: {borrowRecord.DueDate:yyyy-MM-dd}");
        return true;
    }

    public bool ReturnBook(string userId, string bookId)
    {
        var user = _repository.GetUser(userId);
        var book = _repository.GetBook(bookId);
        var borrowRecord = _repository.GetUserBorrowRecords(userId)
            .FirstOrDefault(r => r.BookId == bookId);

        if (user == null || book == null || borrowRecord == null)
        {
            Console.WriteLine("❌ Cannot return book: Invalid user, book, or no active borrow record found.");
            return false;
        }

        borrowRecord.IsReturned = true;
        book.IsAvailable = true;

        Console.WriteLine($"✅ {user.Name} returned '{book.Title}'.");
        return true;
    }

    public void SendDueNotifications()
    {
        var activeRecords = _repository.GetActiveBorrowRecords();
        var dueSoonRecords = activeRecords.Where(r =>
            r.DueDate.Date == DateTime.Now.Date.AddDays(1) || // Due tomorrow
            r.DueDate.Date == DateTime.Now.Date // Due today
        ).ToList();

        Console.WriteLine($"\n📢 Sending {dueSoonRecords.Count} due notifications...");

        foreach (var record in dueSoonRecords)
        {
            var user = _repository.GetUser(record.UserId);
            if (user != null)
            {
                var message = $"Book due on {record.DueDate:yyyy-MM-dd}";
                _notificationSender.SendNotification(user.Email, message);
            }
        }
    }

    public void GenerateOverdueReport()
    {
        var overdueRecords = _repository.GetOverdueRecords();
        var allUsers = _repository.GetActiveBorrowRecords().Select(r => _repository.GetUser(r.UserId)).Where(u => u != null).ToList();
        var allBooks = _repository.GetActiveBorrowRecords().Select(r => _repository.GetBook(r.BookId)).Where(b => b != null).ToList();

        _reportGenerator.GenerateOverdueReport(overdueRecords, allUsers, allBooks);
    }
}
#endregion
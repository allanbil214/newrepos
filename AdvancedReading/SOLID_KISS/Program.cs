using System;
using System.IO;

namespace Exercises
{
    public class Program // exercise bonus (or #6)
    {
        public static void Main()
        {
            Console.WriteLine("🏛️  SOLID LIBRARY MANAGEMENT SYSTEM");
            Console.WriteLine("====================================\n");

            // Setup dependencies (DIP in action - we can easily swap implementations)
            var repository = new InMemoryLibraryRepository();
            var notificationSender = new EmailNotificationSender(); // Could easily switch to SMS
            var reportGenerator = new ConsoleReportGenerator();
            var libraryService = new LibraryService(repository, notificationSender, reportGenerator);

            // Add sample data
            SetupSampleData(repository);

            // Demo the system
            DemoLibraryOperations(libraryService);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void SetupSampleData(ILibraryRepository repository)
        {
            // Add books
            repository.AddBook(new Book { Id = "B001", Title = "Clean Code", Author = "Robert Martin" });
            repository.AddBook(new Book { Id = "B002", Title = "Design Patterns", Author = "Gang of Four" });
            repository.AddBook(new Book { Id = "B003", Title = "Refactoring", Author = "Martin Fowler" });

            // Add users (LSP - all User subtypes work interchangeably)
            repository.AddUser(new Student { Id = "S001", Name = "Alice Johnson", Email = "alice@email.com" });
            repository.AddUser(new Faculty { Id = "F001", Name = "Dr. Smith", Email = "smith@email.com" });
            repository.AddUser(new Guest { Id = "G001", Name = "Bob Wilson", Email = "bob@email.com" });

            Console.WriteLine("📚 Sample data loaded successfully!\n");
        }

        private static void DemoLibraryOperations(LibraryService libraryService)
        {
            Console.WriteLine("🔄 DEMO: Library Operations");
            Console.WriteLine("---------------------------");

            // Test borrowing with different user types (OCP & LSP)
            libraryService.BorrowBook("S001", "B001"); // Student
            libraryService.BorrowBook("F001", "B002"); // Faculty  
            libraryService.BorrowBook("G001", "B003"); // Guest

            // Test borrowing limits
            Console.WriteLine("\n🔄 Testing borrowing limits:");
            libraryService.BorrowBook("G001", "B001"); // Guest trying to borrow 2nd book (should fail)

            // Test returning books
            Console.WriteLine("\n🔄 Testing book returns:");
            libraryService.ReturnBook("S001", "B001");

            // Test notifications (ISP & DIP)
            libraryService.SendDueNotifications();

            // Test reports (ISP & DIP)
            libraryService.GenerateOverdueReport();

            Console.WriteLine("\n✨ All SOLID principles demonstrated!");
            Console.WriteLine("\n📋 SOLID PRINCIPLES SHOWN:");
            Console.WriteLine("• SRP: Each class has single responsibility");
            Console.WriteLine("• OCP: Easy to add new user types without modification");
            Console.WriteLine("• LSP: All User subclasses work interchangeably");
            Console.WriteLine("• ISP: Focused interfaces (notification, reporting, repository)");
            Console.WriteLine("• DIP: LibraryService depends on abstractions, not concretions");
        }
    }
}
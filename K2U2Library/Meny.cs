using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using K2U2Library;

public class Meny
{
    private bool running = true;

    public void Start()
    {
        while (running)
        {
            Console.WriteLine("\n--- Bibliotekssystem ---");
            Console.WriteLine("1. Registrera ny bok");
            Console.WriteLine("2. Registrera ny medlem");
            Console.WriteLine("3. Registrera lån");
            Console.WriteLine("4. Registrera återlämning");
            Console.WriteLine("5. Visa alla aktiva lån");
            Console.WriteLine("6. Sök efter böcker");
            Console.WriteLine("7. Visa alla medlemmar");
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj ett alternativ: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": RegisterBook(); break;
                case "2": RegisterMember(); break;
                case "3": RegisterLoan(); break;
                case "4": RegisterReturn(); break;
                case "5": ShowActiveLoans(); break;
                case "6": SearchBooks(); break;
                case "7": ShowAllMembers(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Ogiltigt val."); break;
            }
        }
    }

    private void RegisterBook()
    {
        using var db = new K2u2library2DbContext();
        Console.Write("Titel: ");
        var title = Console.ReadLine();
        Console.Write("Författare: ");
        var author = Console.ReadLine();
        Console.Write("Genre: ");
        var genre = Console.ReadLine();

        var book = new Book { Title = title, Author = author, Genre = genre };
        db.Books.Add(book);
        db.SaveChanges();
        Console.WriteLine("Bok registrerad!");
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
        Console.Clear();

    }

    private void RegisterMember()
    {
        using var db = new K2u2library2DbContext();
        Console.Write("Förnamn: ");
        var firstName = Console.ReadLine();
        Console.Write("Efternamn: ");
        var lastName = Console.ReadLine();
        Console.Write("Email: ");
        var email = Console.ReadLine();

        var member = new Member { FirstName = firstName, LastName = lastName, Email = email };
        db.Members.Add(member);
        db.SaveChanges();
        Console.WriteLine("Medlem registrerad!");
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
        Console.Clear();

    }

    private void RegisterLoan()
    {
        using var db = new K2u2library2DbContext();
        Console.Write("Medlems-ID: ");
        int memberId = int.Parse(Console.ReadLine());
        Console.Write("Bok-ID: ");
        int bookId = int.Parse(Console.ReadLine());

        var loan = new Loan
        {
            LoanDate = DateOnly.FromDateTime(DateTime.Now),
            Status = "Aktiv",
            FkmemberId = memberId,
            FkbookId = bookId
        };

        db.Loans.Add(loan);
        db.SaveChanges();
        Console.WriteLine("Lån registrerat!");
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
        Console.Clear();

    }

    private void RegisterReturn()
    {
        using var db = new K2u2library2DbContext();
        Console.Write("Låne-ID: ");
        int loanId = int.Parse(Console.ReadLine());

        var loan = db.Loans.FirstOrDefault(l => l.LoanId == loanId && l.Status == "Aktiv");
        if (loan != null)
        {
            loan.Status = "Återlämnad";
            loan.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            db.SaveChanges();
            Console.WriteLine("Återlämning registrerad!");

        }
        else
        {
            Console.WriteLine("Inget aktivt lån hittades med det ID:t.");

        }
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
    }

    private void ShowActiveLoans()
    {
        using var db = new K2u2library2DbContext();
        var loans = db.Loans
            .Where(l => l.Status == "Aktiv")
            .Include(l => l.Fkmember)
            .Include(l => l.Fkbook)
            .ToList();

        foreach (var loan in loans)
        {
            Console.WriteLine($"Lån-ID {loan.LoanId}: {loan.Fkmember.FirstName} {loan.Fkmember.LastName} lånar '{loan.Fkbook.Title}' sedan {loan.LoanDate}\n");
           
        }
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
        Console.Clear();
    }

    private void SearchBooks()
    {
        using var db = new K2u2library2DbContext();
        Console.Write("Sök titel/genre/författare: ");
        var query = Console.ReadLine();

        var books = db.Books
            .Where(b => b.Title.Contains(query) || b.Author.Contains(query) || b.Genre.Contains(query))
            .ToList();

        if (books.Any())
        {
            foreach (var b in books)
                Console.WriteLine($"{b.BookId}: {b.Title} av {b.Author} ({b.Genre})\n");
        }
        else
        {
            Console.WriteLine("⚠️ Inga böcker hittades.");
        }
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
    }

    private void ShowAllMembers()
    {
        using var db = new K2u2library2DbContext();

        var members = db.Members.ToList();

        Console.Clear();
        Console.WriteLine("Alla medlemmar:\n");

        foreach (var member in members)
        {
            Console.WriteLine($"Medlems-ID: {member.MemberId}, {member.FirstName} {member.LastName}");
        }

        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
        Console.Clear();
    }
}


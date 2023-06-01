using System;
using ConsoleNoteboard;

Noteboard noteboard = new Noteboard();

while (true)
{
    Console.Clear();

    Console.WriteLine("==== Notepad Menu ====");
    Console.WriteLine("1. Add a note");
    Console.WriteLine("2. Remove a note");
    Console.WriteLine("3. Edit a note");
    Console.WriteLine("4. Search notes");
    Console.WriteLine("5. Display all notes");
    Console.WriteLine("6. Exit");

    Console.WriteLine("\nEnter your choice (1-6):");
    Console.Write("> ");
    string choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==== Add a Note ====");
            Console.ResetColor();
            noteboard.AddNote();
            break;
        case "2":
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==== Remove a Note ====");
            Console.ResetColor();
            noteboard.RemoveNote();
            break;
        case "3":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==== Edit a Note ====");
            Console.ResetColor();
            noteboard.EditNote();
            break;
        case "4":
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==== Search Notes ====");
            Console.ResetColor();
            noteboard.SearchNotes();
            break;
        case "5":
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Display All Notes ====");
            Console.ResetColor();
            noteboard.DisplayNotes();
            break;
        case "6":
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Exiting the program...");
            Console.ResetColor();
            return;
        default:
            Console.WriteLine("Invalid choice! Please try again");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNoteboard
{
    internal class Noteboard
    {
        private List<Note> notes = new List<Note>();
        private string notesFilePath = "notes.txt";

        public void AddNote()
        {
            Console.WriteLine("Enter note title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter note content:");
            string content = Console.ReadLine();

            Console.WriteLine("Do you want to set a password for this note? (Y/N)");
            string choice = Console.ReadLine();

            Note note;

            if (choice.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                note = new ProtectedNote { Title = title, Content = content, Password = password };
            }
            else
            {
                note = new Note { Title = title, Content = content };
            }

            note.CreationTime = DateTime.Now;

            notes.Add(note);

            Console.WriteLine("Note added successfully!");
        }


        public void RemoveNote()
        {
            Console.WriteLine("Enter note title to remove:");
            string title = Console.ReadLine();

            Note noteToRemove = notes.Find(note => note.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (noteToRemove != null)
            {
                notes.Remove(noteToRemove);
                Console.WriteLine("Note removed successfully!");
            }
            else
            {
                Console.WriteLine("Note not found!");
            }
        }
        public void EditNote()
        {
            Console.WriteLine("Enter note title to edit:");
            string title = Console.ReadLine();

            Note noteToEdit = notes.Find(note => note.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (noteToEdit != null)
            {
                Console.WriteLine("Enter new note content:");
                string content = Console.ReadLine();

                noteToEdit.Content = content;

                Console.WriteLine("Note edited successfully!");
            }
            else
            {
                Console.WriteLine("Note not found!");
            }
        }
        public void DisplayNotes()
        {
            Console.WriteLine("Notes:");
            if (notes.Count > 0)
            {
                for (int i = 0; i < notes.Count; i++)
                {
                    Note note = notes[i];
                    Console.WriteLine($"{i + 1}. Title: {note.Title} - Created: {note.CreationTime}");
                }

                Console.WriteLine("Enter the number of the note to view its content:");
                if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= notes.Count)
                {
                    Note selectedNote = notes[selectedIndex - 1];

                    if (selectedNote is ProtectedNote protectedNote)
                    {
                        Console.WriteLine("Enter the password to view the note content:");
                        string password = Console.ReadLine();

                        if (password == protectedNote.Password)
                        {
                            Console.WriteLine($"Title: {selectedNote.Title}");
                            Console.WriteLine($"Content: {selectedNote.Content}");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password. Access denied!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Title: {selectedNote.Title}");
                        Console.WriteLine($"Content: {selectedNote.Content}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection!");
                }
            }
            else
            {
                Console.WriteLine("No notes found!");
            }
        }


        public void SearchNotes()
        {
            Console.WriteLine("Enter search keyword:");
            string keyword = Console.ReadLine();

            List<Note> matchingNotes = notes.FindAll(note =>
                note.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                note.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Matching Notes:");
            if (matchingNotes.Count > 0)
            {
                foreach (Note note in matchingNotes)
                {
                    Console.WriteLine("Title: " + note.Title);
                    Console.WriteLine("Content: " + note.Content);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No matching notes found!");
            }
        }
        private void LoadNotes()
        {
            if (File.Exists(notesFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(notesFilePath);

                    foreach (string line in lines)
                    {
                        string[] noteData = line.Split('|');
                        if (noteData.Length == 4)
                        {
                            Note note;

                            if (!string.IsNullOrEmpty(noteData[3]))
                            {
                                note = new ProtectedNote
                                {
                                    Title = noteData[0],
                                    Content = noteData[1],
                                    CreationTime = DateTime.Parse(noteData[2]),
                                    Password = noteData[3]
                                };
                            }
                            else
                            {
                                note = new Note
                                {
                                    Title = noteData[0],
                                    Content = noteData[1],
                                    CreationTime = DateTime.Parse(noteData[2])
                                };
                            }

                            notes.Add(note);
                        }
                    }


                    Console.WriteLine("Notes loaded successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred while loading notes: " + ex.Message);
                }
            }
        }
        private void SaveNotes()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(notesFilePath))
                {
                    foreach (Note note in notes)
                    {
                        string noteLine;

                        if (note is ProtectedNote protectedNote)
                        {
                            noteLine = $"{note.Title}|{note.Content}|{note.CreationTime}|{protectedNote.Password}";
                        }
                        else
                        {
                            noteLine = $"{note.Title}|{note.Content}|{note.CreationTime}|";
                        }

                        writer.WriteLine(noteLine);
                    }
                }

                Console.WriteLine("Notes saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while saving notes: " + ex.Message);
            }
        }


    }
}

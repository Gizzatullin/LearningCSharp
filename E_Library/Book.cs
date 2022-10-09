using System;

namespace E_Library
{
    /// <summary>
    /// Опредение параметров книги.
    /// </summary>
    internal class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public string Genre { get; private set; }

        public string FileNameBook { get; private set; }

        public Book(int id, string title, string author, string description, string genre, string filenamebook)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.Description = description; 
            this.Genre = genre;
            this.FileNameBook = filenamebook;
        }
               
        public void SetId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{Id} \t {Title} \t {Author} \t {Description} \t {Genre} \t {FileNameBook}";
        }

    }
}

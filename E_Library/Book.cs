using System;

namespace E_Library
{
    /// <summary>
    /// Опредение параметров книги.
    /// </summary>
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }

        public string FileNameBook { get; set; }

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

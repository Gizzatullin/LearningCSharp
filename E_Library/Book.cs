namespace E_Library
{
    /// <summary>
    /// Опредение параметров книги.
    /// </summary>
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public enum Genre
        {
            Авангардная_литература,
            Боевик,
            Детектив,
            Исторический_роман,
            Любовный_роман,
            Мистика,
            Приключения,
            Триллер_ужасы,
            Фантастика,
            Фэнтези_сказки
        }
        public Genre GenreB { get; }

        public string FileNameBook { get; set }

        public Book(string title, string author, string description, Genre genre, string filenamebook)
        {
            this.Title = title;
            this.Author = author;
            this.Description = description; 
            this.GenreB = genre;
            this.FileNameBook = filenamebook;
        }
        
    }
}

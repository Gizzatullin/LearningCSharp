using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static E_Library.Book;

namespace E_Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            
            InterfaceConsoleBot(library);

        }

      
        /// <summary>
        /// Взаимодейстиве с пользователем (ввод-вывод данных).
        /// </summary>
        /// <param name="library"></param>
        static void InterfaceConsoleBot(Library library)
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine();
                string allCommands = " 1 - Вывести все книги \n 2 - Добавить новую книгу \n 3 - Корректировать данные о книге \n" +
                    " 4 - Удалить книгу \n 5 - Выход из программы \n------------------------------------------";

                Console.WriteLine(allCommands);

                string inputCommandStr = Console.ReadLine();
                Console.WriteLine();

                int inputCommand = GetIntFromString(inputCommandStr);

                switch (inputCommand)
                {
                    case 1:
                        {
                            var allBooks = library.ReadfromFile();
                            if (allBooks.Count == 0) Console.WriteLine("В библиотеке пока нет книг.");

                            foreach (var book in allBooks) Console.WriteLine(book);
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите название:");
                            string title = Console.ReadLine();

                            Console.Write("Введите автора:");
                            string author = Console.ReadLine();

                            Console.Write("Введите описание:");
                            string description = Console.ReadLine();

                            string recommendedGenre = "Рекомендуемый список жанров:\n" +
                                                      "\tАвангардная_литература,\n" +
                                                      "\tБоевик,\n" +
                                                      "\tДетектив,\n" +
                                                      "\tИсторический_роман,\n" +
                                                      "\tЛюбовный_роман,\n" +
                                                      "\tМистика,\n" +
                                                      "\tПриключения,\n" +
                                                      "\tТриллер_ужасы,\n" +
                                                      "\tФантастика,\n" +
                                                      "\tФэнтези_сказки.";

                            Console.WriteLine(recommendedGenre);
                            Console.Write("Введите жанр:");
                            string genre = Console.ReadLine();

                            Console.Write("Введите название прикреплённого файла:");
                            string filenamebook = Console.ReadLine();

                            int id = 0;
                            Book newBook = new Book(id, title, author, description, genre, filenamebook);
                            bool FlagCoorect = false;
                            library.SavetoFile(newBook, FlagCoorect);

                            Console.WriteLine("Добавление книги прошло успешно.");
                            break;
                        }
                    case 3:
                        {
                            var allBooks = library.ReadfromFile();
                            if (allBooks.Count == 0) Console.WriteLine("В библиотеке пока нет книг.");
                            else
                            {
                                Console.Write("Введите ID книги:");
                                string idStr = Console.ReadLine();
                                int id = GetIntFromString(idStr);

                                if (id == 0) Console.WriteLine("Нет такого ID.");
                                else
                                {
                                    Console.Write("Введите название:");
                                    string title = Console.ReadLine();

                                    Console.Write("Введите автора:");
                                    string author = Console.ReadLine();

                                    Console.Write("Введите описание:");
                                    string description = Console.ReadLine();
                                                                        
                                    Console.Write("Введите жанр:");
                                    string genre = Console.ReadLine();

                                    Console.Write("Введите название прикреплённого файла:");
                                    string filenamebook = Console.ReadLine();

                                    library.DeletefromFile(id);

                                    Book newBook = new Book(id, title, author, description, genre, filenamebook);
                                    bool FlagCorrect = true;
                                    library.SavetoFile(newBook, FlagCorrect);
                                    
                                    Console.WriteLine("Корректировка книги прошла успешно.");
                                    break;
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            var allBooks = library.ReadfromFile();
                            if (allBooks.Count == 0) Console.WriteLine("В библиотеке пока нет книг.");
                            else
                            {
                                Console.Write("Введите ID книги:");
                                string idStr = Console.ReadLine();
                                int id = GetIntFromString(idStr);

                                if (id == 0) Console.WriteLine("Нет такого ID.");
                                else
                                {
                                    bool result = library.DeletefromFile(id);
                                    if (result) Console.WriteLine("Удаление книги прошло успешно.");
                                    else Console.WriteLine("Ошибка.");
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            isWork = false;
                            Console.WriteLine("Конец программы.");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Нет такой команды.");
                            break;
                        }
                }
            }
        }

        static int GetIntFromString(string inputStr)
        {
            int input = 0;

            try
            {
                input = int.Parse(inputStr);
            }
            catch (FormatException)
            {
                Console.WriteLine("Не верный формат введённых данных!");
            }
            return input;
        }


    }
}

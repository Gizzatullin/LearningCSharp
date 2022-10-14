using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Nlog;
using Telegram.Bot.Types.ReplyMarkups;

namespace E_Library
{
    internal class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5528529213:AAEs9MALLD3IpShMnSjrAIgzN43obnoSMng");


        static void Main(string[] args)
        {
            Log("Начало работы программы.");
            Library library = new Library();

            Console.WriteLine("Выберите интерфейс: 1 - БОТ-ТЕЛЕГРАМ, 2 - КОНСОЛЬ.");
            string choicemenu = Console.ReadLine();
            int inputmenu = GetIntFromString(choicemenu);

            if (inputmenu == 1) InterfaceBot(library);
            else InterfaceConsole(library);
        }


        /// <summary>
        /// Связь с Телеграм-Ботом.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                ReplyKeyboardMarkup keyboard = new(new[]
                {
                    new KeyboardButton[] { "Вывести все книги", "Добавить новую книгу" },
                    new KeyboardButton[] { "Корректировать данные о книге", "Удалить книгу", "Выход из программы" }
                })
                { ResizeKeyboard = true };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду", replyMarkup: keyboard);
                return;




                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                if (message.Text.ToLower() == "/finish")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Пока, пока! До новых встреч!");
                    return;
                }

                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        /// <summary>
        /// Обработка ошибок при работе Телеграм-Бота.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }



        /// <summary>
        /// Определение файла для логирования.
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            System.IO.File.AppendAllText("E_Library_log.txt", message);
        }

       
        /// <summary>
        /// Взаимодейстиве с пользователем (ввод-вывод данных) через БОТ-ТЕЛЕГРАМ.
        /// </summary>
        /// <param name="library"></param>
        static void InterfaceBot(Library library)
        {
            bool isWork = true;

            string tableHeader = "НОМЕР\tНАЗВАНИЕ\tАВТОР\t\tОПИСАНИЕ\t\t\tЖАНР\t\t\tИМЯ ФАЙЛА";

            while (isWork)
            {
                Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = { }, // Получение всех обновлённых данных.
                };
                bot.StartReceiving(HandleUpdateAsync,HandleErrorAsync,receiverOptions,cancellationToken);
                
                Console.ReadLine();
                cts.Cancel();
                isWork = false;
                    


                /*
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

                            Console.WriteLine(tableHeader);
                            foreach (var book in allBooks) Console.WriteLine(book);

                            Console.WriteLine();
                            Console.WriteLine("Вывести отсортированный список? : 0 - нет, 1 - по названию, 2 - по автору, 3 - по жанру");
                            string inputCommandSortStr = Console.ReadLine();
                            Console.WriteLine();
                            int inputCommandSort = GetIntFromString(inputCommandSortStr);
                            switch (inputCommandSort)
                            {
                                case 0: break;
                                case 1:
                                    {
                                        var sortedBookTitle = allBooks.OrderBy(b => b.Title);
                                        Console.WriteLine("Сортировка книг по названию:");
                                        Console.WriteLine(tableHeader);
                                        foreach (var b in sortedBookTitle) Console.WriteLine(b);
                                        break;
                                    }
                                case 2:
                                    {
                                        var sortedBookAuthor = allBooks.OrderBy(b => b.Author);
                                        Console.WriteLine("Сортировка книг по автору:");
                                        Console.WriteLine(tableHeader);
                                        foreach (var b in sortedBookAuthor) Console.WriteLine(b);
                                        break;
                                    }
                                case 3:
                                    {
                                        var sortedBookGenre = allBooks.OrderBy(b => b.Genre);
                                        Console.WriteLine("Сортировка книг по жанру:");
                                        Console.WriteLine(tableHeader);
                                        foreach (var b in sortedBookGenre) Console.WriteLine(b);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Нет такой команды.");
                                    break;
                            }
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

                                    bool result = library.CorrectBookInfo(id, title, author, description, genre, filenamebook);

                                    if (result) Console.WriteLine("Корректировка данных о книге прошло успешно.");
                                    else Console.WriteLine("Ошибка.");
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
                */
            }
        }


        /// <summary>
        /// Взаимодейстиве с пользователем (ввод-вывод данных) через КОНСОЛЬ.
        /// </summary>
        /// <param name="library"></param>
        static void InterfaceConsole(Library library)
        {
            bool isWork = true;

            string tableHeader = "НОМЕР\tНАЗВАНИЕ\tАВТОР\t\tОПИСАНИЕ\t\t\tЖАНР\t\t\tИМЯ ФАЙЛА";

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

                            Console.WriteLine(tableHeader);
                            foreach (var book in allBooks) Console.WriteLine(book);

                            Console.WriteLine();
                            Console.WriteLine("Вывести отсортированный список? : 0 - нет, 1 - по названию, 2 - по автору, 3 - по жанру");
                            string inputCommandSortStr = Console.ReadLine();
                            Console.WriteLine();
                            int inputCommandSort = GetIntFromString(inputCommandSortStr);
                            switch (inputCommandSort)
                            {   case 0: break;
                                case 1:
                                    {
                                        var sortedBookTitle = allBooks.OrderBy(b => b.Title);
                                        Console.WriteLine("Сортировка книг по названию:");
                                        Console.WriteLine(tableHeader);
                                        foreach (var b in sortedBookTitle) Console.WriteLine(b);
                                        break;
                                    }
                                case 2:
                                    {
                                        var sortedBookAuthor = allBooks.OrderBy(b => b.Author);
                                        Console.WriteLine("Сортировка книг по автору:");
                                        Console.WriteLine(tableHeader);
                                        foreach (var b in sortedBookAuthor) Console.WriteLine(b);
                                        break;
                                    }
                                case 3:
                                    {
                                        var sortedBookGenre = allBooks.OrderBy(b => b.Genre);
                                        Console.WriteLine("Сортировка книг по жанру:");
                                        Console.WriteLine(tableHeader);
                                        foreach (var b in sortedBookGenre) Console.WriteLine(b);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Нет такой команды.");
                                    break;
                            }
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

                                    bool result=library.CorrectBookInfo(id, title, author, description, genre, filenamebook);

                                    if (result) Console.WriteLine("Корректировка данных о книге прошло успешно.");
                                    else Console.WriteLine("Ошибка.");
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

        /// <summary>
        /// Проверка на корректный ввод данных (int).
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static int GetIntFromString(string inputStr)
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

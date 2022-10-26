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
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using File = System.IO.File;
using NLog;

namespace E_Library
{
    public class Program
    {
        static Library library = new Library();

        static ITelegramBotClient botClient = new TelegramBotClient("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                
        static bool sortList = false;

        static string mesList;

        static int addBookFlag = 0;
        static string title;
        static string author;
        static string description;
        static string genre;
        static string filenamebook;
              

        static int correctBookFlag = 0;
        static int idCorrect;

        static bool delBookFlag = false;

        static bool downloadBookFlag = false;

        static bool uploadBookFlag = false;
        static int idUpload = 0;

        private static Logger logger = LogManager.GetCurrentClassLogger();
                      
        static void Main(string[] args)
        {

            logger.Info("Запуск работы программы.");

            Console.WriteLine("Выберите интерфейс: 1 - БОТ-ТЕЛЕГРАМ, 2 - КОНСОЛЬ.");
            string choicemenu = Console.ReadLine();
            int inputmenu = GetIntFromString(choicemenu);

            switch(inputmenu)
            {
                case 1: { logger.Info("Выбран интерфейс <Телеграм-Бот>."); InterfaceBot(); break; }
                case 2: { logger.Info("Выбран интерфейс <Консоль>."); InterfaceConsole();  break; }
                default: { Console.WriteLine("Не верная команда. Нажмите любую клавишу для выхода из программы.");
                           logger.Error("Интерфейс не выбран. Выход из программы."); Console.ReadLine(); break; }
            }
           
            logger.Info("Конец работы программы.");

        }
        

        /// <summary>
        /// Обновление информации с Телеграм-Ботом.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                logger.Info($"Поступило сообщение от пользователя {update.Message.From.Username} - {update.Message.Text}.");
                await HandleMessage(botClient, update.Message);
                return;
            }
        }


        /// <summary>
        /// Обмен сообщениями с Телеграм-ботом.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            string fileNameUser = message.From.Username + ".txt";
            
            string pathBook = Path.Combine(Environment.CurrentDirectory + $@"\BookFile\{fileNameUser}\");
            
            if (!Directory.Exists(pathBook))
            {
                Directory.CreateDirectory(pathBook);
            }
                    
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "Вывести все книги", "Добавить новую книгу" },
                new KeyboardButton[] { "Корректировать данные о книге", "Удалить книгу" },
                new KeyboardButton[] { "Скачать файл книги", "Загрузить файл книги" }
            })
            { ResizeKeyboard = true };


            if (addBookFlag != 0)
            {
                sortList = false;
                delBookFlag = false;
                correctBookFlag = 0;
                downloadBookFlag = false;
                uploadBookFlag = false;

                switch (addBookFlag)
                {
                    case 1:
                        {
                            title = message.Text;
                            addBookFlag++;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите автора:");
                            break;
                        }
                    case 2:
                        {
                            author = message.Text;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите описание:");
                            addBookFlag++;
                            break;
                        }
                    case 3:
                        {
                            description = message.Text;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Рекомендуемый список жанров:\n" +
                                                                                 "\tАвангардная литература,\n" +
                                                                                 "\tБоевик,\n" +
                                                                                 "\tДетектив,\n" +
                                                                                 "\tИсторический роман,\n" +
                                                                                 "\tЛюбовный роман,\n" +
                                                                                 "\tМистика,\n" +
                                                                                 "\tПриключения,\n" +
                                                                                 "\tТриллер ужасы,\n" +
                                                                                 "\tФантастика,\n" +
                                                                                 "\tФэнтези и сказки.");
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите жанр:");
                            addBookFlag++;
                            break;
                        }
                    case 4:
                        {
                            genre = message.Text;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название прикреплённого файла:");
                            addBookFlag++;
                            break;
                        }
                    case 5:
                        {
                            filenamebook = message.Text;
                            int id = 0;
                            Book newBook = new Book(id, title, author, description, genre, filenamebook);
                            bool FlagCoorect = false;
                            library.SavetoFile(newBook, FlagCoorect, fileNameUser, pathBook);
                            
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Добавление книги прошло успешно.");
                            addBookFlag = 0;
                            break;
                        }                    
                }
            }


            if (correctBookFlag != 0)
            {
                sortList = false;
                delBookFlag = false;
                downloadBookFlag = false;
                uploadBookFlag = false;
                addBookFlag = 0;

                switch (correctBookFlag)
                {
                    case 1:
                        {
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                            string idStr = message.Text;
                            int id = GetIntFromString(idStr);
                            idCorrect = id;

                            if (id == 0)
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Нет такого ID.");
                                correctBookFlag = 0;
                            }
                            else
                            {
                                if (id > allBooks.Count)
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "В библиотеке нет книги с данным ID.");
                                    correctBookFlag = 0;
                                }
                                else
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название:");
                                    correctBookFlag++;
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            title = message.Text;
                            correctBookFlag++;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите автора:");
                            break;
                        }
                    case 3:
                        {
                            author = message.Text;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите описание:");
                            correctBookFlag++;
                            break;
                        }
                    case 4:
                        {
                            description = message.Text;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Рекомендуемый список жанров:\n" +
                                                                                 "\tАвангардная литература,\n" +
                                                                                 "\tБоевик,\n" +
                                                                                 "\tДетектив,\n" +
                                                                                 "\tИсторический роман,\n" +
                                                                                 "\tЛюбовный роман,\n" +
                                                                                 "\tМистика,\n" +
                                                                                 "\tПриключения,\n" +
                                                                                 "\tТриллер ужасы,\n" +
                                                                                 "\tФантастика,\n" +
                                                                                 "\tФэнтези и сказки.");
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите жанр:");
                            correctBookFlag++;
                            break;
                        }
                    case 5:
                        {
                            genre = message.Text;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название прикреплённого файла:");
                            correctBookFlag++;
                            break;
                        }
                    case 6:
                        {
                            filenamebook = message.Text;
                            bool result = library.CorrectBookInfo(idCorrect, title, author, description, genre, filenamebook, fileNameUser, pathBook);

                            if (result) await botClient.SendTextMessageAsync(message.Chat.Id, "Корректировка данных о книге прошло успешно.");
                            else await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка.");

                            correctBookFlag = 0;

                            break;
                        }
                }
            }               
            

            if (delBookFlag == true)
            {
                sortList = false;
                correctBookFlag = 0;
                downloadBookFlag = false;
                uploadBookFlag = false;
                addBookFlag = 0;

                var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                if (allBooks.Count == 0) await botClient.SendTextMessageAsync(message.Chat.Id, "В библиотеке пока нет книг.");
                else
                {
                    string idStr = message.Text;
                    int id = GetIntFromString(idStr);

                    if (id == 0) await botClient.SendTextMessageAsync(message.Chat.Id, "Нет такого ID.");
                    else
                    {
                        bool result = library.DeletefromFile(id, fileNameUser, pathBook);
                        if (result)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Удаление книги прошло успешно.");
                            logger.Info($"Пользователь {message.From.Username} удалил книгу c ID {id}.");
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка.");
                            logger.Error($"Пользователь {message.From.Username} не смог удалить книгу.");
                        }
                    }
                }
                delBookFlag = false;                
            }


            if (downloadBookFlag == true)
            {
                sortList = false;
                correctBookFlag = 0;
                delBookFlag = false;
                uploadBookFlag = false;
                addBookFlag = 0;

                var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                if (allBooks.Count == 0) await botClient.SendTextMessageAsync(message.Chat.Id, "В библиотеке пока нет книг.");
                else
                {
                    string idStr = message.Text;
                    int id = GetIntFromString(idStr);

                    if (id == 0) await botClient.SendTextMessageAsync(message.Chat.Id, "Нет такого ID.");
                    else
                    {
                        if (id <= allBooks.Count)
                        {
                            List<Book> allCurrentBooks = library.ReadfromFile(fileNameUser, pathBook);
                            Book bookForDownload = allCurrentBooks.FirstOrDefault(u => u.Id == id);
                            string fileNameDownload = bookForDownload.FileNameBook;

                            string filePath = Path.Combine(Environment.CurrentDirectory + $@"\BookFile\{fileNameUser}", fileNameDownload);
                            int sExeption = 0;

                            try
                            {
                                var stream = File.Open(filePath, FileMode.Open);
                                stream.Close();
                            }
                            catch (Exception)
                            {
                                sExeption = 1;
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Приносим извенение, но файла данной книги пока нет в библиотеке.");
                                logger.Error($"Пользователь {message.From.Username} не смог скачать файл книги c ID {id}.");
                            }
                            if (sExeption == 0)
                            {
                                var stream = File.Open(filePath, FileMode.Open);
                                await botClient.SendDocumentAsync(message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream, fileNameDownload));
                                stream.Close();
                                logger.Info($"Пользователь {message.From.Username} cкачал успешно файл книги c ID {id}.");
                                sExeption = 0;                                                                
                            }
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка, нет такого ID.");
                        }
                    }
                }
                downloadBookFlag = false;
            }


            if (uploadBookFlag == true)
            {
                sortList = false;
                correctBookFlag = 0;
                delBookFlag = false;
                downloadBookFlag = false;
                addBookFlag = 0;

                var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                if (allBooks.Count == 0) await botClient.SendTextMessageAsync(message.Chat.Id, "В библиотеке пока нет книг.");
                else
                {
                    string idStr = message.Text;
                    int id = GetIntFromString(idStr);

                    if (id == 0) await botClient.SendTextMessageAsync(message.Chat.Id, "Нет такого ID.");
                    else
                    {
                        if (id > allBooks.Count)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "В библиотеке нет книги с данным ID.");
                            idUpload = 0;
                        }
                        else
                        {
                            idUpload = id;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Отправьте файл книги сообщением.");
                        }
                    }
                }
                uploadBookFlag = false;
            }


            if (message.Document != null)
           {
                if (idUpload != 0)
                {
                    int id = idUpload;
                    var document = message.Document;
                    var file = await botClient.GetFileAsync(document.FileId);
                    string fP = Path.Combine(Environment.CurrentDirectory + $@"\BookFile\{fileNameUser}", document.FileName);
                    var fs = new FileStream(fP, FileMode.Create);

                    await botClient.DownloadFileAsync(file.FilePath, fs);


                    List<Book> allCurrentBooks = library.ReadfromFile(fileNameUser, pathBook);
                    Book bookForUpload = allCurrentBooks.FirstOrDefault(u => u.Id == id);
                    bookForUpload.FileNameBook = document.FileName;

                    bool result = library.CorrectBookInfo(bookForUpload.Id, bookForUpload.Title, bookForUpload.Author,
                                                          bookForUpload.Description, bookForUpload.Genre, bookForUpload.FileNameBook, fileNameUser, pathBook);
                    if (result)
                    {
                        logger.Info($"Пользователь {message.From.Username} загрузил файл {document.FileName} в книгу с ID {id}.");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Загрузка файла прошла успешно.");
                        Console.WriteLine($"ID после загрузки = {id}");

                    }
                    else
                    {
                        logger.Error($"Пользователь {message.From.Username} не смог загрузить файл в книгу с ID {id}.");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка.");
                    }
                    
                    idUpload = 0; 
                }
                else
                {
                    logger.Error($"Пользователь {message.From.Username} вместо сообщения направил телеграм-боту документ.");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Для загрузки файла в библиотеку выберите сначала команды <Загрузить файл книги>.");
                }
           }

            switch (message.Text)
            {                
                case "/start":
                    {
                        logger.Info("Программа работает по ветке </start>.");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать в электронную библиотеку.");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Для работы используйте дополнительную клавиатуру.", replyMarkup: keyboard);
                        break;
                    }

                case "Вывести все книги":
                    {
                        logger.Info("Программа работает по ветке - Вывести все книги.");
                        var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                        if (allBooks.Count == 0)
                        {
                            Console.WriteLine("В библиотеке пока нет книг.");
                            await botClient.SendTextMessageAsync(message.Chat.Id, "В библиотеке пока нет книг.");
                        }
                        else
                        {
                            foreach (var b in allBooks)
                            {
                                mesList = mesListForOutput(b);
                                await botClient.SendTextMessageAsync(message.Chat.Id, mesList);
                            }

                            await botClient.SendTextMessageAsync(message.Chat.Id, "При необходимости вывода отсортированного списка выберите:" +
                                                                                  "\n1 - по названию, 2 - по автору, 3 - по жанру");
                            sortList = true;
                        }
                        break;
                    }
                case "1":
                    {
                        logger.Info("Пользователь выбрал команду сортировки книг по названию.");
                        if (sortList != false) 
                        {
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                            var sortedBookTitle = allBooks.OrderBy(b => b.Title);
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Сортировка книг по названию:");
                            
                            foreach (var b in sortedBookTitle)
                            {
                                mesList = mesListForOutput(b);
                                await botClient.SendTextMessageAsync(message.Chat.Id, mesList); ;
                            }
                            sortList = false;
                        }
                        break;
                    }
                case "2":
                    {
                        logger.Info("Пользователь выбрал команду сортировки книг по автору.");
                        if (sortList != false)
                        {
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                            var sortedBookTitle = allBooks.OrderBy(b => b.Author);
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Сортировка книг по автору:");

                            foreach (var b in sortedBookTitle)
                            {
                                mesList = mesListForOutput(b);
                                await botClient.SendTextMessageAsync(message.Chat.Id, mesList); ;
                            }
                            sortList = false;
                        }
                        break;
                    }
                case "3":
                    {
                        logger.Info("Пользователь выбрал команду сортировки книг по жанру.");
                        if (sortList != false)
                        {
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                            var sortedBookTitle = allBooks.OrderBy(b => b.Genre);
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Сортировка книг по жанру:");

                            foreach (var b in sortedBookTitle)
                            {
                                mesList = mesListForOutput(b);
                                await botClient.SendTextMessageAsync(message.Chat.Id, mesList); ;
                            }
                            sortList = false;
                        }
                        break;
                    }
                case "Добавить новую книгу":
                    {
                        logger.Info("Программа работает по ветке - Добавить новую книгу.");
                        addBookFlag = 1;
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название:");
                        break;
                    }

                case "Корректировать данные о книге":
                    {
                        logger.Info("Программа работает по ветке - Корректировать данные о книге.");
                        correctBookFlag = 1;
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ID книги:");
                        break;
                    }

                case "Удалить книгу":
                    {
                        logger.Info("Программа работает по ветке - Удалить книгу.");
                        delBookFlag = true;
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ID книги для удаления:");
                        break;
                    }
                case "Скачать файл книги":
                    {
                        logger.Info("Программа работает по ветке - Скачать файл книги.");
                        downloadBookFlag = true;
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ID книги для скачивания:");
                        break;
                    }
                case "Загрузить файл книги":
                    {
                        logger.Info("Программа работает по ветке - Загрузить файл книги.");
                        uploadBookFlag = true;
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ID книги для загрузки файла в библиотеку:");
                        break;
                    }
            }                   
            return;
        }


        /// <summary>
        /// Вывод книги в чат телеграм-бота.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string mesListForOutput(Book b)
        {
            mesList = $"ID-книги: {b.Id}\n" +
                      $"НАЗВАНИЕ: {b.Title}\n" +
                      $"АВТОР: {b.Author}\n" +
                      $"ОПИСАНИЕ: {b.Description}\n" +
                      $"ЖАНР: {b.Genre}\n" +
                      $"ФАЙЛ: {b.FileNameBook}";
            logger.Info("Выполнен метод <mesListForOutput>");
            return mesList;
        }


        /// <summary>
        /// Обработка ошибок при работе Телеграм-Бота.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception;
            switch (exception)
            { case ApiRequestException apiRequestException:
                 {
                        Console.WriteLine($"Ошибка телеграм API:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}");
                        break;
                 }                        
              default: exception.ToString(); break;
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }


        /// <summary>
        /// Взаимодейстиве с пользователем (ввод-вывод данных) через БОТ-ТЕЛЕГРАМ.
        /// </summary>
        static void InterfaceBot()
        {
            var cts = new CancellationTokenSource();
            
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, 
            };
                                   
            botClient.StartReceiving(HandleUpdateAsync,HandleErrorAsync,receiverOptions,cancellationToken: cts.Token);
                               
            Console.ReadLine();
            
            cts.Cancel();
        }


        /// <summary>
        /// Взаимодейстиве с пользователем (ввод-вывод данных) через КОНСОЛЬ.
        /// </summary>
        static void InterfaceConsole()
        {
            string fileNameUser = "BookLibraryCollection.txt";
            string pathBook = Path.Combine(Environment.CurrentDirectory + $@"\BookFile", fileNameUser);

            bool isWork = true;

            string tableHeader = "НОМЕР\tНАЗВАНИЕ\tАВТОР\tОПИСАНИЕ\tЖАНР\tИМЯ ФАЙЛА";

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
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
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
                            library.SavetoFile(newBook, FlagCoorect, fileNameUser, pathBook);

                            Console.WriteLine("Добавление книги прошло успешно.");
                            break;
                        }
                    case 3:
                        {
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
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

                                    bool result=library.CorrectBookInfo(id, title, author, description, genre, filenamebook, fileNameUser, pathBook);

                                    if (result) Console.WriteLine("Корректировка данных о книге прошло успешно.");
                                    else Console.WriteLine("Ошибка.");
                                    break;
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            var allBooks = library.ReadfromFile(fileNameUser, pathBook);
                            if (allBooks.Count == 0) Console.WriteLine("В библиотеке пока нет книг.");
                            else
                            {
                                Console.Write("Введите ID книги:");
                                string idStr = Console.ReadLine();
                                int id = GetIntFromString(idStr);

                                if (id == 0) Console.WriteLine("Нет такого ID.");
                                else
                                {
                                    bool result = library.DeletefromFile(id, fileNameUser, pathBook);
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
                logger.Error("Выполнен метод <GetIntFromString> при котором выявлен не верный ввод данных.");
            }
            logger.Info("Выполнен метод <GetIntFromString> для преобразования строкового значения в число тип <int>.");
            return input;
        }
    }
}

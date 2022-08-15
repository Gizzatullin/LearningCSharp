using System;

namespace Lesson4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Phonebook phonebookInstance = new Phonebook();
            
            phonebookInstance.ReadAbonentFromFile += DisplayMessageRead;
            phonebookInstance.ReadPhonebook();
            
            Menu(phonebookInstance);
            
            phonebookInstance.WriteAbonentToFile += DisplayMessageWrite;
            phonebookInstance.WritePhonebook(phonebookInstance);
        }
        
        /// <summary>
        /// Метод вывода меню управления телефонной книжкой.
        /// </summary>
        static void Menu(Phonebook phonebookInstance)
        {
            bool FlagExit = false;
            
            do
            {   
                Console.WriteLine("---Выберите действие с телефонной книгой---");
                Console.WriteLine("-                                         -");
                Console.WriteLine("-  1. Создать новую запись.               -");
                Console.WriteLine("-  2. Обновить запись.                    -");
                Console.WriteLine("-  3. Удалить запись.                     -");
                Console.WriteLine("-  4. Выйти из программы.                 -");
                Console.WriteLine("-------------------------------------------");

                ConsoleKeyInfo ChoiceMenu = Console.ReadKey(true);
                switch (ChoiceMenu.Key)
                {
                    case ConsoleKey.NumPad1: Creat(phonebookInstance); break;
                    case ConsoleKey.NumPad2: Update(phonebookInstance); break;
                    case ConsoleKey.NumPad3: Delete(phonebookInstance); break;
                    case ConsoleKey.NumPad4: FlagExit = true; break;
                    default: break;
                }
            }
            while (FlagExit == false);
        }
        
        /// <summary>
        /// Метод для добавления нового абонента.
        /// </summary>
        /// <param name="phonebookInstance"></param>
        static void Creat(Phonebook phonebookInstance)
        {
            Console.WriteLine("\n- Добавляем контакт -");
            
            Console.Write("Введите телефонный номер:");
            String NumberPhone = Console.ReadLine();
            
            Console.Write("Введите имя:");
            String Name = Console.ReadLine();
            
            phonebookInstance.AddAbonent += DisplayMessageGreen;
            phonebookInstance.AddContact(phonebookInstance, NumberPhone, Name);
        }

        /// <summary>
        /// Метод для коррекции телефона абонента.
        /// </summary>
        /// <param name="phonebookInstance"></param>
        static void Update(Phonebook phonebookInstance)
        {
            Console.WriteLine("\n- Обновляем запись -");
            
            Console.Write("Введите имя:");
            String Name = Console.ReadLine();

            phonebookInstance.CorrectContact(phonebookInstance, Name);
        }

        /// <summary>
        /// Метод для удаления абонента.
        /// </summary>
        /// <param name="phonebookInstance"></param>
        static void Delete(Phonebook phonebookInstance)
        {
            Console.WriteLine("\n- Удаляем контакт -");
            
            Console.Write("Введите имя, контакт которого вы желаете удалить:");
            string Name = Console.ReadLine();

            phonebookInstance.RemoveAbonent += DisplayMessageRed;
            phonebookInstance.DelContact(phonebookInstance, Name);

        }

        /// <summary>
        /// Метод вызова события чтения информации из файла.
        /// </summary>
        /// <param name="message"></param>
        public static void DisplayMessageRead()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("СОБЫТИЕ - Произведено чтение данных из файла.");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вызова события записи информации в файл.
        /// </summary>
        /// <param name="message"></param>
        public static void DisplayMessageWrite()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("СОБЫТИЕ - Произведена запись данных в файл.");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вызова события добавления абонента.
        /// </summary>
        /// <param name="message"></param>
        public static void DisplayMessageGreen()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("СОБЫТИЕ - Добавлен абонент в книжку.");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вызова события удаления абонента.
        /// </summary>
        /// <param name="message"></param>
        public static void DisplayMessageRed()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("СОБЫТИЕ - Удалён абонент из книжки.");
            Console.ResetColor();
        }
    }
}
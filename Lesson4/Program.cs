using System;

namespace Lesson4
{
    internal class Program
    {
        public static object message { get; private set; }

        static void Main(string[] args)
        {
            Phonebook phonebookInstance = new Phonebook();
            phonebookInstance.readAbonentFromFile += phonebookInstance.DisplayMessage;
            phonebookInstance.ReadPhonebook();
            
            Menu(phonebookInstance);
            phonebookInstance.writeAbonentToFile += phonebookInstance.DisplayMessage;
            phonebookInstance.WritePhonebook(phonebookInstance);
        }
        
        /// <summary>
        /// Phonebook and control interface output method
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
        /// Calling the Phonebook class to add a contact
        /// </summary>
        /// <param name="phonebookInstance"></param>
        static void Creat(Phonebook phonebookInstance)
        {
            Console.WriteLine("\n- Добавляем контакт -");
            Console.Write("Введите телефонный номер:");
            String NumberPhone = Console.ReadLine();
            Console.Write("Введите имя:");
            String Name = Console.ReadLine();
            phonebookInstance.addAbonent += phonebookInstance.DisplayMessageGreen;
            phonebookInstance.AddContact(phonebookInstance, NumberPhone, Name);
        }

        /// <summary>
        /// Calling the Phonebook class to insert a contact
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
        /// Calling the Phonebook class to remove a contact
        /// </summary>
        /// <param name="phonebookInstance"></param>
        static void Delete(Phonebook phonebookInstance)
        {
            Console.WriteLine("\n- Удаляем контакт -");
            Console.Write("Введите имя, контакт которого вы желаете удалить:");
            string Name = Console.ReadLine();

            phonebookInstance.removeAbonent += phonebookInstance.DisplayMessageRed;
            phonebookInstance.DelContact(phonebookInstance, Name);
        }
    }
}
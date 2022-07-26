using System;
using System.Collections.Generic;
using System.IO;


namespace Lesson4
{
    internal class Program
    {
        const string FileName = "phonebook.txt";

        static void Main(string[] args)
        {
            List<string> Contacts = new List<string>();

            ReadPhonebook(Contacts);
            Menu(Contacts);
            WritePhonebook(Contacts);
        }


        /// <summary>
        /// Method of reading a text file and getting a list of contacts
        /// </summary>
        static void ReadPhonebook(List<string> Contacts)
        {
            string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
            string [] lines = File.ReadAllLines(FilePath);
            
            for (int i = 0; i < lines.Length; i++)
            {   Contacts.Add(lines[i]); 
            }                        
        }

        /// <summary>
        /// Method of writing a contacts in txt-file
        /// </summary>
        /// <param name="Contacts"></param>
        static void WritePhonebook(List<string> Contacts)
        {
            string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
            File.WriteAllLines(FilePath, Contacts);
        }


        /// <summary>
        /// Phonebook and control interface output method
        /// </summary>
        static void Menu(List<string> Contacts)
        {
            bool FlagExit = true;
                        
            do
            {   
                Console.WriteLine("\n-Телефонная книжка- ");
                foreach (string Contact in Contacts) 
                {
                    Console.WriteLine(Contact);
                   }
                Console.WriteLine();
                
                
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
                    case ConsoleKey.NumPad1: Creat(Contacts); break;
                    case ConsoleKey.NumPad2: Update(); break;
                    case ConsoleKey.NumPad3: Delete(Contacts); break;
                    case ConsoleKey.NumPad4: FlagExit = false; break;
                    default: break;
                }
            }
            while (FlagExit == true);
        }

        /// <summary>
        /// Calling the Phonebook class to add a contact
        /// </summary>
        /// <param name="Contacts"></param>
        static void Creat(List<string> Contacts)
        {   Phonebook phonebookInstance = new Phonebook();
            phonebookInstance.AddContact(Contacts);
        }
        
        
        static void Update()
        {   Phonebook phonebookInstance = new Phonebook();
            phonebookInstance.CorrectContact();
        }

        /// <summary>
        /// Calling the Phonebook class to remove a contact
        /// </summary>
        /// <param name="Contacts"></param>
        static void Delete(List<string> Contacts)
        {   Phonebook phonebookInstance = new Phonebook();
            phonebookInstance.DelContact(Contacts);
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
            
            
        }


        static void Menu()
        {
            bool FlagExit = true;
            
            do
            {
                Console.WriteLine("---Выберите действие с телефонной книгой---");
                Console.WriteLine("-                                         -");
                Console.WriteLine("-  1. Создать новую запись.               -");
                Console.WriteLine("-  2. Прочитать все записи.               -");
                Console.WriteLine("-  3. Обновить запись.                    -");
                Console.WriteLine("-  4. Удалить запись.                     -");
                Console.WriteLine("-  5. Выйти из программы.                 -");
                Console.WriteLine("-------------------------------------------");

                ConsoleKeyInfo ChoiceMenu = Console.ReadKey(true);

                switch (ChoiceMenu.Key)
                {
                    case ConsoleKey.NumPad1: Creat(); break;
                    case ConsoleKey.NumPad2: Read(); break;
                    case ConsoleKey.NumPad3: Update(); break;
                    case ConsoleKey.NumPad4: Delete(); break;
                    case ConsoleKey.NumPad5: FlagExit = false; break;
                    default: break;
                }
            }
            while (FlagExit == true);
        }


        static void Creat()
        {

        }

        static void Read()
        {

        }

        static void Update()
        {

        }

        static void Delete()
        {

        }

    }

}
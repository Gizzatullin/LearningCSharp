﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal[] animals = new Animal[3];

            Puppy puppy = new Puppy();
            puppy.Name = "Дружок";

            Kitten kitten = new Kitten();
            kitten.Name = "Пушок";

            Humster humster = new Humster();
            humster.Name = "Хома";

            animals[0] = puppy;
            animals[1] = kitten;
            animals[2] = humster;

            int IndexAnimal = 0;

            Menu();
            Choise(ref IndexAnimal);
            Live(animals[IndexAnimal]);
        }

        /// <summary>
        /// Вывод на экран меню управления.
        /// </summary>
        static void Menu()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            for (int j = 1; j < 14; j++)
            {
                Console.SetCursorPosition(0, j);
                for (int i = 0; i < 60; i++) Console.Write(" ");
            }
            Console.BackgroundColor = ConsoleColor.Gray;
            for (int j = 3; j < 13; j++)
            {
                Console.SetCursorPosition(0, j);
                for (int i = 0; i < 60; i++) Console.Write(" ");
            }
            ClearMessageWindow();
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;    
            Console.SetCursorPosition(15, 1);
            Console.Write("--- Т  А  М  А  Г  О  Ч  И ---");
            Console.SetCursorPosition(1, 13);
            Console.Write("(1)НАПОИТЬ  ");
            Console.SetCursorPosition(15, 13);
            Console.Write("(2)НАКОРМИТЬ");
            Console.SetCursorPosition(30, 13);
            Console.Write("(3)ПОИГРАТЬ ");
            Console.SetCursorPosition(45, 13);
            Console.Write("(4)ВЫЛЕЧИТЬ ");

            Console.SetCursorPosition(0, 4);
            Console.WriteLine(AnimalPicture.BeginningPicture);
            Console.ResetColor();
        }

        /// <summary>
        /// Очистка поля экрана для сообщений.
        /// </summary>
        static void ClearMessageWindow()
        {
            Console.BackgroundColor = ConsoleColor.White;
            for (int j = 4; j < 12; j++)
            {
                Console.SetCursorPosition(17, j);
                for (int i = 0; i < 41; i++) Console.Write(" ");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Выбор персонажа.
        /// </summary>
        /// <param name="IndexAnimal"></param>
        static void Choise(ref int IndexAnimal)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("Выберите одного из животных:");
            Console.SetCursorPosition(20, 6);
            Console.WriteLine("(D) - щенок 'ДРУЖОК'");
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("(C) - котёнок 'ПУШОК'");
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("(H) - хомяк 'ХОМА'");
            Console.ResetColor();

            Console.SetCursorPosition(0, 4);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            bool flag = false;
            do
            {
                ConsoleKeyInfo ChoiceNursling = Console.ReadKey(true);
                switch (ChoiceNursling.Key)
                {   
                    case ConsoleKey.D:
                        {
                            Console.WriteLine(AnimalPicture.PuppyPicture);
                            IndexAnimal = 0; flag = true; break;
                        }
                    case ConsoleKey.C:
                        {
                            Console.WriteLine(AnimalPicture.KittenPicture);
                            IndexAnimal = 1; flag = true; break;
                        }
                    case ConsoleKey.H:
                        {
                            Console.WriteLine(AnimalPicture.HumsterPicture);
                            IndexAnimal = 2; flag = true; break;
                        }
                    default: break;
                }
            }
            while (flag == false);
            Console.ResetColor();
        }
  
        /// <summary>
        /// Управление жизнью питомца. 
        /// </summary>
        static void Live(Animal animals)
        {
            ClearMessageWindow();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("ПОЗДРАВЛЯЮ!!! У вас появился " + animals.Name);
            
            DateTime BeginDrink = DateTime.Now;
            DateTime BeginEat = DateTime.Now;
            DateTime BeginPlay = DateTime.Now;
            DateTime BeginSick = DateTime.Now;

            int DrinkWithoutSecond = 10;
            int EatWithoutSecond = 15;
            int PlayWithoutSecond = 20;
            int SickWithoutSecond = 30;
            int Deadpoint = 0;
            string message;

            do
            {   while (!Console.KeyAvailable)
                {
                    DateTime TimeNow = DateTime.Now;

                    if (BeginDrink.AddSeconds(DrinkWithoutSecond) < TimeNow)
                    {
                        ClearMessageWindow();
                        message = "пить";
                        animals.IwantToDrink += DisplayMessage;
                        animals.Drink(animals, message);
                    }

                }
            }while (Deadpoint == 4 && Console.ReadKey(true).Key != ConsoleKey.Escape);

        }

        static void DisplayMessage (string message)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("Ваш питомец хочет " + message);
            Console.ResetColor();
        }












    }
}
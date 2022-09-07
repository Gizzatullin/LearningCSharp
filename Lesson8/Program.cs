using System;
using System.Threading;

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
            animals[IndexAnimal].Died();
            Thread.Sleep(3000);
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
            Console.Write("(Esc)ВЫХОД ");
            
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
            
            int DrinkWithoutSecond = 2;
            int EatWithoutSecond = 5;
            
            int DeadpointDrink = 1;
            int DeadpointEat = 1;
            string message = "";
            int NumberAction = 0;

            do
            {   
                while (!Console.KeyAvailable)
                {
                    DateTime TimeNow = DateTime.Now;
                    Thread.Sleep(2000);
                    ClearMessageWindow();

                    if (BeginDrink.AddSeconds(DrinkWithoutSecond) < TimeNow)
                    {
                        switch (DeadpointDrink)
                        {
                            case 1: { message = "ПИТЬ"; break; }
                            case 2: { message = "ОЧЕНЬ ПИТЬ"; break; }
                            case 3: { message = "ОЧЕНЬ СИЛЬНО ПИТЬ"; break; }
                        }
                                                
                        animals.IwantTo += DisplayMessage;
                        animals.EventIwantTo(message);
                        NumberAction = ChoosingAction(NumberAction);
                        
                        if(NumberAction == 3) { DeadpointDrink = 4;}

                        if (NumberAction == 2)
                        {
                            DisplayMessageWrongChoice();
                            DeadpointDrink++;                            
                        }
                        else
                        {
                            DeadpointDrink = animals.Drink(DeadpointDrink);
                        }
                        
                        if (DeadpointDrink == 1) { BeginDrink = DateTime.Now; }
                        ClearMessageWindow();
                    }


                    if (BeginEat.AddSeconds(EatWithoutSecond) < TimeNow)
                    {
                        switch (DeadpointEat)
                        {
                            case 1: { message = "ЕСТЬ"; break; }
                            case 2: { message = "ОЧЕНЬ ЕСТЬ"; break; }
                            case 3: { message = "ОЧЕНЬ СИЛЬНО ЕСТЬ"; break; }
                        }

                        animals.IwantTo += DisplayMessage;
                        animals.EventIwantTo(message);
                        NumberAction = ChoosingAction(NumberAction);
                       
                        if (NumberAction == 3) { DeadpointEat = 4; }

                        if (NumberAction == 1)
                        {
                            DisplayMessageWrongChoice();
                            DeadpointDrink++;
                        }
                        else
                        {
                            DeadpointEat = animals.Eat(DeadpointEat);
                        }
                        
                        if (DeadpointEat == 1) { BeginEat = DateTime.Now; }
                        ClearMessageWindow();
                    }

                   if (DeadpointDrink == 4 || DeadpointEat == 4) goto M1; // Не получается выйти из цикла по другому при достижении точки смерти!
                }
            }while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        M1:;   // Не получается выйти из цикла по другому при достижении точки смерти!
        }

        /// <summary>
        /// Шаблон сообщения о нуждах питомца.
        /// </summary>
        /// <param name="message"></param>
        static void DisplayMessage (string message)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("Ваш питомец хочет " + message + "!");
            Console.SetCursorPosition(20, 6);
            Console.WriteLine("Выберите действие.");
            Console.ResetColor();
        }

        /// <summary>
        /// Сообщение о не верном действии пользователя
        /// </summary>
        static void DisplayMessageWrongChoice ()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("УУУ! Не верное действие!");
            Console.ResetColor();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Выбор действие игроком на нужды питомца.
        /// </summary>
        /// <param name="NumberAction"></param>
        /// <returns></returns>
        static int ChoosingAction(int NumberAction)
        {
            bool flag = false;
            do
            {
                ConsoleKeyInfo ChoiceNursling = Console.ReadKey(true);
                switch (ChoiceNursling.Key)
                {
                    case ConsoleKey.D1:
                        {
                            NumberAction = 1; flag = true; break;
                        }
                    case ConsoleKey.D2:
                        {
                            NumberAction = 2; flag = true; break;
                        }
                    case ConsoleKey.Escape:
                        {
                            NumberAction = 3; flag = true; break;
                        }
                    default: break;
                }
            } while (flag == false);
            return NumberAction;
        }
    }
}

using System;
using System.Threading;

namespace Lesson8
{    
    /// <summary>
    /// Описание абстрактного животного "Animal" с его функциями.
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Определение события, что питомец хочет пить.
        /// </summary>
        public delegate void EventToWindow(string message);
        public event EventToWindow IwantTo;
                      
        public string Name { get; set; }
        
        /// <summary>
        /// Вывод на экран событий.
        /// </summary>
        /// <param name="message"></param>
        public void EventIwantTo(string message)
        {
            this.IwantTo?.Invoke(message);
        }

        public virtual int Drink(int DeadpointDrink) { return DeadpointDrink; }
        public virtual int Eat(int DeadpointEat) { return DeadpointEat; }
        
        /// <summary>
        /// Смерть питомца.
        /// </summary>
        public void Died()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            for (int j = 4; j < 12; j++)
            {
                Console.SetCursorPosition(17, j);
                for (int i = 0; i < 41; i++) Console.Write(" ");
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(26, 7);
            Console.WriteLine("ВАШ ПИТОМЕЦ УМЕР !!!");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Описание щенка.
    /// </summary>
    public class Puppy : Animal
    {   
        /// <summary>
        /// Щенок пьёт.
        /// </summary>
        /// <param name="DeadpointDrink"></param>
        /// <returns></returns>
        public override int Drink(int DeadpointDrink)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("ГАВ-ГАВ. Дружок напился!");
            Console.ResetColor();
            DeadpointDrink = 1;
            Thread.Sleep(3000);
            return DeadpointDrink;
        }

        /// <summary>
        /// Щенок ест.
        /// </summary>
        /// <param name="DeadpointEat"></param>
        /// <returns></returns>
        public override int Eat(int DeadpointEat)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("ГАВ-ГАВ. Дружок наелся!");
            Console.ResetColor();
            DeadpointEat = 1;
            Thread.Sleep(3000);
            return DeadpointEat;
        }
    }


    /// <summary>
    /// Описание котёнка.
    /// </summary>
    public class Kitten : Animal
    {
        /// <summary>
        /// Котёнок пьёт.
        /// </summary>
        /// <param name="DeadpointDrink"></param>
        /// <returns></returns>
        public override int Drink(int DeadpointDrink)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("МЯУ-МЯУ. Вкусное молочко!");
            Console.ResetColor();
            DeadpointDrink = 1;
            Thread.Sleep(3000);
            return DeadpointDrink;
        }

        /// <summary>
        /// Котёнок ест.
        /// </summary>
        /// <param name="DeadpointEat"></param>
        /// <returns></returns>
        public override int Eat(int DeadpointEat)
        {
           Console.BackgroundColor = ConsoleColor.DarkGreen;
           Console.ForegroundColor = ConsoleColor.White;
           Console.SetCursorPosition(20, 8);
           Console.WriteLine("МЯУ-МЯУ. Рыбку я люблю!");
           Console.ResetColor();
           DeadpointEat = 1;
           Thread.Sleep(3000);
           return DeadpointEat;
        }
    }

    /// <summary>
    /// Описание хомяка.
    /// </summary>
    public class Humster : Animal
    {
        /// <summary>
        /// Хомяк пьёт.
        /// </summary>
        /// <param name="DeadpointDrink"></param>
        /// <returns></returns>
        public override int Drink(int DeadpointDrink)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("Ё-МОЁ. Хозяин МОЛОДЦА!");
            Console.ResetColor();
            DeadpointDrink = 1;
            Thread.Sleep(3000);
            return DeadpointDrink;
        }

        /// <summary>
        /// Хомяк ест.
        /// </summary>
        /// <param name="DeadpointEat"></param>
        /// <returns></returns>
        public override int Eat(int DeadpointEat)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("УХ ТЫ! ОРЕХИ И ДОШИК!");
            Console.ResetColor();
            DeadpointEat = 1;
            Thread.Sleep(3000);
            return DeadpointEat;
        }
    }        
}
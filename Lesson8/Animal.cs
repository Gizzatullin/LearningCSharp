using System;
using System.Threading;

namespace Lesson8
{    
    /// <summary>
    /// Описание абстрактного животного "Animal" с его функциями
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Определение события, что питомец хочет пить.
        /// </summary>
        public delegate void EventToWindow(string message);
        public event EventToWindow IwantTo;

              
        public string Name { get; set; }
        
        public void Event(string message)
        {
            this.IwantTo?.Invoke(message);
        }

        public virtual int Drink(int NumberAction, int Deadpoint) { return Deadpoint; }
        public virtual void Eat() { }
        public virtual void Play() { }
        public virtual void Sick() { }
        public void Died()
        {
            Console.WriteLine("Умираю!!!");
        }
    }

    /// <summary>
    /// Описание щенка
    /// </summary>
    public class Puppy : Animal
    {
        public override int Drink(int NumberAction, int Deadpoint)
        {
            if (NumberAction == 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(20, 8);
                Console.WriteLine("ГАВ-ГАВ. Дружок доволен!");
                Console.ResetColor();
                Deadpoint = 1;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(20, 8);
                Console.WriteLine("УУУ! Не верное действие!");
                Console.ResetColor();
                Deadpoint++;
            }

            Thread.Sleep(5000);
            return Deadpoint;
        }
    }

    /// <summary>
    /// Описание котёнка 
    /// </summary>
    public class Kitten : Animal
    {
        public override int Drink(int NumberAction, int Deadpoint)
        {
            Console.WriteLine("Котёнок пьёт");
            return Deadpoint;
        }
    }

    /// <summary>
    /// Описание хомяка
    /// </summary>
    public class Humster : Animal
    {
        public override int Drink(int NumberAction, int Deadpoint)
        {
            Console.WriteLine("Хомяк пьёт");
            return Deadpoint;
        }
    }        
}
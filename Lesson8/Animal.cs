using System;

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
        public delegate void ToDrinK(string message);
        public event ToDrinK IwantToDrink;

        
        
        public string Name { get; set; }
        public virtual void Drink(Animal animals, string message)
        {
            Console.WriteLine("Пью");
        }
        public virtual void Eat()
        {
            Console.WriteLine("Ем");
        }
        public virtual void Play()
        {
            Console.WriteLine("Играю");
        }
        public virtual void Sick()
        {
            Console.WriteLine("Болею");
        }
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
        public override void Drink(Animal animals, string message)
        {
            this.IwantToDrink?.Invoke(message);
            
        }
    }

    /// <summary>
    /// Описание котёнка 
    /// </summary>
    public class Kitten : Animal
    {
        public override void Drink(Animal animals, string message)
        {
            Console.WriteLine("Котёнок пьёт");
        }
    }

    /// <summary>
    /// Описание хомяка
    /// </summary>
    public class Humster : Animal
    {
        public override void Drink(Animal animals, string message)
        {
            Console.WriteLine("Хомяк пьёт");
        }
    }        
}
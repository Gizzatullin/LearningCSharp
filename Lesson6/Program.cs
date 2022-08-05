using System;

namespace Lesson6
{   
    /// <summary>
    /// Общий класс Animal с методом для издания звука
    /// </summary>
    public class Animal
    {   
        public string Name { get; set; }
        public virtual void Action()
        {
            Console.WriteLine("Какой-то звук");
        }

        public void End()
        {
            Console.WriteLine("The END !!!");
        }
        
    }

    /// <summary>
    /// Подкласс Animal - Корова
    /// </summary>
    public class Cow: Animal
    {
        public override void Action()
        {
            Console.WriteLine("Мммууууууу");
        }
    }

    /// <summary>
    /// Подкласс Animal - Лошадь
    /// </summary>
    public class Horse : Animal
    {
        public override void Action()
        {
            Console.WriteLine("И-и-и-го-го-го");
        }
    }

    /// <summary>
    /// Подкласс Animal - Баран
    /// </summary>
    public class Ram : Animal
    {
        public override void Action()
        {
            Console.WriteLine("Бееееееее");
        }
    }

    /// <summary>
    /// Подкласс Animal - Петух
    /// </summary>
    public class Rooster : Animal
    {
        public override void Action()
        {
            Console.WriteLine("Ку-ка-ре-ку");
        }
    }


    /// <summary>
    /// Задание про создание класса Animal
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal[] animals = new Animal[4];   

            Cow cow = new Cow();
            cow.Name = "Бурёнка";

            Horse horse = new Horse();
            horse.Name = "Игогошка";

            Ram ram = new Ram();
            ram.Name = "Бараш";

            Rooster roster = new Rooster();
            roster.Name = "Петушара";

            animals[0] = cow;
            animals[1] = horse;  
            animals[2] = ram;
            animals[3] = roster; 

            for (int i = 0; i < animals.Length; i++)
            {
                Console.Write($"{animals[i].Name, -10}");
                animals[i].Action();
            }

            animals[0].End();
            Console.ReadKey();
        }
    }
}

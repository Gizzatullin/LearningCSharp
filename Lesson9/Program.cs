using System;

namespace Lesson9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool flagExit = false;
            Console.CursorVisible = false;

            do
            {
                Console.Clear();                            
                MenuTasks();
                ChoiceTask();

                Console.WriteLine("\nДля перехода к перечню задач нажмите любую клавишу, а для ВЫХОДА - <ESC>");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) flagExit = true;
                
            } while (flagExit == false);
        }

        /// <summary>
        /// Вывод перечня задач.
        /// </summary>
        static void MenuTasks()
        {
            string message = "ВЫБЕРИТЕ ЗАДАЧУ" + "\n\n" +
                             "1. Проверить число на простоту." + "\n" +
                             "2. Вычисление високосного года." + "\n" +
                             "3. Переворачивание стрелочки." + "\n" +
                             "4. Проверка на нахождение точки относительно окружности." + "\n" +
                             "5. Вывод таблицы умножения от 1 до 10." + "\n" +
                             "6. Сортировка массива строк." + "\n" +
                             "7. Отправка сообщения на почту.";

            Console.WriteLine(message);
        }

        /// <summary>
        /// Выбор решаемой задачи.
        /// </summary>
        static void ChoiceTask()
        {
            Tasks tasks = new Tasks();
            ConsoleKeyInfo ChoiceNursling = Console.ReadKey(true);
            Console.Clear();
            switch (ChoiceNursling.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    {
                        Console.WriteLine("Выполняем задачу 1 - Проверить число на простоту.");
                        Console.Write("\nВВЕДИТЕ ЦЕЛОЕ ЧИСЛО от 0 до 10.000 : ");

                        int number = Convert.ToInt32(Console.ReadLine());
                        bool primenumber = true;

                        tasks.Solution1(number, primenumber);
                        break;
                    }

                case ConsoleKey.D2: 
                case ConsoleKey.NumPad2:
                    {
                        Console.WriteLine("Выполняем задачу 2 - Вычисление високосного года.");
                        Console.Write("\nВВЕДИТЕ ГОД в ФОРМАТЕ YYYY : ");

                        int year = Convert.ToInt32(Console.ReadLine());
                        bool flag = true;

                        tasks.Solution2(year, flag);
                        break;
                    }

                case ConsoleKey.D3: 
                case ConsoleKey.NumPad3: { tasks.Solution3(); break; }

                case ConsoleKey.D4: 
                case ConsoleKey.NumPad4:
                    {
                        Console.WriteLine("Выполняем задачу 4 - Проверка на нахождение точки относительно окружности.");
                        Console.WriteLine("\nДана окружность с центром в точке (0, -1) и радиусом 2. Введите данные точки для определения нахождения её в границах окружности.");
                        
                        Console.Write("ВВЕДИТЕ КООРДИНАТУ X : ");
                        int XPoint = Convert.ToInt32(Console.ReadLine());
                        Console.Write("ВВЕДИТЕ КООРДИНАТУ Y : ");
                        int YPoint = Convert.ToInt32(Console.ReadLine());

                        bool flagTest = true;

                        tasks.Solution4(XPoint, YPoint, flagTest);
                        break;
                    }

                case ConsoleKey.D5: 
                case ConsoleKey.NumPad5: { tasks.Solution5(); break; }
                case ConsoleKey.D6: 
                case ConsoleKey.NumPad6: { tasks.Solution6(); break; }
                case ConsoleKey.D7: 
                case ConsoleKey.NumPad7: { tasks.Solution7(); break; }
                default: break;
            }            
        }
    }
}

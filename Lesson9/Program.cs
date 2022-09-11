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
                             "7. Отправка сообщения на почту." + "\n" +
                             "8. Unit-тесты для задач 1,2,4,5,6";

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
                case ConsoleKey.D1: { tasks.Solution1(); break; }
                case ConsoleKey.D2: { tasks.Solution2(); break; }
                case ConsoleKey.D3: { tasks.Solution3(); break; }
                case ConsoleKey.D4: { tasks.Solution4(); break; }
                case ConsoleKey.D5: { tasks.Solution5(); break; }
                case ConsoleKey.D6: { tasks.Solution6(); break; }
                case ConsoleKey.D7: { tasks.Solution7(); break; }
                case ConsoleKey.D8: { tasks.Solution8(); break; }
                default: break;
            }            
        }
    }
}

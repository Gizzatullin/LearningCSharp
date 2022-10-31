using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Scheduler
{

    internal class Program
    {
        static Shedule shedule = new Shedule();
        static int number;
        
        static DateTime date = DateTime.Now;
        static int id;
        static string name;
        static string start;
        static string end;
        static string timeAlarm;


        static bool FlagToEnd = true;

        static void Main(string[] args)
        {
            List<Meeting> allMeet = new List<Meeting>();
            while (FlagToEnd)
            {
                Console.Clear();
                Desktop(allMeet);
                Menu(allMeet);
            }
        }

        /// <summary>
        /// Вывод на экран информации о встречах и команд управдения.
        /// </summary>
        static void Desktop(List<Meeting> allMeet)
        {
            Console.WriteLine("КОМАНДЫ УПРАВЛЕНИЯ ПЛАНИРОВЩИКОМ ВСТРЕЧ:\n<1> - добавить, <2> - корректировать, <3> - удалить");
            Console.WriteLine("<4> - предыдущий день, <5> - следующий день, <0> - сохранить список встреч в файле, <ESC> - выход из программы.\n");

            Console.WriteLine("------------ГРАФИК ВАШИХ ВСТРЕЧ НА СЕГОДНЯШНИЙ ДЕНЬ------------");
            Console.WriteLine($"\n{date:M}, {date:dddd}\n");
            
            for(int i = 0; i < allMeet.Count; i++)
            {
                Console.WriteLine($"{allMeet[i]}");
            }
        }

        /// <summary>
        /// Взаимодейстиве с пользователем.
        /// </summary>
        static void Menu(List<Meeting> allMeet)
        { 
            ConsoleKeyInfo ChoiceCommand = Console.ReadKey(true);
            switch (ChoiceCommand.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    {
                        Console.WriteLine("\nВы выбрали команду <Добавить встречу>\n");
                        id = allMeet.Count + 1;
                        Meeting newMeet = InputDataMeeting(date, id);
                        shedule.AddMeeting(allMeet, newMeet);
                        break;
                    }

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    {
                        Console.WriteLine("\nВы выбрали команду <Корректировать встречу>\n");
                        Console.Write("Введите номер встречи, которую вы хотите изменить:");
                        number = int.Parse(Console.ReadLine());
                        if (number > allMeet.Count)
                        {
                            Console.WriteLine("Встречи с таким номером нет.");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine($"Вы корректируете следующую встречу\n\t{allMeet[number-1]}");
                            Meeting correctMeet = InputDataMeeting(date, id);
                            shedule.CorrectMeeting(allMeet, number, correctMeet);
                        }
                        break;
                    }

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    {
                        Console.WriteLine("\nВы выбрали команду <Удалить встречу>\n");
                        Console.Write("Введите номер встречи, которую вы хотите удалить:");
                        number = int.Parse(Console.ReadLine());
                        if (number > allMeet.Count)
                        {
                            Console.WriteLine("Встречи с таким номером нет.");
                            Console.WriteLine($"\n--- Нажмите любую клавишу для продолжения ---\n");
                            Console.ReadLine();
                        }
                        else
                        {
                            allMeet = shedule.DeleteMeeting (allMeet, number);
                        }
                        break;
                    }

                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    {
                        Console.WriteLine("\nВы выбрали команду <Записать список встреч в файл>\n");

                        string filename = $"Список встреч на {date:D}.txt";
                        string path = Path.Combine(Environment.CurrentDirectory, filename);

                        string[] lines = new string[allMeet.Count];

                        for (int i = 0; i < allMeet.Count; i++)
                        {
                            lines[i] = allMeet[i].ToString();
                        }

                        File.WriteAllLines(path, lines);
                        
                        Console.WriteLine($"\nСписок Ваших встреч находися в файле <Список встреч на {date:D}.txt> \n");
                        Console.WriteLine($"\n--- Нажмите любую клавишу для продолжения ---\n");
                        Console.ReadLine();
                        break;
                    }

                case ConsoleKey.Escape:
                    {
                        FlagToEnd = false;
                        break;                    
                    }
            }
        }
        

        /// <summary>
        /// Ввод данных о встрече.
        /// </summary>
        static Meeting InputDataMeeting(DateTime data, int id)
        {                     
            Console.Write("Введите название (описание) встречи:");
            name = Console.ReadLine();

            Console.Write("Введите время начала встречи в формате ##.## час.:");
            start = Console.ReadLine();

            Console.Write("Введите приблизительное время окончания встречи в формате ##.## час.:");
            end = Console.ReadLine();

            Console.Write("Укажите, за сколько минут до начала встречи вас предупредить:");
            timeAlarm = Console.ReadLine();

            Meeting Meet = new Meeting(data, id, name, start, end, timeAlarm);

            return Meet;
        }
    }
}

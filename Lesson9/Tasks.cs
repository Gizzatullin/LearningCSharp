using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace Lesson9
{
    /// <summary>
    /// Коллекция решаемых задач.
    /// </summary>
    public class Tasks
    {   
        /// <summary>
        /// Определение числа на простоту методом деления на делители от 2 до округлённого
        /// в большую сторону квадратного корня из введённого числа.
        /// </summary>
        public void Solution1()
        {
            Console.WriteLine("Выполняем задачу 1 - Проверить число на простоту.");
            Console.Write("\nВВЕДИТЕ ЦЕЛОЕ ЧИСЛО от 0 до 10.000 : ");
            
            int number = Convert.ToInt32(Console.ReadLine());
            bool primenumber = true;
            
            for (int i = 2; i <= Math.Ceiling(Math.Sqrt(number)); i++)
            {
                if (number % i == 0) primenumber = false;
            }
            if (primenumber == true) Console.WriteLine("РЕЗУЛЬТАТ: Число " + number + " простое.");
            else Console.WriteLine("РЕЗУЛЬТАТ: Число " + number + " не является простым.");
        }

        /// <summary>
        /// Вычисление високосного года с помощью отлавливания исключения.
        /// </summary>
        public void Solution2()
        {
            Console.WriteLine("Выполняем задачу 2 - Вычисление високосного года.");
            Console.Write("\nВВЕДИТЕ ГОД в ФОРМАТЕ YYYY : ");

            int year = Convert.ToInt32(Console.ReadLine());
            bool flag = true;

            try
            {
                DateTime time = new DateTime(year, 2, 29);
            }
            catch (Exception)
            {
                flag = false;
            }

            if (flag == true) Console.WriteLine("РЕЗУЛЬТАТ: Год " + year + " является високосным.");
            else Console.WriteLine("РЕЗУЛЬТАТ: Год " + year + " не високосный.");
        }

        /// <summary>
        /// Переворачивание стрелочки.
        /// </summary>
        public void Solution3()
        {   
            Console.WriteLine("Выполняем задачу 3 - Переворачивание стрелочки.");
            Console.Write("\nНАЖИМАЙТЕ СТРЕЛОЧКИ НА КЛАВИАТУРЕ И СЛЕДИТЕ ЗА ЕЁ ОТОБРАЖЕНИЕМ В КОНСОЛИ. ДЛЯ ВЫХОДА НАЖМИТЕ КЛАВИШУ <Esc>.");

            bool flag = true;

            do
            {
                ConsoleKeyInfo ChoiceNursling = Console.ReadKey(true);
                Console.SetCursorPosition( 1 , 4 );
                
                switch (ChoiceNursling.Key)
                {
                    case ConsoleKey.DownArrow: { Console.WriteLine("v"); break; }
                    case ConsoleKey.UpArrow: { Console.WriteLine("^"); break; }
                    case ConsoleKey.LeftArrow: { Console.WriteLine("<"); break; }
                    case ConsoleKey.RightArrow: { Console.WriteLine(">"); break; }
                    case ConsoleKey.Escape: { flag = false; break; }
                    default: break;
                }
            } while (flag == true);
        }

        /// <summary>
        /// Проверка на нахождение точки относительно окружности.
        /// </summary>
        public void Solution4()
        {
            Console.WriteLine("Выполняем задачу 4 - Проверка на нахождение точки относительно окружности.");
            
            int XCircle = 0;
            int YCircle = -1;
            int RadiusCircle = 2;

            Console.WriteLine("\nДана окружность с центром в точке (0, -1) и радиусом 2. Введите данные точки для определения нахождения её в границах окружности.");
            Console.Write("ВВЕДИТЕ КООРДИНАТУ X : ");
            int XPoint = Convert.ToInt32(Console.ReadLine());
            Console.Write("ВВЕДИТЕ КООРДИНАТУ Y : ");
            int YPoint = Convert.ToInt32(Console.ReadLine());

            if (Math.Pow((XPoint - XCircle),2) + Math.Pow((YPoint - YCircle), 2) <= Math.Pow(RadiusCircle, 2))
            {
                Console.WriteLine("РЕЗУЛЬТАТ: Точка с координатами (" + XPoint + ", " + YPoint + ") - находится в границах окружности.");
            }
            else
            {
                Console.WriteLine("РЕЗУЛЬТАТ: Точка с координатами (" + XPoint + ", " + YPoint + ") - НЕ находится в границах окружности.");
            }
        }

        /// <summary>
        /// Вывод таблицы умножения от 1 до 10.
        /// </summary>
        public void Solution5()
        {
            Console.WriteLine("Выполняем задачу 5 - Вывод таблицы умножения от 1 до 10.");
            Console.WriteLine("\n\nРЕЗУЛЬТАТ:\n");
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    Console.Write(j + "*" + i + "=" + j*i + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Сортировка массива строк по длине.
        /// </summary>
        public void Solution6()
        {
            Console.WriteLine("Выполняем задачу 6 - Сортировка массива строк по длине.");
            
            string[] lines = File.ReadAllLines("Lines.txt");
            Console.WriteLine("\n\nИСХОДНЫЕ СТРОКИ В ФАЙЛЕ:\n");
            foreach (string line in lines) Console.WriteLine(line);

            bool welldone = true;
            bool flagEnd = false;
            string buffer;
            
            do
            {   for (int i = 0; i < lines.Length-1; i++)
                {
                    if (lines[i].Length > lines[i + 1].Length)
                    {
                        buffer = lines[i + 1];
                        lines[i + 1] = lines[i];
                        lines[i] = buffer;
                        welldone = false;
                    }
                }
                
                if (welldone == false) { flagEnd = false; welldone = true; }
                else { flagEnd = true; }

            }while (flagEnd == false);

            Console.WriteLine("\n\nОТСОРТИРОВАННЫЕ СТРОКИ от МЕНЬШЕЙ длинны к БОЛЬШЕЙ:\n");
            foreach (string line in lines) Console.WriteLine(line);
        }

        /// <summary>
        /// Отправка сообщения на почту, SMTP-сервер это <smtp.yandex.ru>.
        /// </summary>
        public void Solution7()
        {   
            Console.WriteLine("Выполняем задачу 7 - Отправка сообщения на почту.");
            
            try
            {
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient mySmtpClient = new SmtpClient("smtp.yandex.ru", 465);

                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;

                System.Net.NetworkCredential basicAutificationInfo = new System.Net.NetworkCredential("AccadCif2022GR@yandex.ru", "123Test2022!");
                mySmtpClient.Credentials = basicAutificationInfo;

                MailAddress from = new MailAddress("AccadCif2022GR@yandex.ru", "Руслан");

                Console.Write("\n\nВВЕДИТЕ АДРЕС ПОЛУЧАТЕЛЯ ЭЛЕКТРОННОЙ ПОЧТЫ : ");
                string email = Console.ReadLine();
                string shablon_email = @"\S*@\w*.\D[a-z]{2}";
                if (Regex.IsMatch(email, shablon_email, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Адрес электронной почты " + email + " введен верно");
                                                            
                    MailAddress to = new MailAddress(email);
                
                    MailMessage myMail = new MailMessage(from, to);

                    MailAddress replayTo = new MailAddress("AccadCif2022GR@yandex.ru");
                    myMail.ReplyToList.Add(replayTo);
                        
                    myMail.Subject = "Выполнение задачи на C# по отправке письма";
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    myMail.Body = "<h2> ПРОВЕРКА ОТПРАВКИ ПИСЬМА </h2>";
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    myMail.IsBodyHtml = true;

                    mySmtpClient.Send(myMail);
                }
                else
                {
                    Console.WriteLine("Введенный адрес электронной почты " + email + " некорректен");
                    Console.WriteLine(Regex.IsMatch(email, shablon_email, RegexOptions.IgnoreCase));
                }
                                                
            }
            
            catch (SmtpException ex)
            {
                throw new ApplicationException
                    ("SmtpException has occuried: " + ex.Message);
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }               
    }
}
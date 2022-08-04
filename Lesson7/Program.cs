using System;

namespace Lesson7
{
    /// <summary>
    /// Задание про исключение
    /// </summary>
    internal class Program
    {

        public class MyException1 : Exception
        {
            public MyException1 (string message) : base(message)
            {
            }
        }

        public class MyException2 : Exception
        {
            public MyException2(string message) : base(message)
            {
            }
        }


        static void Main(string[] args)
        {

            try
            {   Console.WriteLine("Введите какую-нибудь информацию: ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                {
                    throw new MyException1 ("Вами введена пустая строка");
                }

                if (input.Length < 2)
                {
                    throw new MyException2("Вы ввели всего один символ");
                }
            }

            catch (MyException1 ex)
            {
                Console.WriteLine("Ошибка:" + ex.Message);
            }

            catch (MyException2 ex)
            {
                Console.WriteLine("Ошибка:" + ex.Message);
            }

            finally
            {
                Console.WriteLine("Конец программы!");
            }
            
            Console.ReadKey();

        }
    }
}

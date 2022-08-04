using System;

namespace Lesson7
{
    /// <summary>
    /// Задание про исключение
    /// </summary>
    internal class Program
    {

        /*public class Exception
        {
                
        }*/


        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Введите какую-нибудь информацию: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    throw new Exception("Вами введена пустая строка");
                }
            }

            catch (Exception)
            {
                Console.WriteLine("Введена пустая срока");
            }

            finally
            {
                Console.WriteLine("Конец программы!");
            }
            
            Console.ReadKey();

        }
    }
}

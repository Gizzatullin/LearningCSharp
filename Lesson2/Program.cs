using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2
{
    internal class Program
    {  
        static void Picture(Char[] Pole)
        {
            for (int i = 0; i < 9; i = i + 3)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("| " + Pole[i] + " | " + Pole[i + 1] + " | " + Pole[i + 2] + " |");
            }
            Console.WriteLine("-------------");
        }

        static void CheckWinner(Char[] Pole, ref int winX, ref int winY)
        {
            if (Pole[0] == 'X' && Pole[1] == 'X' && Pole[2] == 'X') winX = 1;
            if (Pole[3] == 'X' && Pole[4] == 'X' && Pole[5] == 'X') winX = 1;
            if (Pole[6] == 'X' && Pole[7] == 'X' && Pole[8] == 'X') winX = 1;
            if (Pole[0] == 'X' && Pole[3] == 'X' && Pole[6] == 'X') winX = 1;
            if (Pole[1] == 'X' && Pole[4] == 'X' && Pole[7] == 'X') winX = 1;
            if (Pole[2] == 'X' && Pole[5] == 'X' && Pole[8] == 'X') winX = 1;
            if (Pole[0] == 'X' && Pole[4] == 'X' && Pole[8] == 'X') winX = 1;
            if (Pole[2] == 'X' && Pole[4] == 'X' && Pole[6] == 'X') winX = 1;

            if (Pole[0] == 'Y' && Pole[1] == 'Y' && Pole[2] == 'Y') winY = 1;
            if (Pole[3] == 'Y' && Pole[4] == 'Y' && Pole[5] == 'Y') winY = 1;
            if (Pole[6] == 'Y' && Pole[7] == 'Y' && Pole[8] == 'Y') winY = 1;
            if (Pole[0] == 'Y' && Pole[3] == 'Y' && Pole[6] == 'Y') winY = 1;
            if (Pole[1] == 'Y' && Pole[4] == 'Y' && Pole[7] == 'Y') winY = 1;
            if (Pole[2] == 'Y' && Pole[5] == 'Y' && Pole[8] == 'Y') winY = 1;
            if (Pole[0] == 'Y' && Pole[4] == 'Y' && Pole[8] == 'Y') winY = 1;
            if (Pole[2] == 'Y' && Pole[4] == 'Y' && Pole[6] == 'Y') winY = 1;
        }



        static void Main(string[] args)
        {
        bg: Char[] Pole = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Char[] Name = { 'X', 'Y', 'X', 'Y', 'X', 'Y', 'X', 'Y', 'X' };
            int NumName = 0;
            int winX = 0;
            int winY = 0;


            Console.WriteLine("Игра «Крестики-Нолики» (TicTacToe)");
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
            Picture(Pole);


            for (NumName = 0; NumName < 9; NumName++)
            {

            m1: Console.Write("Ход игрока " + Name[NumName] + ":");
                string input = Console.ReadLine();
                if (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "7" && input != "8" && input != "9")
                {
                    Console.WriteLine("Вы нажали не правильную клавишу! Выберите значение от 1 до 9");
                    goto m1;
                }
                else { }

                int a = Convert.ToInt32(input);
                string b = Convert.ToString(Pole[a - 1]);
                switch (b)
                {
                    case "X":
                        Console.WriteLine("Данное поле уже занято X! Выберите другое поле");
                        goto m1;
                    case "Y":
                        Console.WriteLine("Данное поле уже занято Y! Выберите другое поле");
                        goto m1;
                    default:
                        Pole[a - 1] = Name[NumName];
                        break;
                }
                
                Console.WriteLine();
                Picture(Pole);
               
                CheckWinner(Pole, ref winX, ref winY);

                if (winX == 1 || winY == 1)
                {
                    Console.WriteLine("ПОЗДРАВЛЯЮ !!! Выйграл игрок " + Name[NumName]);
                    goto end;
                }

            }
            Console.WriteLine("НИЧЬЯ !!!");

       end:Console.WriteLine("Желаете ещё сыграть (y/n)?");
            string choise = Console.ReadLine();
            if (choise == "y") goto bg;


        }

    }
}

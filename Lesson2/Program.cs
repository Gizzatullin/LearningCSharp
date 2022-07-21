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

        static void CheckWinner(Char[] Pole, ref int winX, ref int winY, int[] WinComb)
        {
            for (int i = 0; i < 22; i = i+3)
            {
                if (Pole[WinComb[i]] == 'X' && Pole[WinComb[i + 1]] == 'X' && Pole[WinComb[i + 2]] == 'X') winX = 1;
                if (Pole[WinComb[i]] == 'Y' && Pole[WinComb[i + 1]] == 'Y' && Pole[WinComb[i + 2]] == 'Y') winY = 1;
            }
        }



        static void Main(string[] args)
        {
            int NewGame = 0;
            int[] WinComb = {0,1,2,3,4,5,6,7,8,0,3,6,1,4,7,2,5,8,0,4,8,2,4,6};

            do
            {
                Char[] Pole = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                Char[] Name = { 'X', 'Y', 'X', 'Y', 'X', 'Y', 'X', 'Y', 'X' }; 
                int NumName = 0;
                int winX = 0;
                int winY = 0;
                int GameBreak = 0;

                Console.WriteLine("Игра «Крестики-Нолики» (TicTacToe)");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Picture(Pole);

                do
                {
                    int ErrorInput;

                    do
                    {
                        ErrorInput = 0;
                        Console.Write("Ход игрока " + Name[NumName] + ":");
                        string input = Console.ReadLine();


                        int index1 = 0;
                        for (int i = 1; i <= 9; i++)
                        {
                            string cf = Convert.ToString(i);
                            if (input == cf) { index1 = 1; }
                        }

                        if (index1 == 1)
                        {
                            int a = Convert.ToInt32(input);
                            string b = Convert.ToString(Pole[a - 1]);
                            switch (b)
                            {
                                case "X": { index1 = 2; break; }
                                case "Y": { index1 = 3; break; }
                                default: { Pole[a - 1] = Name[NumName]; break; }
                            }
                        }

                        if (index1 == 0)
                        {
                            Console.WriteLine("Вы нажали не правильную клавишу! Выберите значение от 1 до 9");
                            ErrorInput = 1;
                        }
                        if (index1 == 2)
                        {
                            Console.WriteLine("Данное поле уже занято X! Выберите другое поле");
                            ErrorInput = 1;
                        }
                        if (index1 == 3)
                        {
                            Console.WriteLine("Данное поле уже занято Y! Выберите другое поле");
                            ErrorInput = 1;
                        }
                    }
                    while (ErrorInput == 1);


                    Console.WriteLine();
                    Picture(Pole);



                    CheckWinner(Pole, ref winX, ref winY, WinComb);

                    if (winX == 1 || winY == 1)
                    {
                        Console.WriteLine("ПОЗДРАВЛЯЮ !!! Выйграл игрок " + Name[NumName]);
                        GameBreak++;
                    }
                    
                    NumName++;
                    if (NumName == 9 && winX == 0 && winY == 0)
                    {
                        Console.WriteLine("НИЧЬЯ !!!");
                        GameBreak++;
                    }
                }
                while (GameBreak == 0);

                Console.WriteLine("Желаете ещё сыграть (y/n)?");
                string choise = Console.ReadLine();
                if (choise == "n") NewGame = 1;
            }
            while (NewGame == 0);

        }

    }
}

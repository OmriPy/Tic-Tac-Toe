using System;

namespace TicTacToe
{
    class Program
    {
        public static void TableInitialization(char[,] Arr)
        {
            int counter = 1;
            for (int i = 0; i < Arr.GetLength(1); i++)
            {
                for (int j = 0; j < Arr.GetLength(0); j++)
                {
                    Arr[j, i] = char.Parse(counter.ToString());
                    counter++;
                }
            }
        }

        public static void Table(char[,] Arr)
        {
            Console.Clear();
            for (int y = 0; y < Arr.GetLength(1); y++)
            {
                if (y != 0) Console.WriteLine("-----------");
                for (int x = 0; x < Arr.GetLength(0); x++)
                {
                    if (x != 0) Console.Write(" | ");
                    if (x % 3 == 0) Console.Write(" ");
                    Console.Write(Arr[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static char WhoWon(char[,] Arr)
        {
            for (int i = 0; i < Math.Sqrt(Arr.Length); i++)
            {
                if (Arr[0, i] == Arr[1, i] && Arr[0, i] == Arr[2, i]) return Arr[0, i]; // Rows
                else if (Arr[i, 0] == Arr[i, 1] && Arr[i, 0] == Arr[i, 2]) return Arr[i, 0]; // Columns
            }
            if (Arr[0, 2] == Arr[1, 1] && Arr[0, 2] == Arr[2, 0]) return Arr[0, 2]; // Diagonal
            else if (Arr[0, 0] == Arr[1, 1] && Arr[0, 0] == Arr[2, 2]) return Arr[0, 0]; // Diagonal
            else return ' ';
        }

        public static int FindX(char[,] Arr, int value) {
            return (value - 1) % (int)Math.Sqrt(Arr.Length);
        }
        public static int FindY(char[,] Arr, int value) {
            return (value - 1) / (int)Math.Sqrt(Arr.Length);
        }

        static void Main(string[] args)
        {
            char[,] TicTacToe = new char[3, 3];
            Random XOrandom = new Random();
            int place = 0;
            char XO;
            string keepPlaying;
            int ZeroOrOne;
            int i;
            do
            {
                TableInitialization(TicTacToe);
                Table(TicTacToe);
                ZeroOrOne = XOrandom.Next(0, 2);
                if (ZeroOrOne == 1) XO = 'X'; else XO = 'O';
                i = ZeroOrOne;
                while (i < ZeroOrOne + 9) // Each round
                {
                    if (i % 2 == 1) Console.WriteLine("It's X's turn!");
                    else Console.WriteLine("It's O's turn!");
                    Start:
                    try
                    {
                        do
                        {
                            Console.Write("Enter place (1-9):  ");
                            place = int.Parse(Console.ReadLine());
                            if (TicTacToe[FindX(TicTacToe, place), FindY(TicTacToe, place)] == 'X' ||
                                TicTacToe[FindX(TicTacToe, place), FindY(TicTacToe, place)] == 'O')
                            {
                                Console.WriteLine("This place is already taken.\n");
                                goto Start;
                            }
                        }
                        while (place < 1 || place > 9);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error:\tYou are able to enter numbers ONLY.\n");
                        goto Start;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Error:\tYou are able to enter numbers between 1 to 9 (both included) ONLY.\n");
                        goto Start;
                    }
                    TicTacToe[FindX(TicTacToe, place), FindY(TicTacToe, place)] = XO;
                    Table(TicTacToe);
                    if (WhoWon(TicTacToe) != ' ')
                    {
                        Console.WriteLine(WhoWon(TicTacToe) + " Won!");
                        break;
                    }
                    i++;
                    if (XO == 'X') XO = 'O'; else XO = 'X';
                }
                if (WhoWon(TicTacToe) == ' ') Console.WriteLine("It's a Tie!");
                Console.Write("\nDo you want to keep playing?\nY/N:\t");
                keepPlaying = Console.ReadLine().ToUpper();
            }
            while ((keepPlaying == "Y" || keepPlaying == "YES") && (keepPlaying != "N" || keepPlaying != "NO")); // Each game.
        }
    }
}

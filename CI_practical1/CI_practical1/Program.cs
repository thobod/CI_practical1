using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;


class Program
{
    static void Main(string[] args)
    {
        SudokuBoard board = Makesudoku();
        int N = 9;//hardcoding for the win, nahh this has to go soon.
        board.PrintSudoku();
        Console.WriteLine("Double values: " + board.EvalueteBoard());

        //for testing purposese.
        int[] test = board.EvalueteBoard();
        for (int i = 0; i < test.Length; i++)
        {
            Console.Write(test[i] + " ");
        }
        Console.WriteLine();
        Search search = new Search(board, N);
        search.Hillclimbing();
        //Console.WriteLine("the board will be better or the same if these values are exchanged: ({0}, {1}), ({2}, {3})", result.Item1, result.Item2, result.Item3, result.Item4);//if this returns (0,0) (0,0) no better value was found.
        Console.ReadLine();
    }

    public static SudokuBoard Makesudoku() //reads the sudoku from console and converts it to a sudokuBoard
    {
        //read input
        Console.WriteLine("Paste the sudoku (numbers only)");
        List<string> input = new List<string>();
        string firstLine = Console.ReadLine();
        int N = firstLine.Length;
        input.Add(firstLine);
        for (int i = 1; i < N; i++)
        {
            input.Add(Console.ReadLine());
        }
        int[,] sudokuIntArray = new int[N, N];
        
        //add input to array
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                sudokuIntArray[i, j] = int.Parse(input[i][j].ToString());
            }
        }
        //make a sudokuboard from the array
        SudokuBoard sudoku= new SudokuBoard(sudokuIntArray,N);
        return sudoku;
    }
}


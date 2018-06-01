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
        SudokuBoard board = makesudoku();
        board.printSudoku();
        Console.WriteLine("Double values: " + board.evalueteBoard());
        Console.ReadLine();
    }

    public static SudokuBoard makesudoku() //reads the sudoku from console and converts it to a sudokuBoard
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


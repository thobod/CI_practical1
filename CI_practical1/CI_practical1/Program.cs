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

        makesudoku();
        Console.ReadLine();
    }
    public static SudokuBoard makesudoku() //reads the sudoku from console and converts it to a sudokuBoard
    {
        Console.WriteLine("give the root of N");
        int rootN = int.Parse(Console.ReadLine());
        List<string> input = new List<string>();
        for (int i = 0; i < rootN * rootN; i++)
        {
            input.Add(Console.ReadLine());
        }
        int N = input.Count;
        int[,] sudokuIntArray = new int[N, N];
        
        string[] row;
        for (int i = 0; i < N; i++)
        {
            row = input[i].Split(' ');
            for (int j = 0; j < N; j++)
            {
                sudokuIntArray[i, j] = int.Parse(row[j]);
            }
        }
        SudokuBoard sudoku= new SudokuBoard(sudokuIntArray,N);
        return sudoku;
    }
}


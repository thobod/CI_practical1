using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SudokuBoard
{
    //this class represents the sudoku board. with all the values in it.
    Grid sudoku;
    int N;
    public SudokuBoard(int[,] sudokuboard, int N)
    {
        this.N = N;
        sudoku = new Grid(N, sudokuboard);
    }

    public void PrintSudoku() //prints the sudoku to console.
    {
        for (int x = 0; x < N; x++)
        {
            for (int y = 0; y < N; y++)
            {
                Console.Write(sudoku.ValuesArray[x, y] + " ");

            }
            Console.WriteLine();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SudokuBoard
{
    //this class represents the sudoku board. with all the values in it.
    Grid sudoku;
    int[,] preboard;
    int N;
    ISearch searchFunction;

    public SudokuBoard(int[,] sudokuboard, int N)
    {
        preboard = sudokuboard;
        this.N = N;
        sudoku = new Grid(N, sudokuboard);
        searchFunction = new Iterated_Local_Search(sudoku, N);
    }

    public void Search(int iterations)
    {
        searchFunction.Search(iterations, (int)Math.Sqrt(N), N);
    }

    public void PrintSudoku() //prints the sudoku to console.
    {
        sudoku.PrintGrid();
    }
}


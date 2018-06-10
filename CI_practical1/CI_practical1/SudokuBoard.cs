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
        preboard = new int[N, N];
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
            {
                preboard[i, j] = sudokuboard[i, j];
            }

        this.N = N;
        sudoku = new Grid(N, sudokuboard);
        searchFunction = new Iterated_Local_Search(sudoku, N);
    }

    public bool Search(int iterations, int S, int P)
    {
        return searchFunction.Search(iterations, S, P);
    }

    public void PrintSudoku() //prints the sudoku to console.
    {
        sudoku.PrintGrid();
    }
    public void resetBoard()
    {
        sudoku = new Grid(N, preboard);
        searchFunction = new Iterated_Local_Search(sudoku, N);
    }
    public int[,] Preboard
    {
        get { return preboard; }
    }
}


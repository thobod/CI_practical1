using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SudokuBoard
{
    Field[,] sudoku;
    int N;
    public SudokuBoard(int[,] sudokuboard, int N)
    {
        sudoku = new Field[N, N];
        this.N = N;
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
            {
                sudoku[i, j] = new Field(sudokuboard[i, j]);
            }
    }
}


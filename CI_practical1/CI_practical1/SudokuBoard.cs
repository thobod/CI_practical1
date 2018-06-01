using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SudokuBoard
{
    //this class represents the sudoku board. with all the values in it.
    Field[,] sudoku;
    int N;
    public SudokuBoard(int[,] sudokuboard, int N)
    {
        sudoku = new Field[N, N];
        this.N = N;
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
            {
                sudoku[i, j] = new Field(sudokuboard[i, j], (i / (int)Math.Sqrt(N)), (j / (int)Math.Sqrt(N)));
            }
        randomizeboard();
    }

    public void randomizeboard()
    {
        for (int x = 0; x < N; x+= (int)Math.Sqrt(N))
            for (int y = 0; y < N; y += (int)Math.Sqrt(N))
            {
                List<int> fixedvalues = new List<int>();
                int nextValue = 1;
                for (int i = sudoku[x, y].BlockX * (int)Math.Sqrt(N); i < (sudoku[x, y].BlockX + 1) * (int)Math.Sqrt(N); i++)
                    for (int j = sudoku[x, y].BlockY * (int)Math.Sqrt(N); j < (sudoku[x, y].BlockY + 1) * (int)Math.Sqrt(N); j++)
                    {
                        if (sudoku[i, j].FieldValue > 0)
                            fixedvalues.Add(sudoku[i, j].FieldValue);
                    }
                for (int i = sudoku[x, y].BlockX * (int)Math.Sqrt(N); i < (sudoku[x, y].BlockX + 1) * (int)Math.Sqrt(N); i++)
                    for (int j = sudoku[x, y].BlockY * (int)Math.Sqrt(N); j < (sudoku[x, y].BlockY + 1) * (int)Math.Sqrt(N); j++)
                    {
                        if (!sudoku[i, j].IsFixed)
                        {
                            while(fixedvalues.Contains(nextValue))
                            {
                                nextValue++;
                            }
                            sudoku[i, j].FieldValue = nextValue;
                            nextValue++;

                        }


                        /* if (!fixedvalues.Contains(sudoku[i, j].FieldValue))
                         {
                             sudoku[i, j].FieldValue = nextValue;
                             fixedvalues.Add(nextValue);
                             nextValue++;
                         }*/

                    }
            }
    }

    public void printSudoku()
    {
        for (int x = 0; x < N; x++)
        {
            for (int y = 0; y < N; y++)
            {
                Console.Write(sudoku[x, y].FieldValue + " ");

            }
            Console.WriteLine();
        }
    }
}


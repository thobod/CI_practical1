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
        for (int i = 0; i < N; i++) //initiate the board with the given array of ints.
            for (int j = 0; j < N; j++)
            {
                sudoku[i, j] = new Field(sudokuboard[i, j], (i / (int)Math.Sqrt(N)), (j / (int)Math.Sqrt(N)));
            }
        randomizeboard();
    }

    public void randomizeboard() //generates a random board, with unique values in each block, and the fixed values staying fixed.
    {
        for (int x = 0; x < N; x+= (int)Math.Sqrt(N)) //for every block in the board:
            for (int y = 0; y < N; y += (int)Math.Sqrt(N))
            {
                List<int> fixedvalues = new List<int>();
                int nextValue = 1;
                for (int i = sudoku[x, y].BlockX * (int)Math.Sqrt(N); i < (sudoku[x, y].BlockX + 1) * (int)Math.Sqrt(N); i++) //for every field in the block : check which values are already in use
                    for (int j = sudoku[x, y].BlockY * (int)Math.Sqrt(N); j < (sudoku[x, y].BlockY + 1) * (int)Math.Sqrt(N); j++)
                    {
                        if (sudoku[i, j].FieldValue > 0)
                            fixedvalues.Add(sudoku[i, j].FieldValue);
                    }
                for (int i = sudoku[x, y].BlockX * (int)Math.Sqrt(N); i < (sudoku[x, y].BlockX + 1) * (int)Math.Sqrt(N); i++) //for every field in the block : add a value which isnt in that block yet. 
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
                    }
            }
    }

    public void printSudoku() //prints the sudoku to console.
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


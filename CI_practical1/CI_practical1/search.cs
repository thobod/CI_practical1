
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Random;


class Search
{
    Random r = new Random();

    SudokuBoard sudoku;
    int N;

    public Search(SudokuBoard sudoku, int N)
    {
        this.sudoku = sudoku;
        this.N = N;
    }

    public void Hillclimbing()
    {
        int[] currentScore = sudoku.EvalueteBoard(); //to evalutate scores.
        int index = 0;
        while (currentScore.Sum() > 0)
        {
            index++;
            SudokuBoard editableSudoku = DuplicateSudoku(sudoku); //this should make a whole copy of the board, but maybe it is only a referrence like with arrays, might be faulty.
                                                                  //choose a random block from the board.
            int block = r.Next(N);
            int blockX = block % (int)Math.Sqrt(N);
            int blockY = block / (int)Math.Sqrt(N);
            currentScore = editableSudoku.EvalueteBoard(); //to evalutate scores.
            int bestScore = currentScore.Sum(); //best score so far, to compare with.
            Tuple<int, int, int, int> bestChange = new Tuple<int, int, int, int>(0, 0, 0, 0); //current best move gets saved here.
            bool betterchange = false; //if a better board than the current board is found this is changed to true
                                       //try all possible swaps within the block.
            for (int i = blockX * (int)Math.Sqrt(N); i < (blockX + 1) * (int)Math.Sqrt(N); i++) //for every field in the block : swap it with every field in the block
                for (int j = blockY * (int)Math.Sqrt(N); j < (blockY + 1) * (int)Math.Sqrt(N); j++)
                {
                    if (!sudoku.Sudoku[i, j].IsFixed)
                    {
                        int currentFieldx = i; //save which field youre on, so not to compare with self
                        int currentFieldy = j;
                        for (int x = blockX * (int)Math.Sqrt(N); x < (blockX + 1) * (int)Math.Sqrt(N); x++) //swap with every non fixed field which also isnt itself
                            for (int y = blockY * (int)Math.Sqrt(N); y < (blockY + 1) * (int)Math.Sqrt(N); y++)
                            {
                                if (x != currentFieldx && y != currentFieldy)
                                    if (!sudoku.Sudoku[x, y].IsFixed)
                                    {
                                        editableSudoku.Changeboard(i, j, x, y); //exchange 2 values from the board.
                                                                                //reëvalutate all the updated lines
                                                                                /*currentScore[(i * 2) + 1] = editableSudoku.evaluateLine(true, i);
                                                                                currentScore[(j * 2)] = editableSudoku.evaluateLine(true, j);
                                                                                currentScore[(x * 2) + 1] = editableSudoku.evaluateLine(true, x);
                                                                                currentScore[(y * 2)] = editableSudoku.evaluateLine(true, y);*/
                                        currentScore = editableSudoku.EvalueteBoard();
                                        //check if the update inproved:
                                        if (currentScore.Sum() <= bestScore)// if the score is better or the same as the first one, set that as the best option.
                                        {
                                            Console.WriteLine("i found " + currentScore.Sum() + " is lower than " + bestScore);
                                            bestScore = currentScore.Sum();
                                            bestChange = new Tuple<int, int, int, int>(i, j, x, y);
                                            betterchange = true;
                                            
                                        }



                                    }
                            }



                    }
                }

            if (betterchange)
            {
                Console.WriteLine(sudoku.EvalueteBoard().Sum()+ "this is a better change:");
                sudoku.Changeboard(bestChange.Item1, bestChange.Item2, bestChange.Item3, bestChange.Item4);
                Console.WriteLine(sudoku.EvalueteBoard().Sum());
            }
            //return the one with the best outcome
            if (index % 50 == 0)
            {
                sudoku.PrintSudoku();
            }
                
            if (index % 500 == 0)
                break;
        }

    }

    public SudokuBoard DuplicateSudoku(SudokuBoard sudoku)
    {
        int[,] values = new int[N, N];
        bool[,] fixedValues = new bool[N, N];
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
            {
                values[i, j] = sudoku.Sudoku[i, j].FieldValue;
                fixedValues[i, j] = sudoku.Sudoku[i, j].IsFixed;
            }

        return (new SudokuBoard(values, fixedValues, N));
    }



}



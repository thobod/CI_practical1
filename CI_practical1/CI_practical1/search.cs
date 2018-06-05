﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Random;


class search
{
    Random r = new Random();

    SudokuBoard sudoku;
    int N;

    public search(SudokuBoard sudoku, int N)
    {
        this.sudoku = sudoku;
        this.N = N;
    }

    public Tuple<int,int,int,int> hillclimbing()
    {
        SudokuBoard editableSudoku = sudoku; //this should make a whole copy of the board, but maybe it is only a referrence like with arrays, might be faulty.
        //choose a random block from the board.
        int block = r.Next(N);
        int blockX = block % (int)Math.Sqrt(N);
        int blockY = block / (int)Math.Sqrt(N);
        int[] currentScore = editableSudoku.evalueteBoard(); //to evalutate scores.
        int[] previousScore = editableSudoku.evalueteBoard(); //to compare with
        int bestScore = previousScore.Sum(); //best score so far, to compare with.
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
                                    editableSudoku.changeboard(i, j, x, y); //exchange 2 values from the board.
                                    //reëvalutate all the updated lines
                                    currentScore[(i * 2)+1] = editableSudoku.evaluateLine(true, i);
                                    currentScore[(j * 2)] = editableSudoku.evaluateLine(true, j);
                                    currentScore[(x * 2)+1] = editableSudoku.evaluateLine(true, x);
                                    currentScore[(y * 2)] = editableSudoku.evaluateLine(true, y);
                                    //check if the update inproved:
                                    if (currentScore.Sum() >= bestScore)// if the score is better or the same as the first one, set that as the best option.
                                    {
                                        bestScore = currentScore.Sum();
                                        bestChange = new Tuple<int, int, int, int>(i, j, x, y);
                                        betterchange = true;
                                    }



                                }
                        }



                }
            }
        if (betterchange)
            return bestChange;//return the one with the best outcome
        else
            return bestChange;//should return board without changes, but returning boards not yet implemented.
    }



    
}



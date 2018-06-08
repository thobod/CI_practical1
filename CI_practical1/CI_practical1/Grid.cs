using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Grid
{
    private HashSet<Tuple<int, int>> fixedPositions;
    private int N, sqrtN;
    public int[,] ValuesArray { get; set; }

    public Grid(int N, int[,] board)
    {
        ValuesArray = board;
        this.N = N;
        this.sqrtN = (int)Math.Sqrt(N);
        RandomizeGrid();
    }

    //Does all swaps possible in the block corresponding to the row and column number and saves the
    //best one in ValuesArray
    public void GenerateSuccessor(int row, int column, ISearch searchFunction)
    {
        List<int[,]> gridList = new List<int[,]>();
        gridList.Add((int[,])ValuesArray.Clone());
        Tuple<int, int>[] swaps = GetSwappablePositions(row, column);
        for(int i = 0; i < swaps.Length; i++)
        {
            for(int j = i + 1; j < swaps.Length; j++)
            {
                int[,] newGrid = Swap(swaps[i], swaps[j]);
                gridList.Add(newGrid);
            }
        }

        ValuesArray = searchFunction.ChooseSucessor(gridList);
    }

    //Randomize the empty spaces
    public void RandomizeGrid()
    {
        Random randomizer = new Random();
        for(int row = 0; row < sqrtN; row++)
        {
            for(int column = 0; column < sqrtN; column++)
            {
                HashSet<int> valuesToFill = new HashSet<int>(Enumerable.Range(1, N));
                for(int i = 0; i < sqrtN; i++)
                {
                    for(int j = 0; j < sqrtN; j++)
                    {
                        if(ValuesArray[row * sqrtN + i, column * sqrtN + j] > 0)
                        {
                            valuesToFill.Remove(ValuesArray[row * sqrtN + i, column * sqrtN + j]);
                            fixedPositions.Add(new Tuple<int, int>(row * sqrtN + i, column * sqrtN + j));
                        }
                        else
                        {
                            int r = randomizer.Next(valuesToFill.Count);
                            ValuesArray[row * sqrtN + i, column * sqrtN + j] = valuesToFill.ElementAt(r);
                            valuesToFill.Remove(valuesToFill.ElementAt(r));
                        }
                    }
                }
            }
        }

    }

    public int[,] Swap(Tuple<int, int> a, Tuple<int, int> b)
    {
        int[,] newArray = ValuesArray;
        int temp = newArray[a.Item1, a.Item2];
        newArray[a.Item1, a.Item2] = newArray[b.Item1, b.Item2];
        if (newArray[a.Item1, a.Item2] == ValuesArray[a.Item1, a.Item2]) throw new Exception("Apparantly shit gets passed by reference");
        newArray[b.Item1, b.Item2] = temp;
        return newArray;
    }

    public int EvaluateGrid(int[,] array)
    {
        int score = 0;
        //Score the vertical lines
        for(int i = 0; i < N; i++)
        {
            HashSet<int> currentLine = new HashSet<int>();
            for(int j = 0; j < N; j++)
            {
                if (currentLine.Contains(array[i, j])) score += 1;
                else currentLine.Add(array[i, j]);
            }
        }

        //Score the horizontal lines
        for(int j = 0; j < N; j++)
        {
            HashSet<int> currentLine = new HashSet<int>();
            for(int i = 0; i < N; i++)
            {
                if (currentLine.Contains(array[i, j])) score += 1;
                else currentLine.Add(array[i, j]);
            }
        }

        return score;
    }

    //Return Array of Tuples of all positions that can be swapped
    public Tuple<int, int>[] GetSwappablePositions(int row, int column)
    {
        if (row >= sqrtN || column >= sqrtN) throw new IndexOutOfRangeException("Row or Column number is larger than square root N");
        HashSet<Tuple<int, int>> swapPositions = new HashSet<Tuple<int, int>>();
        for(int i = row * sqrtN; i < (row + 1) * sqrtN; i++)
        {
            for(int j = column * sqrtN; j < (column + 1) * sqrtN; j++)
            {
                if (!fixedPositions.Contains(new Tuple<int, int>(i, j))) swapPositions.Add(new Tuple<int, int>(i, j));
            }
        }

        return swapPositions.ToArray();
    }
}

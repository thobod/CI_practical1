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
        fixedPositions = new HashSet<Tuple<int, int>>();
        ValuesArray = board;
        this.N = N;
        this.sqrtN = (int)Math.Sqrt(N);
        FindFixedPoints();
        RandomizeGrid();
    }

    public void FindFixedPoints()
    {
        for (int i = 0; i < N; i++)
        {
            for(int j = 0; j < N; j++)
            {
                if(ValuesArray[i,j] > 0)
                {
                    fixedPositions.Add(new Tuple<int, int>(i, j));
                }
            }
        }
    }
    //Randomize the empty spaces
    public void RandomizeGrid()
    {
        Random randomizer = new Random();
        for(int row = 0; row < sqrtN; row++)
        {
            for(int column = 0; column < sqrtN; column++)
            {
                //Console.WriteLine("Filling block ({0},{1}))", row, column);
                HashSet<int> valuesToFill = new HashSet<int>(Enumerable.Range(1, N));
                for(int i = 0; i < sqrtN; i++)
                {
                    for(int j = 0; j < sqrtN; j++)
                    {
                        if(fixedPositions.Contains(new Tuple<int, int>(row * sqrtN + i, column * sqrtN + j)))
                        {
                            //Console.WriteLine("({0},{1}) is fixed value {2}.", row * sqrtN + i, column * sqrtN + j, ValuesArray[row * sqrtN + i, column * sqrtN + j]);
                            valuesToFill.Remove(ValuesArray[row * sqrtN + i, column * sqrtN + j]);
                            /*Console.WriteLine("Removed the value. New valuestoFill is: ");
                            Console.Write("{");
                            for(int k = 0; k < valuesToFill.Count; k++)
                            {
                                Console.Write("{0} ", valuesToFill.ElementAt(k));
                            }
                            Console.Write("}");
                            Console.WriteLine();*/
                        }
                    }
                }
                for(int i = 0; i < sqrtN; i++)
                {
                    for(int j = 0; j < sqrtN; j++)
                    {
                        if(ValuesArray[row * sqrtN + i, column * sqrtN + j] == 0)
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
        int[,] newArray = new int[N, N];
        Array.Copy(ValuesArray, newArray, N * N);
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

    public void PrintGrid()
    {
        for(int j = 0; j < N; j++)
        {
            for(int i = 0; i < N; i++)
            {
                Console.Write("{0} ", ValuesArray[i, j]);
            }
            Console.WriteLine();
        }
    }
}

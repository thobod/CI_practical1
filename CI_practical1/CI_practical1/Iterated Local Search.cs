using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Iterated_Local_Search : ISearch
{
    public Grid Grid { get; set; }
    private int N, sqrtN, S, S_Count, P_Threshold;
    private static Random random;

    public Iterated_Local_Search(Grid grid, int N)
    {
        random = new Random();
        this.Grid = grid;
        this.N = N;
        sqrtN = (int)Math.Sqrt(N);
    }
    
    public bool Search(int iterations, params int[] searchParameters)
    {
        S = searchParameters[0];
        P_Threshold = searchParameters[1];
        int sameScore = Grid.EvaluateGrid(Grid.ValuesArray);
        S_Count = 0;
        int count = 0;
        //Console.WriteLine("Current grid is:");
        //Grid.PrintGrid();
        for (int i = 1; i <= iterations; i++)
        {
            Console.WriteLine("Iteration {0}...", i);
            Grid.ValuesArray = ChooseSucessor();
            if (S_Count == 0 && Grid.EvaluateGrid(Grid.ValuesArray) == sameScore) count += 1;
            else sameScore = Grid.EvaluateGrid(Grid.ValuesArray);
            Console.WriteLine("Score: {0}", Grid.EvaluateGrid(Grid.ValuesArray));
            if(Grid.EvaluateGrid(Grid.ValuesArray) == 0)
            {
                Console.WriteLine("Solution found:");
                Grid.PrintGrid();
                return true;
            }
            if(count > P_Threshold) { S_Count = S; count = 0; }
        }
        Console.WriteLine("Solution not found after {0} iterations, with Plateau Threshold {1} and S {2}", iterations, P_Threshold, S);
        Console.WriteLine("Intermediary solution with score {0}:", Grid.EvaluateGrid(Grid.ValuesArray));
        Grid.PrintGrid();
        return false;
    }

    public int[,] ChooseSucessor()
    {
        int row = random.Next(sqrtN);
        int col = random.Next(sqrtN);
        return GenerateSuccessor(row, col);
    }

    //Does all swaps possible in the block corresponding to the row and column number and saves the
    //best one in ValuesArray
    public int[,] GenerateSuccessor(int row, int column)
    {
        //Console.WriteLine("Sqrt N is {0}", sqrtN);
        int[,] currGrid = new int[N, N];
        Array.Copy(Grid.ValuesArray, currGrid, N * N);
        Tuple<int, int>[] swaps = Grid.GetSwappablePositions(row, column);
        //Console.WriteLine("Chosen column {0} and row {1}", column, row);
        //Console.WriteLine("Swappable positions are:");
        for(int i = 0; i < swaps.Length; i++)
        {
            //Console.WriteLine("Position:\t({0},{1})\tValue {2}", swaps[i].Item1, swaps[i].Item2, Grid.ValuesArray[swaps[i].Item1, swaps[i].Item2]);
        }
        int randomChosen = random.Next(swaps.Length);
        for (int i = 0; i < swaps.Length; i++)
        {
            for (int j = i + 1; j < swaps.Length; j++)
            {
                int[,] newGrid = Grid.Swap(swaps[i], swaps[j]);
                //Console.WriteLine("Grid after swap between ({0},{1}) and ({2},{3}):", swaps[i].Item1, swaps[i].Item2, swaps[j].Item1, swaps[j].Item2);
                //PrintArray(newGrid);
                if (S_Count > 0)
                {
                    if (i == randomChosen) currGrid = newGrid;
                }
                else if (Grid.EvaluateGrid(newGrid) <= Grid.EvaluateGrid(currGrid))
                {
                    currGrid = newGrid;
                    //Console.WriteLine("Accepted new grid as better grid with score {0}", Grid.EvaluateGrid(newGrid));
                }
            }
        }

        if (S_Count > 0) S_Count -= 1;

        return currGrid;
    }

    public void PrintArray(int[,] a)
    {
        for (int j = 0; j < N; j++)
        {
            for (int i = 0; i < N; i++)
            {
                Console.Write("{0} ", a[i, j]);
            }
            Console.WriteLine();
        }
    }
}
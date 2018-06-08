using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Iterated_Local_Search : ISearch
{
    public Grid Grid { get; set; }
    private int N, sqrtN, count, S;
    private static Random random;

    public Iterated_Local_Search(Grid grid, int N)
    {
        S = 0;
        random = new Random();
        this.Grid = grid;
        this.N = N;
        sqrtN = (int)Math.Sqrt(N);
    }
    
    public void Search(int iterations)
    {
        int sameScore = Grid.EvaluateGrid(Grid.ValuesArray);
        count = 0;
        for (int i = 1; i <= iterations; i++)
        {
            Console.WriteLine("Iteration {0}...", i);
            Grid.ValuesArray = ChooseSucessor();
            if (S == 0 && Grid.EvaluateGrid(Grid.ValuesArray) == sameScore) count += 1;
            else sameScore = Grid.EvaluateGrid(Grid.ValuesArray);
            Console.WriteLine("Score: {0}", Grid.EvaluateGrid(Grid.ValuesArray));
            if(Grid.EvaluateGrid(Grid.ValuesArray) == 0)
            {
                Console.WriteLine("Solution found:");
                Grid.PrintGrid();
                return;
            }
            if(count > 50) { S = 10; count = 0; }
        }
        Console.WriteLine("Solution not found after {0} iterations", iterations);
        Console.WriteLine("Intermediary solution:");
        Grid.PrintGrid();
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
        int[,] currGrid = new int[N, N];
        Array.Copy(Grid.ValuesArray, currGrid, N * N);
        Tuple<int, int>[] swaps = Grid.GetSwappablePositions(row, column);
        int randomChosen = random.Next(swaps.Length);
        for (int i = 0; i < swaps.Length; i++)
        {
            for (int j = i + 1; j < swaps.Length; j++)
            {
                int[,] newGrid = Grid.Swap(swaps[i], swaps[j]);
                if(S > 0)
                {
                    if (i == randomChosen) currGrid = newGrid;
                }
                else if (Grid.EvaluateGrid(newGrid) < Grid.EvaluateGrid(currGrid)) currGrid = newGrid;
            }
        }

        if (S > 0) S -= 1;

        return currGrid;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Backtracking : ISearch
{
    public Grid Grid { get; set; }
    private int N, sqrtN, S, S_Count, P_Threshold;
    private static Random random;

    public Backtracking(Grid grid, int N)
    {
        random = new Random();
        this.Grid = grid;
        this.N = N;
        sqrtN = (int)Math.Sqrt(N);
    }

    public bool Search(int iterations, params int[] searchParameters)
    {
        Stack<Grid> backtrackStack = new Stack<Grid>();
        backtrackStack.Push(Grid);
        while (backtrackStack.Count > 0)
        {
            Tuple<int, int> currentField;
            for (int i = 0; i < N; i++) //find the first nonzero value
                for (int j = 0; j < N; j++)
                {
                    if (Grid.ValuesArray[i, j] == 0)
                    {
                        currentField = new Tuple<int, int>(i, j);
                        goto endLoop;
                    }
                        
                }
            endLoop:;


        }
    }

    public bool CheckConstraints(int x, int y) //returns true if contraints are satisfied
    {
        return CheckLines(x, y) && CheckBlock(x, y);
    }
    public bool CheckLines(int x, int y) //returns true if the line constraint is satisfied
    {
        int score = 0;
        //Score the vertical lines
        HashSet<int> verticalLine = new HashSet<int>();
        for (int j = 0; j < N; j++)
        {
            if (verticalLine.Contains(Grid.ValuesArray[x, j])) score += 1;
            else verticalLine.Add(Grid.ValuesArray[x, j]);
        }

        //Score the horizontal lines
        HashSet<int> horizontalLine = new HashSet<int>();
        for (int i = 0; i < N; i++)
        {
            if (horizontalLine.Contains(Grid.ValuesArray[i, y])) score += 1;
            else horizontalLine.Add(Grid.ValuesArray[i, y]);
        }

        return score == 0;
    }

    public bool CheckBlock(int x, int y) //returns true if the block constaint is satisfied.
    {
        int score = 0;
        HashSet<int> block = new HashSet<int>();
        int blockX = x / sqrtN;
        int blockY = y / sqrtN;
        for (int i = blockX*sqrtN; i < (blockX+1) * sqrtN; i++)
            for (int j = blockY*sqrtN; j < (blockY+1) * sqrtN; j++)
            {
                if (block.Contains(Grid.ValuesArray[i, y])) score += 1;
                else block.Add(Grid.ValuesArray[i, y]);
            }
        return score == 0;
    }
}

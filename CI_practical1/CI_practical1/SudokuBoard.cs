using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SudokuBoard
{
    //this class represents the sudoku board. with all the values in it.
    Grid sudoku;
    int[,] preboard;
    int N;
    ISearch searchFunction;

    public SudokuBoard(int[,] sudokuboard, int N)
    {
        preboard = sudokuboard;
        this.N = N;
        sudoku = new Grid(N, sudokuboard);
        searchFunction = new Iterated_Local_Search(sudoku, N);
    }

    public void Search(int iterations)
    {
        Dictionary<Tuple<int, int>, string> succesRates = new Dictionary<Tuple<int, int>, string>(); 
        int[] s_List = { 1, 3, 5, 10, 15, 25, 50 };
        int[] p_list = { 10, 20, 30, 40, 50, 75, 100, 150, 200, 250 };
        int found, notfound;
        for( int i = 0; i < s_List.Length; i++)
        {
            for(int j = 0; j < p_list.Length; j++)
            {
                found = 0;
                notfound = 0;
                Console.WriteLine("Running iteration {0} with S {1} and P {2}...", i * s_List.Length + j + 1, s_List[i], p_list[j]);
                for(int ite = 1; ite <= iterations; ite++)
                {
                    searchFunction.Grid = new Grid(N, preboard);
                    bool solutionFound = searchFunction.Search(2000, new int[] {s_List[i], p_list[j] });
                    if (solutionFound) found++;
                    else notfound++;
                }
                succesRates.Add(new Tuple<int, int>(s_List[i], p_list[j]), (100f * (float)found / (float)(found + notfound)).ToString("0.00"));

            }
        }
        Console.WriteLine("Search complete");
        Console.WriteLine("Succes rates:");
        foreach(Tuple<int, int> tuple in succesRates.Keys)
        {
            Console.WriteLine("\t{0}:\t{1}", tuple, succesRates[tuple]);
        }

    }

    public void PrintSudoku() //prints the sudoku to console.
    {
        sudoku.PrintGrid();
    }
}


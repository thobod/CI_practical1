using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface ISearch
{
    Grid Grid {get; set;}
    int[,] ChooseSucessor();
    void Search(int iterations);
}

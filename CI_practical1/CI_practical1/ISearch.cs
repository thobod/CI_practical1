using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface ISearch
{
    int[,] ChooseSucessor(List<int[,]> successors);
}

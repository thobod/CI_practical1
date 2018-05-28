using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Field
{
    int value;
    bool isFixed;
    public Field(int value)
    {
        this.value = value;
        if (value > 0)
            isFixed = true;
        else
            isFixed = false;
    }
    public int Value
    {
        get { return value; }
    }
    public bool IsFixed
    {
        get { return isFixed; }
    }

}


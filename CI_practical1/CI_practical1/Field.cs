using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Field
{
    //this class keeps track of the value and weither or not the value is fixed.
    int fieldValue;
    bool isFixed;
    int blockX, blockY;
    public Field(int fieldValue, int blockX, int blockY)
    {
        this.fieldValue = fieldValue;
        if (fieldValue > 0)
            isFixed = true;
        else
            isFixed = false;
        this.blockX = blockX;
        this.blockY = blockY;
    }

    public int FieldValue
    {
        get { return fieldValue; }
        set { fieldValue = value; }
    }
    public bool IsFixed
    {
        get { return isFixed; }
    }
    public int BlockX
    {
        get { return blockX; }
    }
    public int BlockY
    {
        get { return blockY; }
    }
}


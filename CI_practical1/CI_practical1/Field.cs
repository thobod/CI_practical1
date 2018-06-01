using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Field
{
    //this class keeps track of the value and weither or not the value is fixed.
    int fieldValue; //value of a field
    bool isFixed;   //weither the value was given in the original problem or generated.
    int blockX, blockY; //the x and y coordinates of the block this field is a part of, in a 3x3 sudoku, the value at (4,1) would be in block (1,0) the value (7,8) in block (2,2) ect.

    public Field(int fieldValue, int blockX, int blockY)
    { 
        //initiating the field.
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


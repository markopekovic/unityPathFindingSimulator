using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    private int row;
    private int column;
    public int fieldWidth;

    private Field [,] gridTable;
    private Vector2 startPosition;
    private Vector2 endPosition;

    public Grid(int row,int numOfObst, int fieldWidth) {
        this.row = row;
        this.column = row;
        this.fieldWidth = fieldWidth;
        gridTable = createFieldGround(row, column);

        /*Debug.Log("[" + row + " , " + column + "]");
        Debug.Log("Matrica: " + gridToString());
        Debug.Log("startPosition :[" + startPosition.x + " , " + startPosition.y + "]");
        Debug.Log("endPosition :[" + endPosition.x + " , " + endPosition.y + "]");*/
        

    }


    public int Row { get { return row; } }
    public int Column { get { return column; } }
    public Field getField(int row, int column)
    {
        return gridTable[row , column];
    }

    public void setStartPosition(Vector2 startPosition)
    {
        int x = (int)startPosition.x;
        int y = (int)startPosition.y;
        if (x < 0 || x > row)
        {
            x = row - 1;
        }
        if (y < 0 || y > column)
        {
            y = row - 1;
        }
        this.startPosition = new Vector2(x, y);
        gridTable[x, y].colorWholeField(Color.green);
    }

    public Field getStartField()
    {
        return gridTable[(int)startPosition.x, (int)startPosition.y];
    }

    public Field getEndField()
    {
        return gridTable[(int)endPosition.x, (int)endPosition.y];
    }

    public void setEndPosition(Vector2 endPosition)
    {
        int x = (int)endPosition.x;
        int y = (int)endPosition.y;
        if (x < 0 || x > row )
        {
            x = row - 1;
        }
        if (y < 0 || y > column)
        {
            y = row - 1;
        }
        this.endPosition = new Vector2(x, y);
        gridTable[x,y].colorWholeField(Color.red);

    }

    public bool setObstacle(int x, int y)
    {
        //TODO proveri da li se prepreka nalazi na postojecoj putanji, ako ne dodaj , ako da, proveri sa promenom!!!
         if ((x >= 0 && x < row && y >= 0 && y < column)
            && (x != getStartField().Row || y != getStartField().Column)
            && (x != getEndField().Row || y != getEndField().Column)) {
            if (gridTable[x, y].isWalkable)
            {
                gridTable[x, y].isWalkable = false;
                return true;
            }
        }
        return false;
    }

    public bool resetObstacle(int x, int y)
    {
        if ((x >= 0 && x < row && y >= 0 && y < column)
           && (x != getStartField().Row || y != getStartField().Column)
           && (x != getEndField().Row || y != getEndField().Column))
        {
            gridTable[x, y].isWalkable = true;
            return true;
        }
        return false;
    }

    public void restartCost()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                gridTable[i, j].GCost = 0;
                gridTable[i, j].HCost = 0;
            }
        }
    }

    private Field[,] copyGrid(Field[,] grid)
    {
        Field[,] gridTableTmp = new Field[grid.GetLength(0), grid.GetLength(1)];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Field field = new Field(grid[i,j]);
                gridTableTmp[i, j] = field;
            }
        }

        return gridTableTmp;
    }

    public List<Field> getNeighbors(Field field)
    {
        return getNeighbors(field.Row, field.Column);
    }

    private List<Field> getNeighbors(int row, int column)
    {
        List<Field> neighbours = new List<Field>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int realX = row + x;
                int realY = column + y;

                if (realX >= 0 && realX < Row && realY >= 0 && realY <Column)
                {
                    neighbours.Add(gridTable[realX,realY]);
                }
            }
        }
        return neighbours;
    }

    
    public string gridToString()
    {
        string str = "";
        for (int i = 0; i < gridTable.GetLength(0); i++)
        {
            for (int j = 0; j < gridTable.GetLength(1); j++)
            {

                string pomstr = gridTable[i, j].isWalkable ? gridTable[i, j].FCost + "" : "T";
                str += ("\t" + pomstr + "\t");
            }
            str += '\n';
        }
        return "\n"+str;
    }
    
    private Field[,] createFieldGround(int row, int column)
    { // realCoordX, float realCoordY)

        Field[,] gridtemp = new Field[row, column];
        

        for (int i = 0; i < row; i++) {
            //float y = realCoordY + fieldWith / 2 + i * fieldWith;
            
            for (int j = 0; j < column; j++) {

                //float x = realCoordX + fieldWith / 2 + j * fieldWith;

                Field field = new Field(fieldWidth, i, j, true);
                
                //field.gCost = Integer.MAX_VALUE;
                gridtemp[i, j] = field;
                
            }
          
        }
        return gridtemp;
    }

    private Vector3 getWorldPosition(int row , int column)
    {
        return new Vector3(row, column) * fieldWidth;
    }

   
}

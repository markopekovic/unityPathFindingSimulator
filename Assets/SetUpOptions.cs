using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpOptions  {
    
    private static SetUpOptions instanc =  null;

    private int gridSize;
    private int startX, startY, endX, endY;
    private int numOfObs;
    private bool isAStar, isBFS;

    public int GridSize {get { return gridSize; } set { gridSize = value; } }
    public int NumOfObs { get { return numOfObs; } set { numOfObs = value; } }

    public int StartX { get { return startX; } set { startX = value; } }
    public int StartY { get { return startY; } set { startY = value; } }
    public int EndX { get { return endX; } set { endX = value; } }
    public int EndY { get { return endY; } set { endY = value; } }

    public bool IsAStar { get { return isAStar; } set { isAStar = value; } }
    public bool IsBFS { get { return isBFS; } set { isBFS = value; } }


    private  SetUpOptions()
    {
       //default value
       gridSize = 10;
       startX = 0;
       startY = 4;
       endX = 9;
       endY = 4;
       numOfObs = 18;
       isAStar = true;
       isBFS = false;
    }

    public static SetUpOptions getInstanc()
    {
        if (instanc == null)
        {
            instanc = new SetUpOptions();
        }
        return instanc;
    }

    public override string ToString()
    {
        return "SetUpOptions: "
            + " \n gridSize = " + gridSize
            + " \n startX = " + startX + " , startY = " + startY
            + " \n endX = " + endX + " , endY = " + endY
            + " \n numOfObs = " + numOfObs
            + " \n isAStar = " + isAStar
            + " \n isBFS = " + isBFS;

    }


}

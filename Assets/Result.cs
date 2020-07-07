
public class Result {

    private int gridSize, numOfObs, checkedField;
    private float time;
    private string runerName;


    public Result(int gridSize, int numOfObs, string runerName, int checkedField, float time)
    {
        this.gridSize = gridSize;
        this.numOfObs = numOfObs;
        this.runerName = runerName;
        this.checkedField = checkedField;
        this.time = time;
    }



public int GridSize { get { return gridSize; } set { gridSize = value; } }
    public int NumOfObs { get { return numOfObs; } set { numOfObs = value; } }
    public int CheckedField { get { return checkedField; } set { checkedField = value; } }

    public float Time { get { return time; } set { time = value; } }
    public string RunerName { get { return runerName; } set { runerName = value; } }




    public override string ToString()
    {
        return "gridSize = " + gridSize
            + ", numOfObs = " + numOfObs
            + ", runerName = " + runerName
            + ", checkedField = " + checkedField
            + ", time = " + time;

    }
}

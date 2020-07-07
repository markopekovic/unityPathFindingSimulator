using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathFindingAlg {

    private static int ID = -1;
    protected int id;
    protected string name;
    protected bool simulate = true;


    public event Func<Vector3, IEnumerator> onMoveToTheNext;
    public event Action<float, int> onFindingPath;
    public event Action<Field, int, Color > onEnterField;


    public string AlgName { get { return name; } }
    public bool Simulate { set { simulate = value; } }

    public PathFindingAlg()
    {
        id = ID++;
        Debug.Log("PathFindingAlg = " + id);

    }

    protected void enterField(Field field,Color color)
    {
        if (onEnterField != null)
        {
            onEnterField(field, id, color);
        }

    }
    protected void onPathFind(float time, int numOfFiled)
    {
        if (onFindingPath != null)
        {
          onFindingPath(time, numOfFiled);
        }
        
    }

    public abstract IEnumerator calculate(Grid grid);

    protected IEnumerator moveToNext( Vector3 v)
    {
        if (onMoveToTheNext != null)
        {
           return onMoveToTheNext(v);
        }
        return null;
    }

    protected int getDistance(Field fieldA, Field fieldB)
    {
        int distX = Mathf.Abs(fieldA.Row - fieldB.Row);
        int distY = Mathf.Abs(fieldA.Column - fieldB.Column);
        return 14 * Mathf.Min(distX, distY) + 10 * Mathf.Abs(distX - distY);
        
    }

    protected void retracePath(Field start, Field end)
    {
        List<Field> path = new List<Field>();
        Field current = end;
        while (current != start)
        {
            path.Add(current);
            current = current.Parent;
        }
        path.Reverse();

        foreach (Field f in path)
        {
            f.changeFieldColorTo(id, Color.blue);
            Debug.Log("[ " + f.Row + "," + f.Column + " ]");
        }
        start.colorWholeField(Color.green);
        end.colorWholeField(Color.red);
    }


    ~PathFindingAlg()
    {
        ID = -1;
    }
}

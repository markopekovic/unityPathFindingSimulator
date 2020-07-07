using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager
{
    private static ResultManager singleInstanc = null;

    private List<Result> results;

    private ResultManager()
    {
        results = new List<Result>();
    }

    public void add(Result result)
    {
        if (results != null) {
            results.Add(result);
        }
    }

    public static ResultManager getInstance()
    {
        if (singleInstanc == null)
        {
            singleInstanc = new ResultManager();
        }
        return singleInstanc;
    }


    public override string ToString()
    {
        string s = "Rsult";
        int i = 0; 
        foreach(var res in results)
        {
            s += "\n No: " + (++i) + ", " + res.ToString();
        }
        return s;
    }

    ~ResultManager()
    {
        results.Clear(); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {

    private Grid grid;
    public PathFindingAlg alg;
    private ResultManager resultManager;
    //private Vector3 destination;

    public void Awake()
    {
        Debug.Log("desio se Awake -> Runner");
        //PathFindingAlg.onMoveToTheNext += movement;
        //destination = transform.position;
    }


    public void Initialize(Grid grid, PathFindingAlg alg, ResultManager manager)
    {
        Debug.Log("desio se Initialize -> Runner");

        this.grid = grid;
        this.alg = alg;
        resultManager = manager;


    }
    // Use this for initialization
    void Start () {
        Debug.Log("desio se Start-> Runner");

        alg.onMoveToTheNext += movement;
        alg.onFindingPath += onFindPath;
        alg.onEnterField += onEnterField;
    }

    private void onEnterField(Field field,int algId, Color color)
    {
        field.changeFieldColorTo(algId, color);
    }

    private void onFindPath(float time, int number)
    {
        Result r = new Result(grid.Column, SetUpOptions.getInstanc().NumOfObs, alg.AlgName, number, time);
        resultManager.add(r);
        Debug.Log("RESULT: " + r);
    }

    private IEnumerator movement(Vector3 destination)
    {
        while (transform.position != destination) {
            transform.position = Vector3.MoveTowards(transform.position, destination, 55  * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator startRunning()
    {
        return alg.calculate(grid);
    }

    private void OnDestroy()
    {
        Debug.Log("desio se OnDestroy Runner");

    }
}

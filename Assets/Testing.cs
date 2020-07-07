using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {


    Grid grid;
    SetUpOptions setUpOptions;
    PathFindingAlg alg;
    List<Runner> runners;
    List<IEnumerator> currentCorutines;

    [SerializeField]
    private Runner runnerPrefab;
    [SerializeField]
    private GameObject goButton;



    public void Awake()
    {
        Debug.Log("Desio se Awake() ->Testing");
    }

    private void Start()
    {
        Debug.Log("Desio se Start() ->Testing");
        setUpOptions = SetUpOptions.getInstanc();

        int fieldSize = 14;
        grid = new Grid(setUpOptions.GridSize, setUpOptions.GridSize, fieldSize);

        GameObject.Find("Main Camera").transform.position =new Vector3(grid.Row * fieldSize / 2, - (grid.Row * fieldSize / 2), -10);
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = (int)((grid.Row * fieldSize / 2) + fieldSize);

        grid.setStartPosition(new Vector2(setUpOptions.StartX, setUpOptions.StartY));
        grid.setEndPosition(new Vector2(setUpOptions.EndX, setUpOptions.EndY));

        StopAllCoroutines();
        currentCorutines = new List<IEnumerator>();
        IEnumerator current = addObstacle(grid, SetUpOptions.getInstanc().NumOfObs);
        currentCorutines.Add(current);
        StartCoroutine(current);

        runners = new List<Runner>();

       // Runner BFSRunner;
       // Runner AStarRunner;
        if (setUpOptions.IsBFS)
        {
            var BFSRunner = Instantiate(runnerPrefab);
            BFSRunner.transform.position = new Vector3(grid.getStartField().getWorldPosition().x, grid.getStartField().getWorldPosition().y);
            BFSRunner.Initialize(grid, new BFS(), ResultManager.getInstance());
            runners.Add(BFSRunner);
        }

        if (setUpOptions.IsAStar)
        {
            var AStarRunner = Instantiate(runnerPrefab);
            AStarRunner.transform.position = new Vector3(grid.getStartField().getWorldPosition().x, grid.getStartField().getWorldPosition().y);
            AStarRunner.GetComponent<SpriteRenderer>().color = Color.black;
            AStarRunner.Initialize(grid, new AStar(), ResultManager.getInstance());
            runners.Add(AStarRunner);
        }



    }

    bool acceptTower = false;

    void onFindPath(float time, int number)
    {
        if (number > -1)
        {
            acceptTower = true;
        }
    }

    private IEnumerator addObstacle(Grid grid, int numOfObstacle) {
        PathFindingAlg alg = new AStar();
        alg.Simulate = false;
        alg.onFindingPath += onFindPath;
        int safetyInterruption = numOfObstacle * 2;

        System.Random random = new System.Random();
        for (int i = 0; i < numOfObstacle;)
        {
            int x = random.Next(0, grid.Row);
            int y = random.Next(0, grid.Row);

            if (grid.setObstacle(x, y))
            {
                yield return alg.calculate(grid);
                if (acceptTower)
                {
                    i++;
                    acceptTower = false;
                } else
                {
                    grid.resetObstacle(x, y);
                    --safetyInterruption;
                }
            }

            if (safetyInterruption == 0)
            {
                break;
            }
        }
        alg.onFindingPath -= onFindPath;
        alg.Simulate = false;
        goButton.SetActive(true);


    }

    IEnumerator simulateOneByOne(List<Runner> runners, Action onFinish)
    {

        Debug.Log("Number of Runers is: " + runners.Count);
        foreach (var runner in runners)
        {
            yield return runner.startRunning();
        }
        if (onFinish != null)
        {
            onFinish();
        }
        
    }

    void simulate(List<Runner> currentRunners, bool simulateInParalel, Action onFinish)
    {
        stopCurrentlyRunningCorutine();

        Debug.Log("Number of Runers is: " + currentRunners.Count);
        if (simulateInParalel)
        {
            foreach (var runner in currentRunners)
            {
                IEnumerator corutinCorutine = runner.startRunning();
                currentCorutines.Add(corutinCorutine);
                StartCoroutine(corutinCorutine);
            }
        }
        else
        {
            IEnumerator corutinOneByOne = simulateOneByOne(currentRunners, onFinish);
            currentCorutines.Add(corutinOneByOne);
            StartCoroutine(corutinOneByOne);
        }


    }

    private void stopCurrentlyRunningCorutine()
    {
        foreach (var currentCorutine in currentCorutines)
        {
            StopCoroutine(currentCorutine);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Desio se OnDestroy() ->Testing");

        StopAllCoroutines();
    }

    public void runGame()
    {
        goButton.SetActive(false);
        simulate(runners, false, openPopUp);
        
    }

    private void Update()
    {  }


    public void goToHomeScreen()
    {
        closePopUp();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void goToNextLevel()
    {
        closePopUp();
        ++SetUpOptions.getInstanc().NumOfObs;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToFinishScreen()
    {
        Debug.Log("goTFinishScreen");
        closePopUp();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Finish results :" + ResultManager.getInstance().ToString());
    }


    public GameObject popUp;
 

    private void openPopUp()
    {
        popUp.SetActive(true);
    }

    private void closePopUp()
    {
        popUp.SetActive(false);
    }

}

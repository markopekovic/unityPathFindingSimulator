using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    SetUpOptions setUpOptions;

    public void Awake()
    {
        setUpOptions = SetUpOptions.getInstanc();
    }

    public void playGame()
    {
        Debug.Log("Play set up: " + setUpOptions.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void quit()
    {
        Debug.Log("Quit GAME");
        Application.Quit();
    }

    public void gridSizeChange(string size)
    {
        if(!size.Equals(""))
        {
            setUpOptions.GridSize = int.Parse(size);
        }

    }

    public void obstaclesChange(string num)
    {
        if (!num.Equals(""))
        {
            setUpOptions.NumOfObs = int.Parse(num);
        }
    }


    public void startXChange(string x)
    {
        if (!x.Equals(""))
        {
            setUpOptions.StartX = int.Parse(x);
        }
    }
    public void startYChange(string y)
    {
        if (!y.Equals(""))
        {
            setUpOptions.StartY = int.Parse(y);
        }
    }
    public void endXChange(string x)
    {
        if (!x.Equals(""))
        {
            setUpOptions.EndX = int.Parse(x);
        }
    }
    public void endYChange(string y)
    {
        if (!y.Equals(""))
        {
            setUpOptions.EndY = int.Parse(y);
        }
    }

    public void setAStar (bool value)
    {
        setUpOptions.IsAStar = value;
    }
    public void setBFS(bool value)
    {
        setUpOptions.IsBFS= value;
    }

    //for pop up Options or Using from UNITIy direct
    //public GameObject popUp;

    //public void openPopUp()
    //{
    //    popUp.SetActive(true);
    //}

    //public void closePopUp()
    //{
    //    popUp.SetActive(false);
    //}

}

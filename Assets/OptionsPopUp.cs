using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopUp : MonoBehaviour {

    public InputField gridSize;
    public InputField startX;
    public InputField startY;
    public InputField endX;
    public InputField endY;
    public InputField numOfObs;
    public Toggle isAStar;
    public Toggle isBFS;


    public void Start()
    {
        SetUpOptions options = SetUpOptions.getInstanc();
        gridSize.text = options.GridSize.ToString();
        startX.text = options.StartX.ToString();
        startY.text = options.StartY.ToString();
        endX.text = options.EndX.ToString();
        endY.text = options.EndY.ToString();
        numOfObs.text = options.NumOfObs.ToString();

        isAStar.isOn = options.IsAStar;
        isBFS.isOn = options.IsBFS;
    }

    public void openPopUp()
    {
        GetComponent<OptionsPopUp>().enabled = true;
    }

    public void closePopUp()
    {
        GetComponent<OptionsPopUp>().enabled = false;
        //popUp.SetActive(false);
    }
}

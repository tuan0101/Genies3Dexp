using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeUISelection : MonoBehaviour
{
    [SerializeField] Toggle mazeToggle;
    [SerializeField] Toggle laserToggle;
    [SerializeField] Toggle easyToggle;

    private void Start()
    {
        InitializeValues();

        mazeToggle.onValueChanged.AddListener(delegate { OnMazeToggle(); });
        laserToggle.onValueChanged.AddListener(delegate { OnLaserToggle(); });
        easyToggle.onValueChanged.AddListener(delegate { OnEasyToggle(); });

    }

    //from previous gameplay section
    void InitializeValues()
    {
        mazeToggle.isOn = StaticVariables.maze_mode;
        laserToggle.isOn = StaticVariables.laser_mode;
        easyToggle.isOn = StaticVariables.is_easy_mode;
    }

    void OnMazeToggle()
    {
        StaticVariables.maze_mode = mazeToggle.isOn ? true : false;
    }

    void OnLaserToggle()
    {
        StaticVariables.laser_mode = laserToggle.isOn ? true : false;
    }

    void OnEasyToggle()
    {
        StaticVariables.is_easy_mode = easyToggle.isOn ? true : false;
    }
}

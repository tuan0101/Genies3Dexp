using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeUISelection : MonoBehaviour
{
    [SerializeField] Toggle mazeToggle;
    [SerializeField] Toggle laserToggle;

    private void Start()
    {
        mazeToggle.onValueChanged.AddListener(delegate { OnMazeToggle(); });
        laserToggle.onValueChanged.AddListener(delegate { OnLaserToggle(); });

    }

    void OnMazeToggle()
    {
        StaticVariables.maze_mode = mazeToggle.isOn ? true : false;
    }

    void OnLaserToggle()
    {
        StaticVariables.laser_mode = laserToggle.isOn ? true : false;
    }
}

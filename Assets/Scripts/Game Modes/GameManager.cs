using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject laser;

    private void Awake()
    {
        if (StaticVariables.maze_mode)
            maze.SetActive(true);
        else
            maze.SetActive(false);

        if (StaticVariables.laser_mode)
            laser.SetActive(true);
        else
            laser.SetActive(false);

        Debug.Log("laser: " + StaticVariables.laser_mode);
    }
}

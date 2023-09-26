using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] GameObject laser;
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] EndGameUI endGameUI;
    [SerializeField] Player player;

    public List<GameObject> grid;
    WaitForSeconds timeToDisappear = new WaitForSeconds(2f);
    bool isEasyMode = false;

    #region Public Getters and Setters
    public bool IsEasyMode { get => isEasyMode; set => isEasyMode = value; }
    #endregion


    private void Start()
    {
        if (StaticVariables.maze_mode)
            maze.SetActive(true);
        else
            maze.SetActive(false);

        if (StaticVariables.laser_mode)
            laser.SetActive(true);
        else
            laser.SetActive(false);

        isEasyMode = StaticVariables.is_easy_mode;

        grid = gridGenerator.Grid;
        StartCoroutine(DisableSquare());
    }

    // a random square will be disappeared every 2 seconds
    IEnumerator DisableSquare()
    {
        // wait until the grid is fully initialized;
        while (grid.Count != StaticVariables.grid_size * StaticVariables.grid_size)
        {
            yield return null;
        }

        while (grid.Count > 0)
        {
            yield return timeToDisappear;

            // check win condition
            if (grid.Count == 1)
            {
                Victory();
                yield break;
            }

            int chosenSquare = Random.Range(0, grid.Count);

            // hint the falling square on Easy Mode
            if (isEasyMode)
            {
                yield return FlashingSquare(grid[chosenSquare], Color.red);
            }
            StartCoroutine(FallingSquare(grid[chosenSquare].transform));

            grid.RemoveAt(chosenSquare);
        }
    }

    IEnumerator FallingSquare(Transform square)
    {
        float time = 0;
        float duration = 1;
        Vector3 targetPosition = square.position - Vector3.up * 10;
        while (time < duration)
        {
            square.position = Vector3.Lerp(square.position, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        square.gameObject.SetActive(false);
    }



    public IEnumerator FlashingSquare(GameObject chosenSquare, Color color)
    {
        float flashDuration = 0.25f;
        Renderer renderer;
        renderer = chosenSquare.GetComponent<Renderer>();

        for (int i = 0; i < 3; i++)
        {
            renderer.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(flashDuration);
            renderer.material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(flashDuration);
        }
    }
    public void Victory()
    {
        endGameUI.Victory();
        player.PlayVictoryAnim();
    }

    public void Defeat()
    {
        endGameUI.Defeat();

    }
}

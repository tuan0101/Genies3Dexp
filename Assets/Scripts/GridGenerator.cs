using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    const int GRID_SIZE = 5;
    const int SQUARE_SIZE = 2;

    [SerializeField]
    GameObject square1;

    [SerializeField]
    GameObject square2;

    [SerializeField]
    List<GameObject> grid;

    GameObject square;
    WaitForSeconds timeToDisappear = new WaitForSeconds(2f);

    public List<GameObject> Grid { get => grid; set => grid = value; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGrids();
        StartCoroutine(DisableSquare());
    }


    void InitializeGrids()
    {
        int count = 0;
        int x = 0, z;
        for (int i = 0; i < GRID_SIZE; i++)
        {
            z = 0;
            for (int j = 0; j < GRID_SIZE; j++)
            {
                if (i % 2 == 0)
                    square = j % 2 == 0 ? square1 : square2;
                else
                    square = j % 2 == 0 ? square2 : square1;

                grid[count] = Instantiate(square, new Vector3(x, 0, z), Quaternion.identity, this.transform);
                count++;
                z += SQUARE_SIZE;
            }
            x += SQUARE_SIZE;
        }
    }

    // a random square will be disappeared every 2 seconds
    IEnumerator DisableSquare()
    {      
        while(grid.Count > 0)
        {
            yield return timeToDisappear;
            int chosenSquare = Random.Range(0, grid.Count);
            StartCoroutine( FallingSquare(grid[chosenSquare].transform));

            grid.RemoveAt(chosenSquare);
        }        
    }



    IEnumerator FallingSquare(Transform square)
    {
        float time = 0;
        float duration = 1;
        Vector3 targetPosition = square.position - Vector3.up * 10;
        while(time < duration)
        {
            square.position = Vector3.Lerp(square.position, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        square.gameObject.SetActive(false);
    }


}

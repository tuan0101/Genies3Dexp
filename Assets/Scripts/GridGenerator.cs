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
            grid[chosenSquare].SetActive(false);
            grid.RemoveAt(chosenSquare);
        }        
    }
}

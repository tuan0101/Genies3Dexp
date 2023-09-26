using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{   
    const int SQUARE_SIZE = 2;
    int grid_size = 5;

    [SerializeField] GameObject square1;
    [SerializeField] GameObject square2;
    [SerializeField] List<GameObject> grid;

    GameObject square;
    
    #region Public Getters and Setters
    public List<GameObject> Grid { get => grid; set => grid = value; }

    public int Grid_Size { get => grid_size; set => grid_size = value; }
    #endregion

    private void Awake()
    {
        grid_size = StaticVariables.grid_size;
        InitializeGridList();
        InitializeGrids();
    }

    void InitializeGridList()
    {
        GameObject empty = new GameObject();
        for (int i = 0; i < grid_size * grid_size; i++)
        {
            grid.Add(empty);
        }
    }

    void InitializeGrids()
    {
        int count = 0;
        int x = 0, z;
        for (int i = 0; i < grid_size; i++)
        {
            z = 0;
            for (int j = 0; j < grid_size; j++)
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

   
}

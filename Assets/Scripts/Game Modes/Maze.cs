using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    int maze_size = 8;

    [SerializeField]
    GameObject verBlock;

    [SerializeField]
    GameObject horBlock;

    // Start is called before the first frame update
    void Start()
    {
        maze_size = StaticVariables.grid_size * 2 - 1;
        InitializeMaze();
    }

    void InitializeMaze()
    {
        for(int x=1; x<=maze_size; x++)
        {
            // 75% to generate a block in every 2 rows
            int chance = Random.Range(1, 100);
            if(chance <= 50)
            {
                // even x and odd z for horizontal blocks
                int z = Random.Range(0, maze_size/2)*2+1;  // multiple 2 to decrease the chance of overlaping blocks
                Vector3 randomPosition = new Vector3(x/2*2, 1.5f, z);
                Instantiate(horBlock, randomPosition, horBlock.transform.rotation, this.transform);

                // even z and odd x for vertical blocks
                z = Random.Range(0, maze_size / 2) * 2;
                randomPosition = new Vector3(x/2*2+1, 1.5f, z);
                Instantiate(verBlock, randomPosition, verBlock.transform.rotation);
            }
        }
    }
}

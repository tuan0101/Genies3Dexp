using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    const int Maze_SIZE = 8;

    [SerializeField]
    GameObject verBlock;

    [SerializeField]
    GameObject horBlock;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMaze();
    }


    void InitializeMaze()
    {
        for(int x=0; x<=Maze_SIZE; x+=2)
        {

            // 75% to generate a block in every 2 rows
            int chance = Random.Range(1, 100);
            if(chance <= 75)
            {
                int z = Random.Range(0, Maze_SIZE/2)*2;  // multiple 2 to decrease the chance of overlaping blocks
                Vector3 randomPosition = new Vector3(x, 2, z);
                Instantiate(horBlock, randomPosition, horBlock.transform.rotation);

                z = Random.Range(0, Maze_SIZE + 1);
                randomPosition = new Vector3(x, 2, z);
                Instantiate(verBlock, randomPosition, verBlock.transform.rotation);
            }
        }
    }
}

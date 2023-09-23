using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    GameObject[] projectile;
    [SerializeField]
    GridGenerator gridG;


    GameObject Instance;
    Hovl_Laser LaserScript;
    float lazerDuration = 2f;

    int index = 0;
    List<GameObject> grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = gridG.Grid;
        StartCoroutine(FireTripleLasersCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator SpawnLaserCo(Transform firePoint)
    {
        index++;
        //Destroy(Instance);
        // set fire Position on top of the chosen square
        Vector3 firePosition = firePoint.position + Vector3.up * 5;


        // ignite the lazer for 1 second
        //Instance = Instantiate(projectile[index % 3], firePosition, Quaternion.Euler(90,0 ,0));
        Instance = projectile[index % 3];
        Instance.SetActive(true);
        Instance.transform.position = firePosition;
       // Instance.transform.parent = transform;
        LaserScript = Instance.GetComponent<Hovl_Laser>();
        yield return new WaitForSeconds(lazerDuration);
        if (LaserScript) LaserScript.DisablePrepare();
        //Destroy(Instance, 1);
        Instance.SetActive(false);
    }


    void FireLaser()
    {
            int chosenSquare = Random.Range(0, grid.Count);
            Transform firePoint = grid[chosenSquare].transform;
            StartCoroutine(SpawnLaserCo(firePoint));
    }

    IEnumerator FireTripleLasersCo()
    {
        while(grid.Count > 0)
        {
            float timeToSpawnLazer = Random.Range(2, 4);
            yield return new WaitForSeconds(timeToSpawnLazer);

            // the number of lazers being fired is depended on the remaining square
            // the less square remaining, the less lazer

            int numberOfLasers = 3;
            if (grid.Count > 15) numberOfLasers = 3;
            else if (grid.Count > 8) numberOfLasers = 2;
            else if (grid.Count > 0) numberOfLasers = 1;
            for (int i = 0; i < numberOfLasers; i++)
            {
                float timeBetweenLasers = Random.Range(0.1f, .5f);
                FireLaser();
                yield return new WaitForSeconds(timeBetweenLasers);
            }
        }

    }
}

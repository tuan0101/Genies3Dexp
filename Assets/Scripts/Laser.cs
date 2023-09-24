using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    GameObject[] lasers;

    [SerializeField]
    GridGenerator gridG;

    [SerializeField]
    Material flashMaterial;

    int index = 0;
    List<GameObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = gridG.Grid;
        StartCoroutine(FireTripleLasersCo());
    }

    IEnumerator FireLaserCo()
    {
        int chosenSquare = Random.Range(0, grid.Count);
        Transform firePoint = grid[chosenSquare].transform;

        // flash 1s before firing
        StartCoroutine(FlashingSquare(grid[chosenSquare]));
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnLaserCo(firePoint));
    }

    public IEnumerator SpawnLaserCo(Transform firePoint)
    {       
        // set fire Position on top of the chosen square
        Vector3 firePosition = firePoint.position + Vector3.up * 5;
        float lazerDuration = 1.5f;

        // ignite the lazer for 1 second
        //GameObject Instance = Instantiate(lasers[index % 3], firePosition, Quaternion.Euler(90, 0, 0));
        int laserNum = lasers.Length;
        index++;
        GameObject Instance = lasers[index % laserNum]; // alternating lazers in the pool
        Instance.SetActive(true);
        Instance.transform.position = firePosition;
        Instance.transform.parent = transform;
        yield return new WaitForSeconds(lazerDuration);
        Instance.SetActive(false);
    }


    IEnumerator FireTripleLasersCo()
    {
        while(grid.Count > 0)
        {
            float timeToSpawnLazer = Random.Range(3, 5);
            yield return new WaitForSeconds(timeToSpawnLazer);

            // the number of lazers being fired is depended on the remaining square
            // the less square remaining, the less lazer

            int numberOfLasers = 3;
            if (grid.Count > 15) numberOfLasers = 3;
            else if (grid.Count > 8) numberOfLasers = 2;
            else if (grid.Count > 0) numberOfLasers = 1;
            for (int i = 0; i < numberOfLasers; i++)
            {
                float timeBetweenLasers = Random.Range(0.1f, 0.5f);
                StartCoroutine( FireLaserCo());
                //Flash();
                yield return new WaitForSeconds(timeBetweenLasers);
            }
        }

    }

    // Indicating the square is being fired by a lazer
    IEnumerator FlashingSquare(GameObject chosenSquare)
    {
        float flashDuration = 0.25f;
        Renderer renderer;
        renderer = chosenSquare.GetComponent<Renderer>();
        Color color = new Color(.5f, .2f, .2f);

        for (int i = 0; i < 3; i++)
        {
            renderer.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(flashDuration);
            renderer.material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(flashDuration);
        }
    }
}

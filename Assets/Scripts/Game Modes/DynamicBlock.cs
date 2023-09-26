using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBlock : MonoBehaviour
{
    Vector3 originalPosition;
    Vector3 targetPosition;


    public void UpdateTargetPosition(bool isVerticalType)
    {
        int maze_size = StaticVariables.grid_size * 2 - StaticVariables.square_size;
        originalPosition = this.transform.position;

        // avoid moving out of edges
        float z = Mathf.Clamp(originalPosition.z + StaticVariables.square_size, 0, maze_size);
        float x = Mathf.Clamp(originalPosition.x + StaticVariables.square_size, 0, maze_size);
        // even z and odd x for vertical blocks
        if (isVerticalType)
        {
            targetPosition = new Vector3(originalPosition.x, originalPosition.y, z);
        }
        // even x and odd z for horizontal blocks
        else
        {
            targetPosition = new Vector3(x, originalPosition.y, originalPosition.z);

        }
        StartCoroutine(MovingBlock());
    }

    IEnumerator MovingBlock()
    {      
        while (true)
        {
            float time = Mathf.PingPong(Time.time, 1);
            this.transform.position = Vector3.Lerp(originalPosition, targetPosition, time);
            yield return null;
        }
    }
}

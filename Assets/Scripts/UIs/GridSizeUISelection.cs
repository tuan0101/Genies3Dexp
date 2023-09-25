using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSizeUISelection : MonoBehaviour
{
    [SerializeField] Button increaseBtn;
    [SerializeField] Button decreaseBtn;
    [SerializeField] Text gridSize;

    // Start is called before the first frame update
    void Start()
    {
        increaseBtn.onClick.AddListener(OnIncrease);
        decreaseBtn.onClick.AddListener(OnDecrease);
    }

    void OnIncrease()
    {
        int curSize = int.Parse(gridSize.text);
        if (curSize < 9)
            curSize++;
        StaticVariables.grid_size = curSize;
        gridSize.text = curSize.ToString();
    }

    void OnDecrease()
    {
        int curSize = int.Parse(gridSize.text);
        if(curSize > 3)
            curSize--;
        StaticVariables.grid_size = curSize;
        gridSize.text = curSize.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_SpriteSorter : MonoBehaviour
{   
    

    void Start()
    {
        int sortValue = (int)(transform.position.y * -10.0f);
        GetComponent<SpriteRenderer>().sortingOrder = sortValue;
    }
}

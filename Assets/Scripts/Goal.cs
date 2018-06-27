using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public ClearFlower[] clearFlowers;
    public bool isGameClear { get; set; }

    // Use this for initialization
    void Start()
    {
        isGameClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var bloomFlower in clearFlowers)
        {
            //isGameClear = bloomFlower.isBloom;
            if (isGameClear == false)
                break;
        }

        if (isGameClear == true)
        {

        }
    }
}

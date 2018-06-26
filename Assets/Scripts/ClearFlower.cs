using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlower : MonoBehaviour
{

    public bool isBloom { get; set; }

    private WaterReceiver waterReceiver;
    private bool isInWater;

    // Use this for initialization
    void Start()
    {
        isInWater = false;
        waterReceiver = GetComponent<WaterReceiver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInWater == false && waterReceiver.fNowWater > 1f)
        {
            isInWater = true;
        }

        if(isInWater)
        {

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReceiver : MonoBehaviour {

    public float fAmountWater;
    public float fNowWater;
    public float fWaterRatio = 1.0f;// 水の影響度

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if ( fNowWater >= fAmountWater )
        {
            fNowWater = fAmountWater;
        }
        if ( fNowWater < 0f )
        {
            fNowWater = 0f;
        }
    }

    public void RunWater(float fWater)
    {
        fNowWater += fWater / fWaterRatio;
        if ( fNowWater >= fAmountWater )
        {
            fNowWater = fAmountWater;
        }
    }

}

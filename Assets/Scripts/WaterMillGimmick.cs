using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMillGimmick : MonoBehaviour {

    public float maxSpeed;
    public WaterReceiver leftReceiver;
    public WaterReceiver RightReceiver;
    public float fWaterDirection { get; set; }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        fWaterDirection = leftReceiver.fNowWater - RightReceiver.fNowWater;
        GetComponent<Rigidbody>().angularVelocity = new Vector3( 0, 0, fWaterDirection );
        if ( fWaterDirection < 0f )
        {
            RightReceiver.fNowWater = -fWaterDirection;
            leftReceiver.fNowWater = 0f;
        }
        else if ( fWaterDirection > 0f )
        {
            leftReceiver.fNowWater = fWaterDirection;
            RightReceiver.fNowWater = 0f;
        }
        else
        {
            fWaterDirection = 0f;
            RightReceiver.fNowWater = fWaterDirection;
            leftReceiver.fNowWater = fWaterDirection;
        }
	}
}

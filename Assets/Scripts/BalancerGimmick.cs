using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancerGimmick : MonoBehaviour {

    public WaterReceiver leftReceiver;
    public WaterReceiver rightReceiver;
    public Transform transBalancerRod;
    public float fWaterDirection { get; set; }

    private const float fMaxBalanceAngle = 10;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        fWaterDirection = leftReceiver.fNowWater - rightReceiver.fNowWater;
        if ( fWaterDirection < 0f || fWaterDirection > 1f )
        {
            transBalancerRod.rotation = Quaternion.Euler( 0, 0, fWaterDirection );
        }// end if
        else if ( leftReceiver.fNowWater == leftReceiver.fAmountWater && rightReceiver.fNowWater == rightReceiver.fAmountWater )// 水の量が最大量
        {
            leftReceiver.fNowWater = 0f;
            rightReceiver.fNowWater = 0f;
            transBalancerRod.rotation = Quaternion.Euler( 0, 0, 0 );
        }
        else// 水の量が同じ
        {
            transBalancerRod.rotation = Quaternion.Euler( 0, 0, 0 );
        }
        leftReceiver.gameObject.transform.rotation = Quaternion.Euler( 0, 0, 0 );
        rightReceiver.gameObject.transform.rotation = Quaternion.Euler( 0, 0, 0 );

    }
}

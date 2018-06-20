﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour {

    public float amountWater;
    public float runningSpeed;
    public bool isRotateRun = false;

    private ParticleSystem runningWater;
    private Quaternion rotate;
    private bool isRun;

    // Use this for initialization
    void Start ()
    {
        isRun = true;
        runningWater = GetComponent<ParticleSystem>();
        runningWater.Stop();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isRotateRun)
        {
            if (transform.rotation.eulerAngles.x > 70f || transform.rotation.eulerAngles.z > 70f ||
            transform.rotation.eulerAngles.x < -70f || transform.rotation.eulerAngles.z < -70f)
            {

                if (isRun)
                {
                    runningWater.Play();
                    isRun = false;
                }// end if
            }// end if
            else
            {
                if (!isRun)
                {
                    runningWater.Stop();
                    isRun = true;
                }// end if
            }// end else
        }// end if

	}

    // 流水に
    void OnParticleCollision( GameObject obj )
    {
        WaterReceiver receiver = obj.GetComponent<WaterReceiver>();
        // 水のパーティクルに触れるオブジェクトがWaterReceiverコンポーネントを持っている場合に水量を増やす
        if ( receiver != null ) 
        {
            receiver.RunWater( runningSpeed );
        }
    }

}

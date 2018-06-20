using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPipeline : MonoBehaviour
{

    public WaterReceiver receiverPipe;

    //private Vector3 outWaterPosition;
    private ParticleSystem waterParticle;
    private bool isRun;

    // Use this for initialization
    void Start()
    {
        //if (GetComponent<SphereCollider>() != null)
        //    outWaterPosition = GetComponent<SphereCollider>().center;
        //else if (GetComponent<BoxCollider>() != null)
        //    outWaterPosition = GetComponent<BoxCollider>().center;
        //else if (GetComponent<CapsuleCollider>() != null)
        //    outWaterPosition = GetComponent<CapsuleCollider>().center;

        waterParticle = GetComponentInChildren<ParticleSystem>();
        waterParticle.Stop();
        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 水が流れた場合
        if (receiverPipe.fNowWater > 0)
        {
            receiverPipe.fNowWater -= 1;
            if (isRun == false)
            {
                waterParticle.Play();
                isRun = true;
            }// end if
        }// end if
        else if (receiverPipe.fNowWater <= 0 && isRun == true)
        {
            waterParticle.Stop();
            isRun = false;
        }

    }

}

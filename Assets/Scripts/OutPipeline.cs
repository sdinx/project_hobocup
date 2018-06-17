using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPipeline : MonoBehaviour
{

    public InPipeline inPipe;

    //private Vector3 outWaterPosition;
    private ParticleSystem waterParticle;

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
    }

    // Update is called once per frame
    void Update()
    {
        // 水が流れた場合
        if (inPipe.fRunningTime > 0f)
        {
            inPipe.fRunningTime -= 0.1f;
            waterParticle.Play();
        }
        else
            waterParticle.Stop();

    }
}

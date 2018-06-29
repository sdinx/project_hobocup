using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ClearFlower : MonoBehaviour
{

    public GameObject bloomFlower;
    public bool isBloom { get; set; }

    private ParticleSystem particle;
    private WaterReceiver waterReceiver;
    private bool isInWater;
    private Stopwatch stopwatch;

    // Use this for initialization
    void Start()
    {
        isInWater = false;
        stopwatch = new Stopwatch();
        waterReceiver = GetComponent<WaterReceiver>();
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInWater == false && waterReceiver.fNowWater >= 1f)
        {
            isInWater = true;
            stopwatch.Stop();
            stopwatch.Reset();
            stopwatch.Start();
            particle.Play();
        }

        if(isInWater)
        {
            if (stopwatch.ElapsedMilliseconds > 1500)
            {
                particle.Stop();
                // 咲いた花に差し替える
                Instantiate( bloomFlower, GetComponentInParent<Transform>() );

                //gameObject.SetActive( false );
            }
        }

    }
}

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
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                stopwatch.Stop();
                stopwatch.Reset();
                particle.Stop();
                // 咲いた花に差し替える
                GameObject bloomObject = Instantiate( bloomFlower, GetComponent<Transform>() );
                bloomObject.transform.localPosition = new Vector3( 0f, 0f );

                //bloomObject.transform.rotation = Quaternion.Euler( new Vector3( 0f, Random.Range( 0f, 360f ), 0f ) );

                //gameObject.SetActive( false );
            }
        }

    }
}

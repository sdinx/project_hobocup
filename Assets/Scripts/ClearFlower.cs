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
    private Vector3 initPos;

    // Use this for initialization
    void Start()
    {
        isInWater = false;
        stopwatch = new Stopwatch();
        initPos = new Vector3();
        particle = GetComponentInChildren<ParticleSystem>();
        initPos = particle.transform.position;
        //initPos.z += 1000f;
        waterReceiver = GetComponent<WaterReceiver>();
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
                GameObject bloomObject = Instantiate( bloomFlower, GetComponentInParent<Transform>() );
                Vector3 pos = new Vector3( transform.position.x, transform.position.y, transform.position.z );
                transform.localPosition = new Vector3( 0f, 0f, -1000f );
                bloomObject.transform.position = pos;
                GetComponentInChildren<ParticleSystem>().transform.position = initPos;
                //GetComponent<Renderer>().enabled = false;
                //bloomObject.transform.localPosition = new Vector3( bloomObject.transform.localPosition.x, 0f, bloomObject.transform.localPosition.z );
                isBloom = true;
                //bloomObject.transform.rotation = Quaternion.Euler( new Vector3( 0f, Random.Range( 0f, 360f ), 0f ) );

                //gameObject.SetActive( false );
            }
        }

    }
}

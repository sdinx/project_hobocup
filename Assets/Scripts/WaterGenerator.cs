using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour {

    public float amountWater;
    public float runningSpeed;

    private ParticleSystem runningWater;
    private Quaternion rotate;
    private bool isRun;

    // Use this for initialization
    void Start ()
    {
        isRun = true;
        runningWater = GetComponent<ParticleSystem>();
        if (runningWater == null)
            runningWater = GetComponentInChildren<ParticleSystem>();
        runningWater.Stop();
    }
	
	// Update is called once per frame
	void Update ()
    {

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

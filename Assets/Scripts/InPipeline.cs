using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPipeline : MonoBehaviour
{

    public int nRunningTime;// 水が注がれた時間

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay()
    {
        nRunningTime += 2;
    }

}

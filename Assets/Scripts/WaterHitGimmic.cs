using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHitGimmic : MonoBehaviour
{

    public bool isAlways = true;
    public bool isGimmickEnable = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter( Collider other )
    {

        isGimmickEnable = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{

    public bool isGimmickEnable = false;
    public float? fGimmickControll = null;

    private BalancerGimmick balancer = null;
    private WaterMillGimmick waterMill = null;

    // Use this for initialization
    void Start()
    {
        balancer = GetComponentInChildren<BalancerGimmick>();
        waterMill = GetComponentInChildren<WaterMillGimmick>();
    }

    // Update is called once per frame
    void Update()
    {
        if(balancer!=null)
        {
            fGimmickControll = balancer.fWaterDirection;
        }// end if
        else if(waterMill!=null)
        {
            fGimmickControll = waterMill.fWaterDirection;
        }// end else if

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateGimmick : MonoBehaviour
{

    public WaterMillGimmick gimmick;
    public float fOpenHeight;

    private Vector3 currentPos;

    // Use this for initialization
    void Start()
    {
        currentPos = new Vector3();
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gimmick.fWaterDirection > 0f)
        {// 開放
            transform.position = Vector3.Lerp( transform.position, new Vector3( currentPos.x, currentPos.y, currentPos.z ), Time.deltaTime * gimmick.fWaterDirection );
        }// end if
        else if (gimmick.fWaterDirection < 0f)
        {// 閉鎖
            transform.position = Vector3.Lerp( transform.position, new Vector3( currentPos.x, currentPos.y + fOpenHeight, currentPos.z ), Time.deltaTime * -gimmick.fWaterDirection );
        }// end else if

    }
}

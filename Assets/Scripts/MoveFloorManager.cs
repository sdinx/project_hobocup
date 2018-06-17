using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorManager : MonoBehaviour
{

    public float fSpeed = 1f;
    public float fHeightLimit = 11f;
    public float fLowLimit = -3f;
    public float fSwitchX = 3f;

    // Use this for initialization
    void Start()
    {
        fHeightLimit += transform.localPosition.y;
        fLowLimit += transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

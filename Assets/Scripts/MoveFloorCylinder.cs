using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorCylinder : MonoBehaviour
{

    public Gimmick gimmick = null;

    private float fRotate;

    // Use this for initialization
    void Start()
    {
        fRotate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gimmick == null || gimmick.isGimmickEnable)
        {
            transform.localRotation = Quaternion.Euler( transform.localRotation.eulerAngles.x, ++fRotate, transform.localRotation.eulerAngles.z );
        }// end if

    }
}

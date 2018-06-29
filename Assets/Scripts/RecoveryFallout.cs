using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryFallout : MonoBehaviour
{

    public Transform RecoverPosition;
    public float fHeight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fHeight)
        {
            transform.position = RecoverPosition.position;
            if (GetComponent<CarryCup>().playerState != PlayerState.None && GetComponent<CarryCup>().playerState != PlayerState.None)
                transform.position = new Vector3( transform.position.x, transform.position.y, 252f );
        }
    }
}

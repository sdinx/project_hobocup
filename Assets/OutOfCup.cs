using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCup : MonoBehaviour
{

    public CarryCup player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay( Collider collider )
    {
        if (collider.CompareTag( "Cup" ) && ( player.playerState == PlayerState.None || player.playerState == PlayerState.Return ))
        {
            collider.GetComponent<Transform>().position = collider.GetComponentInParent<Transform>().position;
        }

    }

}

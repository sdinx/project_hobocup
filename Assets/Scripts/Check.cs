using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{

    public CarryCup carrier;
    public Material material { get; set; }

    private void Awake()
    {
        material = new Material( GetComponentInChildren<MeshRenderer>().material);
    }


    // Use this for initialization
    void Start()
    {
        GetComponentInChildren<MeshRenderer>().material = material;

    }

    // Update is called once per frame
    void Update()
    {
        var playerState = carrier.playerState;
        if (playerState == PlayerState.Carry)
            GetComponentInChildren<MeshRenderer>().material = material;

    }
    void OnTriggerStay( Collider collider )
    {

        var playerState = carrier.playerState;
        if (!collider.CompareTag( "Cup" ))
        {
            if (playerState == PlayerState.None || playerState == PlayerState.Return)
            {
                GetComponentInChildren<MeshRenderer>().material = material;
                return;

            }
        }
        else if (playerState != PlayerState.None || playerState != PlayerState.Return)
        {
            //GetComponentInChildren<MeshRenderer>().material = material;
            return;
        }
        GetComponentInChildren<MeshRenderer>().material = collider.GetComponent<HaveMaterial>().material;


    }

    void OnTriggerExit(Collider collider)
    {


        GetComponentInChildren<MeshRenderer>().material = material;



    }

}

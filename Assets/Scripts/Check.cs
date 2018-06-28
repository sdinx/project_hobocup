using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{

    public CarryCup carrier;
    public Material material;

    private void Awake()
    {
        material = new Material(transform.GetChild(0).gameObject.gameObject.GetComponent<MeshRenderer>().material);
    }


    // Use this for initialization
    void Start()
    {
        transform.GetChild(0).gameObject.gameObject.GetComponent<MeshRenderer>().material = material;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider collider)
    {
        transform.GetChild(0).gameObject.gameObject.GetComponent<MeshRenderer>().material = collider.GetComponent<HaveMaterial>().material;

        var playerState = carrier.playerState;
        if (collider.CompareTag("Cup"))
        {
            if (playerState != PlayerState.None || playerState != PlayerState.Return)
            {
                transform.GetChild(0).gameObject.gameObject.GetComponent<MeshRenderer>().material = material;
                return;

            }
        }
        else if(playerState == PlayerState.None || playerState == PlayerState.Return)
        {
            transform.GetChild(0).gameObject.gameObject.GetComponent<MeshRenderer>().material = material;
            return;
        }
                

    }

    void OnTriggerExit(Collider collider)
    {


        transform.GetChild(0).gameObject.gameObject.GetComponent<MeshRenderer>().material = material;



    }

}

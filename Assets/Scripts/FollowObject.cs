using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform followObject;
    private Vector3 distance;

	// Use this for initialization
	void Start ()
    {
        distance = transform.position - followObject.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = followObject.position + distance;
	}
}

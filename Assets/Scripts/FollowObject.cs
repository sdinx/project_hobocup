using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform followObject;
    public Vector3 distance { set; get; }

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

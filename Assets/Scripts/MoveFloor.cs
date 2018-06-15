using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour {

    public Vector3 movePower;
    public Vector3 moveLimit;

    private Vector3 currentPos;
    private bool moveDirection;

	// Use this for initialization
	void Start ()
    {
        currentPos = new Vector3();
        currentPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {


    }
}

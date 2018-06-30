using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatFloor_4 : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 defaultPos;
    [Range(0, 30)] public float height;
    [Range(0, 10)] public int speed;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        defaultPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        rb.MovePosition(new Vector3(defaultPos.x, defaultPos.y + Mathf.PingPong(Time.time*speed, height), defaultPos.z)); ;
    }
}

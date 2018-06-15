using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector2 movePower;
    public float maxPower;

    private bool isPlayerDirection = true;// true: 右向き,  false: 左向き
    private bool isJumpReady = false;
    private bool isAnimation = false;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        GetComponent<SphereCollider>().isTrigger = true;
        anim = GetComponent<Animator>();
        anim.CrossFade( "Standby", 0 );
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void FixedUpdate()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        Vector3 move = Vector3.zero;
        move.x = Input.GetAxis( "Horizontal" ) * movePower.x;
        if ( isAnimation == false && move.x != 0f )
        {
            anim.CrossFade( "Walk", 0 );
            isAnimation = true;
        }
        else if ( isAnimation && move.x == 0f )
        {
            anim.CrossFade( "Standby", 0 );
            isAnimation = false;
        }
        if ( move.x > 0f )
            isPlayerDirection = true;
        else
            isPlayerDirection = false;

        if ( isJumpReady && GetComponentInChildren<CarryCup>().playerState == PlayerState.Return && Input.GetButtonDown( "Jump" ) ) { }
        if( Input.GetButtonDown( "Jump" ))
        {
            move.y = movePower.y;
            isJumpReady = false;
            anim.CrossFade( "Standby", 0 );
        }

        rigid.AddForce( move, ForceMode.Force );

    }

    void OnTriggerEnter ()
    {
        isJumpReady = true;
    }

    void OnTriggerExit ()
    {
        isJumpReady = false;
    }
}

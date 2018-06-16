using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector2 movePower;
    public float maxPower;

    private bool isPlayerDirection = true;// true: 右向き,  false: 左向き
    private bool isJumpReady = false;
    private Animator anim;
    private PlayerState playerState;

	// Use this for initialization
	void Start ()
    {
        playerState = GetComponentInChildren<CarryCup>().playerState;
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
        Vector3 move = Vector3.zero;
        move.x = Input.GetAxis( "Horizontal" ) * movePower.x;

        if (isJumpReady && move.x != 0f)
        {
            anim.CrossFade("Walk", 0);

            // プレイヤーの向き
            if (move.x > 0f)
                isPlayerDirection = true;
            else
                isPlayerDirection = false;
        }
        else if (isJumpReady && move.x == 0f)
        {
            anim.CrossFade( "Standby", 0 );
        }

        if (isJumpReady && (playerState == PlayerState.Return || playerState == PlayerState.None) && Input.GetButtonDown("Jump")) 
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, movePower.y, 0), ForceMode.Force);
            isJumpReady = false;
            anim.CrossFade( "Jump", 0 );
        }

        transform.position += move;

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

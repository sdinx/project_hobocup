using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 movePower;
    public float maxPower;
    public bool isControll { get; set; }
    public bool isPlayerDirection { get; set; }// true: 右向き,  false: 左向き

    private bool isJumpReady = false;
    private Animator anim;
    private PlayerState playerState;

    // Use this for initialization
    void Start()
    {
        isPlayerDirection = true;
        isControll = true;
        playerState = GetComponentInChildren<CarryCup>().playerState;
        GetComponent<SphereCollider>().isTrigger = true;
        anim = GetComponent<Animator>();
        anim.CrossFade( "Standby", 0 );
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDirection)
            transform.rotation = Quaternion.Euler( 0, 90, 0 );
        else
            transform.rotation = Quaternion.Euler( 0, -90, 0 );
    }

    void FixedUpdate()
    {
        if (isControll == false)
            return;


        Vector3 move = Vector3.zero;
        move.x = Input.GetAxis( "Horizontal" ) * movePower.x;
        playerState = GetComponentInChildren<CarryCup>().playerState;

        if ( move.x != 0f)
        {
            if (isJumpReady == true)
                if (( playerState == PlayerState.Return || playerState == PlayerState.None ))
                    anim.CrossFade( "Walk", 0 );
                else
                    anim.CrossFade( "CarryingWalk", 0 );

            // プレイヤーの向き
            if (move.x > 0f)
                isPlayerDirection = true;
            else
                isPlayerDirection = false;
        }
        else if ( move.x == 0f)
        {
            if (isJumpReady == true)
                anim.CrossFade( "Standby", 0 );
        }

        if (isJumpReady && ( playerState == PlayerState.Return || playerState == PlayerState.None ) && Input.GetButtonDown( "Jump" ))
        {
            GetComponent<Rigidbody>().AddForce( new Vector3( 0, movePower.y, 0 ), ForceMode.Force );
            isJumpReady = false;
        }
        transform.position += move;

        if (isJumpReady == false && Input.GetButtonDown( "Jump" ))
            anim.CrossFade( "Jump", 0 );

    }

    void OnTriggerStay()
    {
        isJumpReady = true;
    }

    void OnTriggerExit()
    {
        isJumpReady = false;
    }
}

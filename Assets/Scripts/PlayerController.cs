using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 movePower;
    public float maxPower;
    public bool isControll { get; set; }
    public bool isPlayerDirection { get; set; }// true: 右向き,  false: 左向き
    public bool isJumpReady { get; set; }

    private Animator anim;
    private PlayerState playerState;

    void Awake()
    {
        GetComponent<Animator>().SetFloat( "Offset", 0.3615f );
    }

    // Use this for initialization
    void Start()
    {
        isJumpReady = false;
        isPlayerDirection = true;
        isControll = true;
        playerState = GetComponentInChildren<CarryCup>().playerState;
        GetComponent<SphereCollider>().isTrigger = true;
        anim = GetComponent<Animator>();
        anim.SetFloat( "Offset", 0.3615f );
        anim.CrossFade( "Standby", 0 );
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != PlayerState.Carry)
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

        if (playerState != PlayerState.RunWater)
            if (move.x != 0f)
            {
                if (isJumpReady == true)
                    if (( playerState == PlayerState.Return || playerState == PlayerState.None ))
                        anim.CrossFade( "Walk", 0 );
                    else
                        anim.CrossFade( "CarryingWalk", 0 );

                // プレイヤーの向き
                if (move.x > 0f)
                    isPlayerDirection = true;
                else if (move.x < 0f)
                    isPlayerDirection = false;

            }// end if
            else if (move.x == 0f)
            {
                if (isJumpReady == true && playerState != PlayerState.Carrying)
                {
                    anim.SetFloat( "Offset", 0.361f );
                    anim.CrossFade( "Standby", 0 );
                }// end if
                else if (playerState == PlayerState.Carrying)
                {
                    anim.SetFloat( "CarryingOffset", 0.518f );
                    anim.CrossFade( "CarryingStandby", 0 );
                }// end else if
            }// end if

        if (isJumpReady && ( playerState == PlayerState.Return || playerState == PlayerState.None ) && Input.GetButtonDown( "Jump" ))
        {
            GetComponent<Rigidbody>().AddForce( new Vector3( 0, movePower.y, 0 ), ForceMode.Force );
            isJumpReady = false;
        }
        transform.position += move;

        if (isJumpReady == false && Input.GetButtonDown( "Jump" ))
        {
            anim.CrossFade( "Jump", 0 );
        }// end if

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

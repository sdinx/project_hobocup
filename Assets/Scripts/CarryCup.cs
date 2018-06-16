using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum PlayerState
{
    None = 0,
    Carry,
    Carrying,
    RunWater,
    Return
};

public class CarryCup: MonoBehaviour
{

    public Transform carrier;
    public Vector3 carryPosition;
    public PlayerState playerState;

    private Transform target;
    private Vector3 targetDirection;
    private float currentTime;
    private Vector3 setEuler;
    private Stopwatch stopWatch;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponentInParent<Animator>();
        setEuler = new Vector3();
        stopWatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update ()
    {
        switch(playerState)
        {
            case PlayerState.Carry:
            {
                playerState = CarryState();
            }
            break;
            case PlayerState.Carrying:
            {
                playerState = CarryingState();
            }
            break;
            case PlayerState.Return:
            {
                playerState = ReturnState();
            }
            break;
            case PlayerState.RunWater:
            {
                playerState = RunWaterState();
            }
            break;
            default: break;/* NOTHING */
        }// end switch

    }

    // 範囲内のコップの持ち上げ
    void OnTriggerStay ( Collider obj )
    {

        if ( playerState != PlayerState.Carrying && playerState != PlayerState.Carry && Input.GetButtonDown( "Catch" ) && obj.gameObject.CompareTag( "Cup" ) )
        {
            target = obj.gameObject.transform;

            // 対象との差
            var head = obj.transform.position - carrier.position;
            head.y = 0;

            // 正規化
            float distance = head.magnitude;
            var direction = head / distance;

            targetDirection = direction;
            playerState = PlayerState.Carry;
            currentTime = Time.deltaTime;
            stopWatch.Start();
        }
    }

    void OnTriggerExit ()
    {

    }

    public PlayerState CarryState()
    {
        PlayerState state = playerState;
        carrier.position = Vector3.Slerp( carrier.position, target.position, Time.deltaTime );
        target.position = Vector3.Slerp( target.position, carryPosition + carrier.position, Time.deltaTime );

        if ( stopWatch.ElapsedMilliseconds > 1500 )
        {
            stopWatch.Stop();
            stopWatch.Reset();
            state = PlayerState.Carrying;
        }
        else if ( Input.GetButtonDown( "Jump" ) )
        {
            stopWatch.Stop();
            stopWatch.Reset();
            state = PlayerState.Return;
        }

        return state;
    }

    public PlayerState CarryingState ()
    {
        PlayerState state = playerState;

        var followObject = target.gameObject.GetComponent<FollowObject>();
        if ( followObject == null )
        {
            followObject = target.gameObject.AddComponent<FollowObject>();
            followObject.followObject = carrier;
        }

        if ( Input.GetButtonDown( "Fire3" ) )
        {
            state = PlayerState.Return;
        }
        else if ( Input.GetButtonDown( "Catch" ) )
        {
            state = PlayerState.RunWater;
            setEuler = target.gameObject.transform.rotation.eulerAngles;
            setEuler.z = -100;
        }

        return state;
    }

    public PlayerState ReturnState ()
    {
        PlayerState state = playerState;

        var followObject = target.gameObject.GetComponent<FollowObject>();
        if ( followObject != null )
            Destroy( followObject );

        carrier.position = Vector3.Lerp( carrier.position, new Vector3( carrier.position.x, carrier.position.y, 250 ), Time.deltaTime );

        return state;
    }

    public PlayerState RunWaterState ()
    {
        PlayerState state = playerState;

        //target.gameObject.transform.rotation = Quaternion.Euler(setEuler);

        target.gameObject.transform.rotation = Quaternion.Slerp(target.gameObject.transform.rotation, Quaternion.Euler(setEuler), Time.deltaTime );

        if ( Input.GetButtonDown( "Fire3" ) )
        {
            state = PlayerState.Carrying;
        }


        return state;
    }

}
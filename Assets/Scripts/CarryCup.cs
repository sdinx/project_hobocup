using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum PlayerState
{
    None = 0,
    Carry,
    Lever,
    Carrying,
    RunWater,
    Return
};

public class CarryCup : MonoBehaviour
{

    public Transform carrier;
    public Vector3 carryPosition;
    public PlayerState playerState;

    private Transform target;
    private Vector3 targetDirection;
    private float currentTime;
    private Vector3 setEuler;
    private Stopwatch stopWatch;
    private bool isTimerStarted;
    private Animator animator;
    private PlayerController playerController;
    private float fPlayerReturnZ;
    private Vector3 distancedR;
    private Vector3 distancedL;

    // Use this for initialization
    void Start()
    {
        distancedR = new Vector3();
        distancedL = new Vector3();
        fPlayerReturnZ = 0f;
        target = null;
        isTimerStarted = false;
        playerController = GetComponentInParent<PlayerController>();
        fPlayerReturnZ = playerController.GetComponentInParent<Transform>().position.z;
        animator = GetComponentInParent<Animator>();
        setEuler = new Vector3();
        stopWatch = new Stopwatch();
    }// end Start()

    private void FixedUpdate()
    {
        if (playerState != PlayerState.Carrying && playerState != PlayerState.Carry && Input.GetButtonDown( "Catch" ))
            GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BoxCollider>().enabled = false;

        switch (playerState)
        {
            case PlayerState.Carry:
            {
                playerState = CarryState();
            }
            break;
            case PlayerState.Lever:
            {
                playerState = LeverState();
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
            default:
                /* NOTHING */
                break;
        }// end switch

        if (target != null)
        {
            var followObject = target.gameObject.GetComponent<FollowObject>();
            if (followObject != null && playerState != PlayerState.RunWater)
                if (playerController.isPlayerDirection)
                {
                    followObject.distance = distancedR;
                    target.localRotation = Quaternion.Euler( target.localRotation.eulerAngles.x, 0, target.localRotation.eulerAngles.z );
                }
                else
                {
                    followObject.distance = distancedL;
                    target.localRotation = Quaternion.Euler( target.localRotation.eulerAngles.x, -180, target.localRotation.eulerAngles.z );
                }
        }// end if

    }// end Update()

    // 範囲内のコップの持ち上げ
    void OnTriggerStay( Collider obj )
    {
        if (( playerState == PlayerState.None || playerState == PlayerState.Return ))
        {
            if (obj.gameObject.CompareTag( "Cup" ))
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
                playerController.isControll = false;
                currentTime = Time.deltaTime;
                stopWatch.Start();

                // 重力の停止
                target.GetComponent<Rigidbody>().useGravity = false;
            }
            else if(obj.gameObject.CompareTag( "Lever" ))
            {
                target = obj.gameObject.transform;
                playerState = PlayerState.Lever;
                playerController.isControll = false;
                stopWatch.Start();
                target.GetComponentInChildren<ParticleSystem>().Play();
            }
        }

    }// end OnTriggerStay()

    void OnTriggerExit()
    {

    }// end OnTriggerExit()

    public PlayerState CarryState()
    {
        PlayerState state = playerState;
        carrier.position = Vector3.Slerp( carrier.position, target.position - new Vector3( carryPosition.x, 0f, carryPosition.z ), Time.deltaTime );

        if (stopWatch.ElapsedMilliseconds > 300)
        {
            playerController.GetComponent<Animator>().CrossFade( "CatchCup", 0 );
            target.position = Vector3.Lerp( target.position, new Vector3( target.position.x, target.position.y + carryPosition.y, target.position.z ), Time.deltaTime );
        }// end if

        if (stopWatch.ElapsedMilliseconds > 1500)
        {
            stopWatch.Stop();
            stopWatch.Reset();
            state = PlayerState.Carrying;
            playerController.isControll = true;
        }// end if
        else if (playerController.isControll && Input.GetButtonDown( "Jump" ))
        {
            stopWatch.Stop();
            stopWatch.Reset();
            state = PlayerState.Return;
            playerController.isControll = true;
        }// end else if

        return state;
    }// end CarryState()
    
    public PlayerState LeverState()
    {
        PlayerState state = playerState;

        target.GetComponent<Gimmick>().isGimmickEnable = true;

        if (stopWatch.ElapsedMilliseconds < 800)
        {
            carrier.position = Vector3.Slerp( carrier.position, target.position - new Vector3( carryPosition.x + 1.5f, 0f, carryPosition.z ), Time.deltaTime * 3f );
        }

        if (stopWatch.ElapsedMilliseconds > 300)
        {
            playerController.GetComponent<Animator>().CrossFade( "DrawWater", 0 );

        }// end if
        
        if (Input.GetButtonDown( "Fire3" ))
        {
            target.GetComponent<Gimmick>().isGimmickEnable = false;
            target.GetComponentInChildren<ParticleSystem>().Stop();
            stopWatch.Stop();
            stopWatch.Reset();
            state = PlayerState.Return;
            playerController.isControll = true;
        }// end else if

        carrier.rotation = Quaternion.Euler( 0, 90, 0 );

        return state;
    }

    public PlayerState CarryingState()
    {
        var receiver = target.GetComponent<WaterReceiver>();
        PlayerState state = playerState;

        // 水量を計算
        playerController.fCupWeight = receiver.fNowWater / receiver.fAmountWater;
        if (playerController.fCupWeight > 0.5f)
            playerController.fCupWeight = 0.5f;

        var followObject = target.gameObject.GetComponent<FollowObject>();
        if (followObject == null)
        {
            followObject = target.gameObject.AddComponent<FollowObject>();
            followObject.followObject = carrier;
            distancedR = followObject.distance;
            distancedL = followObject.distance;
            distancedR.x += 0.5f;
            distancedL.x -= 0.5f;
            distancedR.y -= 0.5f;
            distancedL.y -= 0.5f;
        }

        if (playerController.isControll && playerController.isJumpReady && Input.GetButtonDown( "Fire3" ))
        {
            state = PlayerState.Return;
        }
        else if (playerController.isControll && Input.GetButtonDown( "Catch" ))
        {
            playerController.GetComponent<Animator>().SetFloat( "Speed", 1f );
            playerController.GetComponent<Animator>().CrossFade( "PutWater", 0 );
            playerController.isControll = false;
            target.GetComponentInChildren<ParticleSystem>().Play();
            state = PlayerState.RunWater;
            setEuler = target.gameObject.transform.rotation.eulerAngles;
            setEuler.z = -100;
        }

        return state;
    }// end CarryingState()

    public PlayerState ReturnState()
    {
        PlayerState state = playerState;

        var followObject = target.gameObject.GetComponent<FollowObject>();
        if (followObject != null)
        {
            target.GetComponent<Rigidbody>().useGravity = true;
            Destroy( followObject );
        }

        carrier.position = Vector3.Lerp( carrier.position, new Vector3( carrier.position.x, carrier.position.y, fPlayerReturnZ ), Time.deltaTime );

        return state;
    }// end ReturnState()

    public PlayerState RunWaterState()
    {
        var receiver = target.GetComponent<WaterReceiver>();
        PlayerState state = playerState;

        if (isTimerStarted == false)
        {
            target.gameObject.transform.rotation = Quaternion.Slerp( target.gameObject.transform.rotation, Quaternion.Euler( setEuler ), Time.deltaTime );
            playerController.isControll = false;
        }

        if (receiver.fNowWater > 0)
        {
            receiver.fNowWater -= 0.1f;
        }
        else
        {
            target.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (Input.GetButtonDown( "Fire3" ))
        {
            if (isTimerStarted == false)
            {// コップの角度を戻す用のタイマー
                isTimerStarted = true;
                stopWatch.Start();
                playerController.GetComponent<Animator>().SetFloat( "Speed", -1f );
                playerController.GetComponent<Animator>().CrossFade( "PutWater", 0, 0, 1f );
            }// end if

        }

        if (isTimerStarted == true)
        {
            if (stopWatch.ElapsedMilliseconds > 300)
                target.GetComponentInChildren<ParticleSystem>().Stop();

            if (stopWatch.ElapsedMilliseconds > 1200)
            {
                playerController.isControll = true;
                isTimerStarted = false;
                stopWatch.Stop();
                stopWatch.Reset();
                state = PlayerState.Carrying;
                playerController.GetComponent<Animator>().SetFloat( "Speed", 1f );
            }// end if
            else
            {
                if (playerController.isPlayerDirection)
                    target.gameObject.transform.rotation = Quaternion.Lerp( target.gameObject.transform.rotation, Quaternion.Euler( 0, 0, 0 ), Time.deltaTime * 4 );
                else
                    target.gameObject.transform.rotation = Quaternion.Lerp( target.gameObject.transform.rotation, Quaternion.Euler( 0, -180, 0 ), Time.deltaTime * 4 );
            }
        }

        return state;
    }// end RunWaterState()

}
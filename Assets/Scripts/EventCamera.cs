using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//--------------------------------------------------------------------
// イベント発生時にカメラをフォローさせる
//--------------------------------------------------------------------
public class EventCamera : MonoBehaviour
{

    public Camera useCamera;
    public PlayerController playerController;
    public GameObject followObject;
    public long lFollowTime;
    public float fSpeed = 1f;
    public Vector3 offset;
    public Vector3 rotateOffset;
    public GameObject fadeObject;

    private Stopwatch stopwatch;
    private Vector3 currentPos;
    private Quaternion currentQuat;
    private bool isFollowing;
    private Vector2 camXY;
    private Vector2 camWH;

    // Use this for initialization
    void Start()
    {
        currentPos = new Vector3();
        currentQuat = new Quaternion();
        stopwatch = new Stopwatch();
        isFollowing = false;

        camXY = new Vector2( 0, 0.1f );
        camWH = new Vector2( 1f, 0.76f );
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            Vector3 move = new Vector3( followObject.transform.position.x + offset.x, followObject.transform.position.y + offset.y, useCamera.transform.position.z + offset.z );
            useCamera.transform.position = Vector3.Slerp( useCamera.transform.position, move, Time.deltaTime * fSpeed );
            useCamera.transform.rotation = Quaternion.Slerp( useCamera.transform.rotation, Quaternion.Euler( rotateOffset ), Time.deltaTime * fSpeed );

            //useCamera.rect = new Rect( 
            //    Vector2.Lerp( new Vector2( useCamera.rect.x, useCamera.rect.y ), camXY, Time.deltaTime ), 
            //    Vector2.Lerp( new Vector2( useCamera.rect.width, useCamera.rect.height ), camWH, Time.deltaTime ) );

            if (stopwatch.ElapsedMilliseconds >= lFollowTime)
            {
                useCamera.GetComponent<FollowObject>().enabled = true;
                isFollowing = false;
                enabled = false;
                playerController.isControll = true;
                stopwatch.Stop();
                stopwatch.Reset();
                useCamera.transform.position = currentPos;
                useCamera.transform.rotation = currentQuat;
                //useCamera.rect = new Rect( 0f, 0f, 1f, 1f );
                fadeObject.SetActive( false );
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Gimmick>().isGimmickEnable && isFollowing == false)
        {
            currentPos= useCamera.transform.position;
            currentQuat = useCamera.transform.rotation;
            //useCamera.orthographicSize = offset.z;
            useCamera.GetComponent<FollowObject>().enabled = false;
            stopwatch.Start();
            playerController.isControll = false;
            isFollowing = true;
            fadeObject.SetActive( true );
        }

    }

}

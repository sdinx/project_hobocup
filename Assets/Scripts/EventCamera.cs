using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//--------------------------------------------------------------------
// イベント発生時にカメラをフォローさせる
//--------------------------------------------------------------------
public class EventCamera : MonoBehaviour
{

    public Camera cloneCamera;
    public PlayerController playerController;
    public GameObject followObject;
    public long lFollowTime;
    public float fSpeed = 1f;
    public Vector3 offset;
    public Vector3 rotateOffset;

    private Stopwatch stopwatch;
    private Vector3 currentPos;
    private Quaternion currentQuat;
    private bool isFollowing;

    // Use this for initialization
    void Start()
    {
        currentPos = new Vector3();
        currentQuat = new Quaternion();
        stopwatch = new Stopwatch();
        isFollowing = false;
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            Vector3 move = new Vector3( followObject.transform.position.x + offset.x, followObject.transform.position.y + offset.y, cloneCamera.transform.position.z + offset.z );
            cloneCamera.transform.position = Vector3.Slerp( cloneCamera.transform.position, move, Time.deltaTime * fSpeed );
            cloneCamera.transform.rotation = Quaternion.Slerp( cloneCamera.transform.rotation, Quaternion.Euler( rotateOffset ), Time.deltaTime * fSpeed );

            if (stopwatch.ElapsedMilliseconds >= lFollowTime)
            {
                cloneCamera.GetComponent<FollowObject>().enabled = true;
                isFollowing = false;
                enabled = false;
                playerController.isControll = true;
                stopwatch.Stop();
                stopwatch.Reset();
                cloneCamera.transform.position = currentPos;
                cloneCamera.transform.rotation = currentQuat;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Gimmick>().isGimmickEnable && isFollowing == false)
        {
            currentPos= cloneCamera.transform.position;
            currentQuat = cloneCamera.transform.rotation;
            //cloneCamera.orthographicSize = offset.z;
            cloneCamera.GetComponent<FollowObject>().enabled = false;
            stopwatch.Start();
            playerController.isControll = false;
            isFollowing = true;
        }

    }

}

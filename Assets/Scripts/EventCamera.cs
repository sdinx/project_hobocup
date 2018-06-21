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
    public long fFollowTime;

    private Stopwatch stopwatch;
    private Camera camera;
    private Vector3 currentPos;
    private bool isFollowing;

    // Use this for initialization
    void Start()
    {
        currentPos = new Vector3();
        camera = new Camera();
        camera = Instantiate( cloneCamera );
        stopwatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Gimmick>().isGimmickEnable && isFollowing == false)
        {
            currentPos= cloneCamera.transform.position;
            //camera.transform.position = cloneCamera.transform.position;
            stopwatch.Start();
            //cloneCamera.enabled = false;
            //camera.enabled = true;
            playerController.isControll = false;
            isFollowing = true;
        }

        if (isFollowing)
        {
            if (stopwatch.ElapsedMilliseconds > fFollowTime)
            {
                isFollowing = false;
                //camera.enabled = false;
                //cloneCamera.enabled = true;
                stopwatch.Stop();
                stopwatch.Reset();
                cloneCamera.transform.position = currentPos;
            }

            cloneCamera.transform.position = Vector3.Slerp( currentPos, followObject.transform.position, Time.deltaTime );
        }

    }

}

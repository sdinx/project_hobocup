using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{

    public float fSpeed = 1f;
    public Vector3 moveLimit;
    public bool moveDirection;

    private Vector3 currentPos;
    private bool isMove;
    private float angle;

    // Use this for initialization
    void Start()
    {
        isMove = true;
        if (moveDirection)
            isMove = false;
        angle = 180f;
        currentPos = new Vector3();
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3( 0, 0, 0 );

        if (moveDirection)
        {
            move.y = -0.1f * fSpeed;
            if (transform.position.y <= 0f)
            {
                if (!isMove)
                    angle = 0;
                isMove = true;
                if (transform.position.y <= -3f)
                    moveDirection = false;
            }
        }
        else
        {
            move.y = 0.1f * fSpeed;
            if (transform.position.y >= 8f)
            {
                if (isMove)
                    angle = 0;
                isMove = false;
                if (transform.position.y >= 11f)
                    moveDirection = true;
            }
        }

        if (isMove)
        {
            if (angle < 180f)
            {
                angle += 3f * fSpeed;
                transform.rotation = Quaternion.Euler( 0, 0, angle );
            }
            transform.position = Vector3.Lerp( transform.position, new Vector3( currentPos.x + 4f, transform.position.y, transform.position.z ), Time.deltaTime * ( 2 * fSpeed ) );
        }
        else
        {
            if (angle < 180f)
            {
                angle += 3f * fSpeed;
                transform.rotation = Quaternion.Euler( 0, 0, 180 + angle );
            }
            transform.position = Vector3.Lerp( transform.position, new Vector3( currentPos.x, transform.position.y, transform.position.z ), Time.deltaTime * ( 2 * fSpeed ) );
        }

        transform.position += move;
    }
}

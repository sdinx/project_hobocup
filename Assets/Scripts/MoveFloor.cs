using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{

    public Gimmick gimmick = null;
    public bool isLeftRight;
    
    private float fHeightLimit;
    private float fLowLimit;
    private float fSwitchX;
    private Vector3 currentPos;
    private bool isMove;
    private float angle;

    // Use this for initialization
    void Start()
    {
        fHeightLimit = GetComponentInParent<MoveFloorManager>().fHeightLimit;
        fLowLimit = GetComponentInParent<MoveFloorManager>().fLowLimit;
        fSwitchX = GetComponentInParent<MoveFloorManager>().fSwitchX;

        currentPos = new Vector3();
        currentPos = transform.localPosition;

        if (isLeftRight)
        {
            isMove = false;
        }// end if
        else
        {
            isMove = true;
            currentPos.x -= 4f;
        }// end else

        angle = 180f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gimmick == null || gimmick.isGimmickEnable || gimmick.fGimmickControll != 0f)
        {
            float fSpeed = GetComponentInParent<MoveFloorManager>().fSpeed;
            Vector3 move = new Vector3( 0, 0, 0 );

            if (isLeftRight)
            {
                move.y = -0.1f * fSpeed;
                if (transform.position.y <= fLowLimit + fSwitchX) 
                {
                    if (!isMove)
                        angle = 0;
                    isMove = true;

                    if (transform.position.y <= fLowLimit)
                        isLeftRight = false;
                }// end if
            }// end if
            else
            {
                move.y = 0.1f * fSpeed;
                if (transform.position.y >= fHeightLimit - fSwitchX) 
                {
                    if (isMove)
                        angle = 0;

                    isMove = false;
                    if (transform.position.y >= fHeightLimit)
                        isLeftRight = true;
                }// end if
            }// end else

            if (isMove)
            {
                if (angle < 180f)
                {
                    angle += 3f * fSpeed;
                    transform.localRotation = Quaternion.Euler( 0, 0, angle );
                }// end if
                transform.localPosition = Vector3.Lerp( transform.localPosition, new Vector3( currentPos.x + 4.5f, transform.localPosition.y, transform.localPosition.z ), Time.deltaTime * ( 2 * fSpeed ) );
            }// end if
            else
            {
                if (angle < 180f)
                {
                    angle += 3f * fSpeed;
                    transform.localRotation = Quaternion.Euler( 0, 0, 180 + angle );
                }
                transform.localPosition = Vector3.Lerp( transform.localPosition, new Vector3( currentPos.x, transform.localPosition.y, transform.localPosition.z ), Time.deltaTime * ( 2 * fSpeed ) );
            }// end else

            transform.position += move;
        }// end if

    }
}

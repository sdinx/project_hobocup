using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MonoBehaviour
{
    
    public Vector3 movePos;
    public float fSpeed = 1f;
    
    private Vector3 currentPos;
    
    void Awake()
    {
        currentPos = new Vector3();
        currentPos = GetComponent<RectTransform>().anchoredPosition3D;
    }

    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()
    {
        // 位置リセット
        GetComponent<RectTransform>().anchoredPosition3D = currentPos;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp( GetComponent<RectTransform>().anchoredPosition3D, movePos, Time.deltaTime * fSpeed );
    }
}

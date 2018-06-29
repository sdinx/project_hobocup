
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAlpha : MonoBehaviour
{

    private Renderer renderer;
    [Range( 0f, 1f )] private float fAlpha;
    private float fAlphaVar;

    // Use this for initialization
    void Start()
    {
        fAlphaVar = .01f;
        renderer = GetComponent<MeshRenderer>();
        fAlpha = 1f;
        renderer.material.color = new Color( renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, fAlpha );
    }

    private void FixedUpdate()
    {
        fAlpha += fAlphaVar;
        if (fAlpha > 1f)
            fAlpha = 1f;
        else if (fAlpha < 0f)
            fAlpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.color = new Color( renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, fAlpha );
    }

    // プレイヤーが範囲内に入ったら透過する.
    private void OnTriggerEnter( Collider other )
    {
        fAlphaVar = -.1f;
    }

    private void OnTriggerExit( Collider other )
    {
        fAlphaVar = .1f;
    }

}

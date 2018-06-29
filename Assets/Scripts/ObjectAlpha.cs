using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAlpha : MonoBehaviour
{

    private Renderer renderer;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.color = new Color( 0f, 0f, 0f, 12f );
    }
}

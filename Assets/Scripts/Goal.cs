﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject gameClear;
    public ClearFlower[] clearFlowers;
    public bool isGameClear;
    
    // Use this for initialization
    void Start()
    {
        gameClear.SetActive(false);   
       // isGameClear = false;

    }

    // Update is called once per frame
    void Update()
    {
        foreach(var bloomFlower in clearFlowers)
        {
            isGameClear = bloomFlower.isBloom;
            if (isGameClear == false)
                break;
        }

        if (isGameClear == true)
        {

            gameClear.SetActive(true);

        }
    }
}

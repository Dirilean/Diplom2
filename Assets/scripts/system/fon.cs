﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fon : MonoBehaviour {


    [SerializeField]
    Camera MyCamera;
    float scale;
    // Use this for initialization
    void Start ()
    {
        scale = MyCamera.pixelWidth / 1086.0F;
        transform.localScale = new Vector3(scale, 1, 0);
        transform.localPosition =new Vector3(0,-MyCamera.pixelHeight/100+1);
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x,MyCamera.transform.position.x, 500F * Time.deltaTime), -2F);
    }
}

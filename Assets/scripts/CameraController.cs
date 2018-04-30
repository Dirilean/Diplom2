﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public float damping = 5f;
    public Vector2 offset;
    public bool faceLeft;
    private int lastX;
    [SerializeField]
    Character player;
    [SerializeField]
    GameObject ManagerForest;
    [SerializeField]
    GameObject ManagerSky;
    [SerializeField]
    GameObject ManagerSpace;

    #region Level settings
    [Header("Level settings")]
    public int to2lvl;
    public int to3lvl;
    public int toWin;
    public int Level;
    #endregion


    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        ManagerSky.SetActive(false);
        ManagerSpace.SetActive(false);
    }

    public void FindPlayer(bool playerFaceLeft)
    {
        lastX = Mathf.RoundToInt(player.transform.position.x);
        if (playerFaceLeft)
        {
            transform.position = new Vector3(player.transform.position.x - offset.x, offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.transform.position.x);
            lastX = Mathf.RoundToInt(player.transform.position.x);

            Vector3 target = new Vector3(player.transform.position.x + offset.x, offset.y, transform.position.z);

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }

        switch (Level)//для перехода на сл уровень
        {
            case 1: if (player.FireColb > to2lvl) { Level = 2; ManagerSky.SetActive(true); ManagerForest.SetActive(false); } break;//с 1 на 2
            case 2: if (player.FireColb > to3lvl) { Level = 3; ManagerSpace.SetActive(true); ManagerSky.SetActive(false); } break;//со 2 на 3
            case 3: if (player.FireColb > toWin) { Level = 4; Debug.Log("WIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIN"); } break;//выигрыш
        }
        
    }




}

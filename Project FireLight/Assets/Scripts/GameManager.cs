﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Player player;
    public Camera overheadCam; // TODO: get main camera via script and tags
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        player.cam = overheadCam;
        Vector3 playerPos = player.GetPlayerPosition();
        cameraOffset = overheadCam.transform.position - playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOverheadCamera();
        
    }

    // Update the position of the overhead camera
    void UpdateOverheadCamera() {
        Vector3 playerPos = player.GetPlayerPosition();
        overheadCam.transform.position = new Vector3(playerPos.x + cameraOffset.x, cameraOffset.y, playerPos.z + cameraOffset.z);
        //TODO: work in edge of room cases to keep gameplay on screen
    }
}

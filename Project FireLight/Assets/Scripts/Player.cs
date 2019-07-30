﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PrivateComponents
    private Rigidbody rb; // Rigidbody on player
    #endregion

    #region PublicVariables
    public float speed = 5; // Speed of player
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAim();
    }

    // Handles all directional movement for player
    void PlayerMove() {
        // Get axis inputs
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Set movement
        rb.MovePosition(movement * Time.deltaTime * speed + rb.position);
    }

    // Rotates the player to aim
    void PlayerAim() {

        // Get mouse position from screen to world
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        // Convert player and mouse positions to pseudo 2D
        Vector3 playerPos = new Vector3(transform.position.x, 0,  transform.position.z);
        Vector3 mousePos = new Vector3(mousePos3D.x, 0, mousePos3D.z);

        // Get direction from player to mouse and angle from forward
        Vector3 direction = playerPos - mousePos;
        float angle = Vector3.Angle(Vector3.back, direction); // Back is front for some reason. TODO: keep an eye here
            // Using constant vector3 opposed to player vector as game will maintain static camera rotation

        // Handle full rotation
        if (direction.x > 0) {
            angle = -angle;
        }

        // Set rotation
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    
    #region GettersSetters

    // Return player's current position (for external classes)
    public Vector3 GetPlayerPosition() {
        return transform.position;
    }

    #endregion
}


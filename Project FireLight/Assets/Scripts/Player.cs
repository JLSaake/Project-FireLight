using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Components
    private Rigidbody rb; // Rigidbody on player
    #endregion

    #region Public Variables
    public float speed = 5; // Speed of player
    public Projectile projectile;
    public Camera cam;
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

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }


    #region Movement and Shooting
    // Handles all directional movement for player
    void PlayerMove() {
        // Get axis inputs
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Set movement
        rb.MovePosition(movement * Time.deltaTime * speed + rb.position);
    }

    // Rotates the player to aim
    void PlayerAim() {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = new Vector3(hit.point.x, 1, hit.point.z);
            Vector3 playerPos = transform.position;

            // Get direction from player to mouse and angle from forward
            Vector3 direction = playerPos - mousePos;
            float angle = Vector3.Angle(Vector3.back, direction);

            // Handle full rotation
            if (direction.x > 0) {
                angle = -angle;
            }

            // Set rotation
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    // TODO: Make attack to allow for melee / other attacks
    void Shoot() {
        Projectile newProjectile = Instantiate(projectile);
        // TODO: give direction and velocity
        newProjectile.Fire(transform.position, transform.rotation.eulerAngles.y);
    }

    #endregion
    
    #region Getters and Setters

    // Return player's current position on the floor {y=0} (for external classes) 
    public Vector3 GetPlayerPosition() {
        return transform.position;
    }

    #endregion
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Components
    private Rigidbody rb; // Rigidbody on player
    private Camera overheadCam; // Set in GameManager
    private BoxCollider meleeCollider;

    #endregion

    #region Public Variables
    public float speed = 5; // Speed of player
    public int health = 10; // Health of player
    public int healthMax = 10;
    public float MeleeTime = 0.5f;
    public Projectile projectile;
    public int meleeDamage = 1;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        meleeCollider = GetComponent<BoxCollider>();
        meleeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAim();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Melee();
        }
    }


    #region Movement and Shooting
    // Handles all directional movement for player
    void PlayerMove()
    {
        // Get axis inputs
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Set movement
        rb.MovePosition(movement * Time.deltaTime * speed + rb.position);
    }

    // Rotates the player to aim
    void PlayerAim()
    {

        Ray ray = overheadCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            // Get direction from player to mouse and angle from forward
            Vector3 direction = transform.position - mousePos;
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
    void Shoot()
    {
        Projectile newProjectile = Instantiate(projectile);
        // TODO: give direction and velocity
        newProjectile.Fire(transform.position, transform.rotation.eulerAngles.y);
    }


    void Melee()
    {
        // sword.Swing();
        meleeCollider.enabled = true;
        StartCoroutine("MeleeTimer");
    }

    IEnumerator MeleeTimer()
    {
        yield return new WaitForSeconds(MeleeTime);
        meleeCollider.enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(meleeDamage);
        }
    }


    // Player loses health
    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Player gains health
    public void Heal(int heal)
    {
        health += heal;
        if (health > healthMax)
        {
            health = healthMax;
        }
    }

    void Die()
    {
        // TODO: add end of game scenes
    }

    #endregion
    
    #region Getters and Setters

    // Return player's current position on the floor {y=0} (for external classes) 
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void SetCam(Camera cam)
    {
        overheadCam = cam;
    }

    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 direction = Quaternion.Euler(0,0,0) * transform.forward * 5;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.transform.position.z), direction);
    }
}


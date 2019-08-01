using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb; // Projectile's rigidbody (found in Start)
    bool isMoving = false; // Is this projectile in motion / has been fired
    public float speed = 10; // Speed of projectile
    public int damage = 1; // Damage done to enemies
    public float destroyTime = 3; // Falloff time, after which the projectile is destroyed

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            rb.MovePosition(transform.forward * Time.deltaTime * speed + rb.position);
        }
    }

    // Begin the motion of the projectile
    public void Fire(Vector3 playerPosition, float shotAngle)
    {
        transform.position = new Vector3(playerPosition.x, 2, playerPosition.z);
        transform.rotation = Quaternion.Euler(new Vector3(0, shotAngle, 0));
        isMoving = true;
        Destroy(gameObject, destroyTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // When the projectile collides with an enemy, deal it damage. Otherwise destroy
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

}

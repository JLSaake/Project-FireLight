using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    bool isMoving = false;
    public float speed = 10;
    public float destroyTime = 3;

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

    public void Fire(Vector3 playerPosition, float shotAngle)
    {
        transform.position = new Vector3(playerPosition.x, 2, playerPosition.z);
        transform.rotation = Quaternion.Euler(new Vector3(0, shotAngle, 0));
        isMoving = true;
        Destroy(gameObject, destroyTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

}

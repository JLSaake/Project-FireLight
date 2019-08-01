using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent; // NavMesh agent found in Start on Enemy
    private Player player; // TODO: get from gamemanager
    private bool isFollowingPlayer = false; // If this enemy is currently tracking the player
    private bool isAttacking = false; // If this enemy is currently making an attack
    public float attackDistance = 2f;
    public int health = 1; // HP pool. Below 0 = Death
    public float detectionDistance = 10; // Normal distance to detect player directly in front
    public float limitedDetectionDistance = 5; // Shortened distance to detect player in peripherial vision
    public float forwardDetectionAngle = 45; // Angle from forward at which normal detection distance applies
    public float sideDetectionAngle = 90; // Angle from forward at which shortened detection distance applies

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        bool seePlayer = CanSeePlayerInRange();
        if (!isAttacking && seePlayer) // Design question: Do we store player's last known position while attacking?
        {
            agent.SetDestination(player.GetPlayerPosition());
            isFollowingPlayer = true;
            

            if (Vector3.Distance(agent.destination, agent.transform.position) <= attackDistance && isFollowingPlayer) {
                agent.SetDestination(agent.transform.position);
                isAttacking = true;
                StartCoroutine("Attack");
            }
        } else
        if (!seePlayer && isFollowingPlayer && !isAttacking)
        {
            if (agent.remainingDistance <= 1) // close enough to point
            {
                isFollowingPlayer = false;
                // TODO add stationary check and sentry mode return
                agent.SetDestination(agent.transform.position);
            }
        }

    }

    // Checks to see if the player can be seen, and is range based on sight angles
    bool CanSeePlayerInRange()
    {
        Vector3 playerPos = player.GetPlayerPosition();
        RaycastHit hit;
        Vector3 direction = transform.position - playerPos; 
        float currentDetection = detectionDistance;
        float angle = Vector3.Angle(transform.forward * -1, direction);

        // Check to see if player is within sight ranges, and set detection check distance
        if (angle > sideDetectionAngle) {
            return false;
        } else
        if (angle > forwardDetectionAngle && angle <= sideDetectionAngle)
        {
            currentDetection = limitedDetectionDistance;
        }

        // Player is within vision angles, check to see if they are within range
        if (Vector3.Distance(playerPos, transform.position) <= currentDetection)
        {
            if (Physics.Raycast(transform.position, playerPos-transform.position, out hit, currentDetection))
            {
                if (hit.transform == player.transform) {
                    return true;
                }
            }
        }
        return false;
    }

    // Deal damage to this enemy
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(2); // TODO: Remove, as this is temporary to get funciton working
        // Add attack animation / damage / effects here
        isAttacking = false;

    }

    // Death sequence (Destorys gameobject at end)
    void Die()
    {
        Destroy(this.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Debug vision angles
        Gizmos.color = Color.red;
        Vector3 direction = Quaternion.Euler(0, forwardDetectionAngle, 0) * transform.forward * detectionDistance;
        Vector3 bodyPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Gizmos.DrawRay(bodyPos, direction);
        direction = Quaternion.Euler(0, -forwardDetectionAngle, 0) * transform.forward * detectionDistance;
        Gizmos.DrawRay(bodyPos, direction);

        direction = Quaternion.Euler(0, sideDetectionAngle, 0) * transform.forward * limitedDetectionDistance;
        Gizmos.DrawRay(bodyPos, direction);
        direction = Quaternion.Euler(0, -sideDetectionAngle, 0) * transform.forward * limitedDetectionDistance;
        Gizmos.DrawRay(bodyPos, direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent;
    private Player player;
    public float detectionDistance = 10;
    public float limitedDetectionDistance = 5;
    public float forwardDetectionAngle = 45;
    public float sideDetectionAngle = 90;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayerInRange())
        {
            agent.SetDestination(player.GetPlayerPosition());
        }
    }

    bool CanSeePlayerInRange()
    {
        Vector3 playerPos = player.GetPlayerPosition();
        RaycastHit hit;
        Vector3 direction = transform.position - playerPos; 
        float currentDetection = detectionDistance;
        float angle = Vector3.Angle(transform.forward * -1, direction);

        
        if (angle > sideDetectionAngle) {
            return false;
        } else
        if (angle > forwardDetectionAngle && angle <= sideDetectionAngle)
        {
            currentDetection = limitedDetectionDistance;
        }

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = Quaternion.Euler(0, forwardDetectionAngle, 0) * transform.forward * detectionDistance;
        Gizmos.DrawRay(transform.position, direction);
        direction = Quaternion.Euler(0, -forwardDetectionAngle, 0) * transform.forward * detectionDistance;
        Gizmos.DrawRay(transform.position, direction);

        direction = Quaternion.Euler(0, sideDetectionAngle, 0) * transform.forward * limitedDetectionDistance;
        Gizmos.DrawRay(transform.position, direction);
        direction = Quaternion.Euler(0, -sideDetectionAngle, 0) * transform.forward * limitedDetectionDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}

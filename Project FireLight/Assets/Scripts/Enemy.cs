using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent;
    private Player player;
    public float detectionDistance = 10;

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

    bool CanSeePlayerInRange() {
        Vector3 playerPos = player.GetPlayerPosition();
        RaycastHit hit;

        if (Vector3.Distance(playerPos, this.transform.position) <= detectionDistance)
        {
            if (Physics.Raycast(transform.position, playerPos-transform.position, out hit, detectionDistance))
            {
                if (hit.transform == player.transform) {
                    return true;
                }
            }
        }
        return false;
    }
}

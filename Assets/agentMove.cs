using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollowPlayer : MonoBehaviour
{
    public Transform player;  // Drag your player GameObject here
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);

            Debug.Log("AAAAAA");
            NavMeshHit hit;
            if (NavMesh.SamplePosition(player.position, out hit, 2f, NavMesh.AllAreas))
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Debug.Log("Player not being tracked on navmesh");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class CrabsRunaway : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    public Animator anim;

    [Header("Player Interaction")]
    public float howCloseisTooClose = 5f;
    public float fleeDistance = 10f;
    private float distanceToPlayer;

    [Header("Wandering Settings")]
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
    private float wanderTimer;

    [Header("Behavior Tuning")]
    public float checkPathQuality = 0.9f; // 0 to 1, higher = stricter about picking good flee paths

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent)
        {
            Debug.LogError("No NavMeshAgent found on crab!");
            return;
        }

        player = GameObject.FindWithTag("Player")?.transform;
        if (!player)
        {
            Debug.LogError("No object tagged 'Player' found!");
            return;
        }

        wanderTimer = wanderInterval;
    }

    void Update()
    {
        if (!player || !agent) return;

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= howCloseisTooClose)
            FleeFromPlayer();
        else
            WanderAround();

        anim.enabled = agent.velocity.magnitude > 0.1f;
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 bestFleePosition = transform.position;
        float bestScore = 0f;

        // Try multiple possible escape points
        for (int i = 0; i < 20; i++)
        {
            // Add some random angle variation around the opposite direction
            Vector3 randomDir = Quaternion.Euler(0, Random.Range(-90f, 90f), 0) * fleeDirection;
            Vector3 potentialPos = transform.position + randomDir * fleeDistance;

            if (NavMesh.SamplePosition(potentialPos, out NavMeshHit hit, 3f, NavMesh.AllAreas))
            {
                // Check if path is reachable
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    // Evaluate candidate by distance and path simplicity
                    float distScore = Vector3.Distance(player.position, hit.position);
                    float pathLength = GetPathLength(path);
                    float efficiency = distScore / (pathLength + 0.01f);
                    if (efficiency > bestScore)
                    {
                        bestScore = efficiency;
                        bestFleePosition = hit.position;
                    }
                }
            }
        }

        // If no valid path found, circle around player to escape
        if (bestScore < checkPathQuality)
        {
            Vector3 circleDir = Quaternion.Euler(0, 120f * (Random.value > 0.5f ? 1 : -1), 0) * (player.position - transform.position);
            Vector3 circlePos = transform.position - circleDir.normalized * fleeDistance * 0.6f;
            if (NavMesh.SamplePosition(circlePos, out NavMeshHit circleHit, 3f, NavMesh.AllAreas))
                agent.SetDestination(circleHit.position);
        }
        else
        {
            agent.SetDestination(bestFleePosition);
        }
    }

    void WanderAround()
    {
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= wanderInterval)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
            if (newPos != transform.position)
                agent.SetDestination(newPos);
            wanderTimer = 0;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection.y = 0;
        randomDirection += origin;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, dist, layermask))
            return navHit.position;

        return origin;
    }

    float GetPathLength(NavMeshPath path)
    {
        if (path.corners.Length < 2) return 0f;
        float length = 0f;
        for (int i = 1; i < path.corners.Length; i++)
            length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        return length;
    }
} 


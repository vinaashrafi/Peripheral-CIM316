using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    public NavMeshAgent agent;
    public float timer;
    public bool isWandering = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ScaryFollow follow = GetComponent<ScaryFollow>();
        if (follow != null)
        {
            follow.OnPlayerDetected += () => isWandering = false;
            follow.OnPlayerLost += () => isWandering = true;
        }

        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        agent.speed = 2f; // or whatever speed you want
    }

    void Update()
    {
        if (!isWandering) return;

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask))
        {
            return navHit.position;
        }

        return origin;
    }
}
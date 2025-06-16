using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    public float detectionRadius = 15f;
    public float accelerationRate = 0.1f; // How fast the enemy builds up speed
    public float maxSpeed = 5f;
    public float stopDistance = 1f; // Distance at which the enemy will stop moving

    private NavMeshAgent agent;
    private Transform player;
    public Animator animator;

    public float currentSpeed = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            agent.SetDestination(player.position);

            // Smoothly ramp up speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, accelerationRate * Time.deltaTime);
            agent.speed = currentSpeed;

            // Stop when reaching player
            if (distance <= stopDistance)
            {
                currentSpeed = 0f; // Stop the enemy
                agent.speed = 0f; // Stop the NavMeshAgent as well
                agent.ResetPath(); // Reset the path
            }
        }
        else
        {
            // Slow down if player leaves range
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, accelerationRate * Time.deltaTime);
            agent.speed = currentSpeed;

            if (currentSpeed <= 0.1f)
            {
                agent.ResetPath();
            }
        }

        // Feed speed into animator
        float animationSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", animationSpeed);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Color for the detection range
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Draw the detection radius as a wire sphere
    }
    
}

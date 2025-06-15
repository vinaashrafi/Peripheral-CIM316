using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScaryFollow : MonoBehaviour
{
    public float detectionRadius = 15f;
    public float baseAccelerationRate = 0.1f; // Base acceleration rate
    public float maxSpeed = 5f;
    public float stopDistance = 1f; // Distance at which the enemy will stop moving
    public float sprintStartDistance = 8f; // distance inw hich he starts sprinting
    private NavMeshAgent agent;
    private Transform player;
    public Animator animator;

    public float currentSpeed = 0f;
    public bool wasPlayerDetected = false;
    public event System.Action OnPlayerDetected;
    public event System.Action OnPlayerLost;

    

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
        bool playerDetected = distance <= detectionRadius;

        // Fire detection events
        if (playerDetected && !wasPlayerDetected)
        {
            Debug.Log("OnPlayerDetected event sent");
            OnPlayerDetected?.Invoke();
        }
        else if (!playerDetected && wasPlayerDetected)
        {
            Debug.Log("OnPlayerLost event sent");
            OnPlayerLost?.Invoke();
        }

        wasPlayerDetected = playerDetected;

        // Movement logic only if player is detected
        if (playerDetected)
        {
            agent.SetDestination(player.position);

            if (distance > sprintStartDistance)
            {
                // Calculate creep speed if the player is within detection radius but far away
                float creepAcceleration = Mathf.Lerp(baseAccelerationRate, baseAccelerationRate * 0.5f, distance / detectionRadius);
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed * 0.5f, creepAcceleration * Time.deltaTime);
                agent.speed = currentSpeed;

                if (distance <= stopDistance)
                {
                    // Stop the agent if it's close to the player
                    currentSpeed = 0f;
                    agent.speed = 0f;
                    agent.ResetPath();
                    
                    Debug.Log("Agent has reached stop distance. Stopping movement.");
                    // animator.Play("Idle");
                }
            }
            else
            {
                // Calculate sprint speed when close to the player
                float sprintAcceleration = Mathf.Lerp(baseAccelerationRate, baseAccelerationRate * 2f, 1 - (distance / detectionRadius));
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, sprintAcceleration * Time.deltaTime);
                agent.speed = currentSpeed;
                // animator.Play("Run");
            }
        }
        else
        {
            // No speed adjustments if the player is out of detection radius
            agent.speed = agent.speed; // Keep current speed (no change)
        }

        // Update animator parameter
        float animationSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", animationSpeed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // Color for the detection range
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Draw the detection radius as a wire sphere
        
        // Gizmos for Sprint Start Distance (red)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sprintStartDistance);
        
        // Gizmos for Stop Distance (yellow)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
        
    }
    
    public bool IsPlayerDetected()
    {
        if (player == null) return false;
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= detectionRadius;
    }
    
    
    
}
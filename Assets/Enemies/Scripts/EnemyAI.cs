using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1f;
    public float obstacleAvoidanceRange = 10f;
    public float cooldown = 2f;
    public float bounceBack = 100f;
    public Transform seeker;

    private Transform player;
    private Rigidbody2D rb;
    private bool canMove = true;
    private float timer;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        if (Time.deltaTime > timer + cooldown)
        {
            canAttack = true;
            canMove = true;
        }
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the player is within the obstacle avoidance range
        if (distanceToPlayer < obstacleAvoidanceRange)
        {
            AvoidObstacles(directionToPlayer);
        }
        else if(canMove)
        {
            // Move towards the player
            rb.velocity = directionToPlayer.normalized * speed;
        }
    }

    void AvoidObstacles(Vector2 directionToPlayer)
    {
        // Raycast to check for obstacles in the path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, obstacleAvoidanceRange);
        Debug.Log(hit.collider.tag);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            // Calculate a perpendicular vector to move around the obstacle
            Vector2 avoidDirection = Vector2.Perpendicular(directionToPlayer).normalized;

            // Move away from the obstacle
            rb.velocity = avoidDirection * speed;
        }
        else
        {
            // If no obstacle detected, move towards the player
            rb.velocity = directionToPlayer.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer = Time.deltaTime;
            canMove = false;
            rb.AddForce(rb.velocity * -bounceBack);
            if (collision.TryGetComponent<Health>(out Health hinge) && canAttack)
            {
                //play animation
                Health health;
                health = collision.GetComponent<Health>();
                health.takeDamage(1);
            }
        }
    }
}

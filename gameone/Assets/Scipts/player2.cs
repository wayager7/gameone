using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public Transform target; // The object to follow
    public float speed = 5f; // Speed of following
    public float stoppingDistance = 1f; // Distance to maintain from the target
    public byte life = 200;
    public byte attack = 10;

    void Update()
    {
        if (target != null)
        {
            // Calculate the distance to the target
            float distance = Vector3.Distance(transform.position, target.position);

            // Move towards the target if it's farther than the stopping distance
            if (distance > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
        if (life == 0) { 
            
        }
    }
}


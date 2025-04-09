using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camfollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float height = 4f;          
    public float baseDistance = 2f;    
    public float zoomFactor = 1.5f;    
    public float minDistance = 5f;     
    public float maxDistance = 15f;    
    public float smoothTime = 0.3f;    

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 midpoint = (player1.position + player2.position) * 0.5f;
        float playersDistance = Vector3.Distance(player1.position, player2.position);
        float desiredDistance = Mathf.Clamp(baseDistance + playersDistance * zoomFactor, minDistance, maxDistance);

        // Positionnement de la caméra
        Vector3 desiredPosition = midpoint
                                + Vector3.up * height
                                - Vector3.forward * desiredDistance;

        // Lissage du mouvement
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(midpoint);
    }
}
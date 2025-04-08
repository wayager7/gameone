using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class moove: MonoBehaviour
{
    //
    public float moveSpeed = 5f; // Vitesse de d�placement
    public float minX = -5f;     // Limite gauche
    public float maxX = 5f;      // Limite droite

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        //// D�placement gauche/droite
        //float horizontal = Input.GetAxis("Horizontal"); // Fl�ches gauche/droite
        //transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);
        
        // D�placement gauche/droite
        float horizontal = Input.GetAxis("Horizontal"); // Fl�ches gauche/droite
        Vector3 newPosition = transform.position + new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);

        // Limitation de la position avec Mathf.Clamp
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Appliquer la nouvelle position
        transform.position = newPosition;
    }
}

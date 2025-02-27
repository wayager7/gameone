using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class moove: MonoBehaviour
{
    //
    public float moveSpeed = 5f; // Vitesse de déplacement

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        // Déplacement gauche/droite
        float horizontal = Input.GetAxis("Horizontal"); // Flèches gauche/droite
        transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readyplayerone : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float jumpHeight = 2f; // Hauteur du saut
    public float downHeight = -1f; // descente pour baisser
    private bool isGrounded = true; // Pour vérifier si le personnage est au sol
    private float initialY; // Position initiale en Y pour gérer le saut

    // Start is called before the first frame update
    void Start()
    {
        // Enregistre la position initiale Y du personnage
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // mouvement
        // Déplacement gauche/droite
        float horizontal = Input.GetAxis("Horizontal"); // Flèches gauche/droite
        transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);

        // Saut
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            transform.position += new Vector3(0f, jumpHeight, 0f);
            isGrounded = false; // Le personnage est en l'air après un saut
        }

        // Gestion du retour au sol
        if (!isGrounded && transform.position.y > initialY)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, initialY, transform.position.z), 5f * Time.deltaTime);
            if (Mathf.Abs(transform.position.y - initialY) < 0.01f)
            {
                transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
                isGrounded = true;
            }
        }

        //// Se baisser
        //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position += new Vector3(0f, downHeight);
        //    //isGrounded = false; // Le personnage est en l'air après un saut
        //}
        ////else
        ////{
        ////    transform.position += new Vector3(downHeight, 0f);
        ////}
    }
}


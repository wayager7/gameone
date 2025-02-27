using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class readyplayerone : MonoBehaviour
{
    //
    public float moveSpeed = 5f; // Vitesse de d�placement
    public float jumpHeight = 2f; // Hauteur du saut

    //imput var
    public KeyCode FastAttack = KeyCode.J;
    public KeyCode ScapeAttack = KeyCode.K;
    public KeyCode FinalAttack = KeyCode.I;
    public KeyCode AnswereAttack = KeyCode.L;

    //
    private bool isGrounded = true; // Pour v�rifier si le personnage est au sol
    private float initialY; // Position initiale en Y pour g�rer le saut

    // Start is called before the first frame update
    void Start()
    {
        // Enregistre la position initiale Y du personnage
        initialY = transform.position.y;
    }


    // Update is called once per frame
    void Update()
    {
        // d�placement
        // D�placement gauche/droite
        float horizontal = Input.GetAxis("Horizontal"); // Fl�ches gauche/droite
        transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);

        // Saut
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            transform.position += new Vector3(0f, jumpHeight, 0f);
            isGrounded = false; // Le personnage est en l'air apr�s un saut
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


        // action
    }
}


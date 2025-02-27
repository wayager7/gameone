using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camfollow : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
    public float minDistance = 7f; // Distance minimale entre C et A/B

    private float AB;
    private float BC;
    private float CA;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (A != null && B != null && C != null)
        {
            // Calcul de la distance entre A et B
            float AB = Vector3.Distance(A.position, B.position) + 5f;

            // Vérifier si la distance AB est trop petite
            float effectiveDistance = Mathf.Max(AB, minDistance);

            // Trouver le point milieu entre A et B
            Vector3 midpoint = (A.position + B.position) / 2f;

            // Calculer la direction perpendiculaire au segment AB
            Vector3 direction = (B.position - A.position).normalized;
            Vector3 perpendicular; // Déclaration avant l'if

            // Vérifier si B est "à droite" de A (dans l'axe X, par exemple)
            if (B.position.x > A.position.x)
            {
                Vector3 perpendicular = new Vector3(-direction.z, 0, direction.x); // Perpendiculaire dans XZ
            }
            else
            {
                Vector3 perpendicular = new Vector3(direction.z, 0, -direction.x); // Inverser la perpendiculaire
            }


            // Placer C à une distance contrôlée du milieu de AB
            C.position = midpoint + perpendicular * (effectiveDistance / 2f);
        }
    }
}

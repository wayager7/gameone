using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barredevie : MonoBehaviour
{
    public byte life = 200;
    public byte attack = 10;
    public Transform target;
    public float damageDistance = 3f;
    public float attackCooldown = 1f;

    [Header("Effets de d�g�ts")]
    public float recoilForce = 0.3f; // Force du recul
    public float shakeIntensity = 0.1f; // Intensit� du tremblement
    public float shakeDuration = 0.2f; // Dur�e du tremblement

    private float currentCooldown;
    private Vector3 originalPosition;
    private bool isShaking = false;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= damageDistance)
        {
            if (currentCooldown <= 0f)
            {
                ApplyDamage();
                currentCooldown = attackCooldown;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }
        else
        {
            currentCooldown = 0f;
        }
    }

    void ApplyDamage()
    {
        life = (byte)Mathf.Clamp(life - attack, 0, 200);

        // Effet de recul
        RecoilEffect();

        // Effet de tremblement
        if (!isShaking)
        {
            StartCoroutine(ShakeEffect());
        }

        Debug.Log("Vie restante: " + life);

        if (life == 0)
        {
            Debug.Log("Le sujet est mort!");
        }
    }

    void RecoilEffect()
    {
        // Calcul de la direction oppos�e � la cible
        Vector3 recoilDirection = (transform.position - target.position).normalized;
        transform.position += recoilDirection * recoilForce;
    }

    IEnumerator ShakeEffect()
    {
        isShaking = true;
        originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            // G�n�re un d�placement al�atoire
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );

            transform.position = originalPosition + randomOffset;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;
    }
}
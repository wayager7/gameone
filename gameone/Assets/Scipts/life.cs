using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class barredevie : MonoBehaviour
{
    public UnityEvent<byte, byte> OnHealthChanged; // current, max
    public UnityEvent<byte> OnDamageTaken; // montant des dégâts

    [Header("Paramètres de Santé")]
    [SerializeField] private byte maxLife = 200;
    [SerializeField] private byte currentLife;

    [Header("Effets de Dégâts")]
    [SerializeField] private float recoilForce = 0.5f;
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeDuration = 0.2f;

    [Header("Références")]
    [SerializeField] private Transform target;
    [SerializeField] private Image healthBarImage;

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        currentLife = maxLife;
        UpdateHealthUI();
        Debug.Log($"<color=cyan>[SANTE]</color> Initialisation de {gameObject.name} : {currentLife}/{maxLife} PV");
    }

    public byte GetCurrentLife() => currentLife;

    public void TakeDamage(byte damage)
    {
        byte previousLife = currentLife;
        currentLife = (byte)Mathf.Clamp(currentLife - damage, 0, maxLife);

        OnHealthChanged?.Invoke(currentLife, maxLife);
        OnDamageTaken?.Invoke(damage);

        Debug.Log(
            $"<color=orange>[DEGATS]</color> {gameObject.name}\n" +
            $"Dégâts reçus: <color=red>{damage}</color>\n" +
            $"PV: {previousLife} → <color=green>{currentLife}/{maxLife}</color>"
        );

        ApplyRecoil();
        StartCoroutine(ShakeEffect());
        UpdateHealthUI();

        if (currentLife == 0) Die();
    }

    private void ApplyRecoil()
    {
        if (target != null)
        {
            Vector3 recoilDirection = (transform.position - target.position).normalized;
            transform.position += recoilDirection * recoilForce;
        }
    }

    private IEnumerator ShakeEffect()
    {
        if (isShaking) yield break;

        isShaking = true;
        originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
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

    private void UpdateHealthUI()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentLife / maxLife;
        }
    }

    private void Die()
    {
        Debug.Log($"<color=red>[MORT]</color> {gameObject.name} est hors combat !");
        // Ajouter logique de mort ici
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private barredevie enemyHealth; // Garde le nom original
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject damagePopupPrefab;

    private void OnEnable()
    {
        // Utilise les événements de barredevie
        enemyHealth.OnHealthChanged.AddListener(UpdateHealthUI);
        enemyHealth.OnDamageTaken.AddListener(ShowDamagePopup);
    }

    private void OnDisable()
    {
        enemyHealth.OnHealthChanged.RemoveListener(UpdateHealthUI);
        enemyHealth.OnDamageTaken.RemoveListener(ShowDamagePopup);
    }

    private void UpdateHealthUI(byte current, byte max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;
        healthText.text = $"{current}/{max}";
    }

    private void ShowDamagePopup(byte damage)
    {
        if (damagePopupPrefab == null) return;

        Vector3 popupPosition = enemyHealth.transform.position + Vector3.up * 2;
        GameObject popup = Instantiate(damagePopupPrefab, popupPosition, Quaternion.identity, transform);

        if (popup.TryGetComponent<TMP_Text>(out var damageText))
        {
            damageText.text = $"-{damage}";
        }

        if (popup.TryGetComponent<DamagePopup>(out var damagePopup))
        {
            damagePopup.Initialize();
        }
    }
}
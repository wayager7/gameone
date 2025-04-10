using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private barredevie enemyHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject damagePopupPrefab;

    void Start()
    {
        // Abonne-toi aux événements de santé
        enemyHealth.OnHealthChanged.AddListener(UpdateHealthUI);
        enemyHealth.OnDamageTaken.AddListener(ShowDamagePopup);
    }

    void UpdateHealthUI(byte current, byte max)
    {
        // Met à jour la barre de vie
        healthSlider.maxValue = max;
        healthSlider.value = current;

        // Met à jour le texte
        healthText.text = $"{current}/{max}";
    }

    void ShowDamagePopup(byte damage)
    {
        // Crée un popup de dégâts
        GameObject popup = Instantiate(
            damagePopupPrefab,
            enemyHealth.transform.position + Vector3.up * 2,
            Quaternion.identity,
            transform // Parent au canvas
        );

        // Configure le texte
        TMP_Text damageText = popup.GetComponent<TMP_Text>();
        damageText.text = $"-{damage}";

        // Animation automatique (ajoute un composant DamagePopup au prefab)
        popup.GetComponent<DamagePopup>().Initialize();
    }
}
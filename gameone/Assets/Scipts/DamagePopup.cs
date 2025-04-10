using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float fadeDuration = 0.8f;

    private TMP_Text text;
    private float alpha;
    private Vector3 initialPosition;
    private Vector3 randomOffset;

    void Awake()
    {
        InitializeComponents();
        CacheInitialValues();
    }

    public void Initialize()
    {
        CalculateRandomOffset();
        StartCoroutine(Animate());
    }

    private void InitializeComponents()
    {
        text = GetComponent<TMP_Text>();
        if (text == null)
        {
            Debug.LogError("Missing Text component!", gameObject);
            enabled = false;
        }
    }

    private void CacheInitialValues()
    {
        if (text != null)
        {
            alpha = text.color.a;
            initialPosition = transform.position;
        }
    }

    private void CalculateRandomOffset()
    {
        randomOffset = new Vector3(
            Random.Range(-0.5f, 0.5f),
            0f,
            0f
        );
    }

    IEnumerator Animate()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            UpdatePosition(timer);
            UpdateAlpha(timer);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void UpdatePosition(float timer)
    {
        transform.position = initialPosition +
                           Vector3.up * (moveSpeed * timer) +
                           randomOffset * timer;
    }

    private void UpdateAlpha(float timer)
    {
        Color newColor = text.color;
        newColor.a = Mathf.Lerp(alpha, 0f, timer / fadeDuration);
        text.color = newColor;
    }
}
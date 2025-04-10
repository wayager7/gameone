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

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        alpha = text.color.a;
    }

    public void Initialize()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float timer = 0f;
        Vector3 startPos = transform.position;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            // Déplacement vers le haut
            transform.position = startPos + Vector3.up * moveSpeed * timer;

            // Fade out
            alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            transform.position += new Vector3(
            Random.Range(-0.5f, 0.5f),
                0,
                0
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}

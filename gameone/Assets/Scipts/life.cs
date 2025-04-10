using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class barredevie : MonoBehaviour
{
    #region Events
    public UnityEvent<byte, byte> OnHealthChanged = new UnityEvent<byte, byte>();
    public UnityEvent<byte> OnDamageTaken = new UnityEvent<byte>();
    #endregion

    #region Health Settings
    [Header("Health Configuration")]
    [SerializeField] private byte _maxLife = 200;
    [SerializeField] private byte _currentLife;
    #endregion

    #region Visual Effects
    [Header("Visual Feedback")]
    [SerializeField] private float _recoilForce = 0.5f;
    [SerializeField] private float _shakeIntensity = 0.1f;
    [SerializeField] private float _shakeDuration = 0.2f;
    [SerializeField] private Transform _target;
    #endregion

    #region UI Components
    [Header("UI References")]
    [SerializeField] private Image _healthBarImage;
    #endregion

    #region State
    private Vector3 _originalPosition;
    private bool _isShaking;
    #endregion

    private void Start()
    {
        InitializeHealthSystem();
        LogInitialHealthStatus();
    }

    /// <summary>
    /// Initializes the health system with default values
    /// </summary>
    private void InitializeHealthSystem()
    {
        _currentLife = _maxLife;
        UpdateHealthUI();
    }

    /// <summary>
    /// Applies damage to the entity and triggers related effects
    /// </summary>
    public void TakeDamage(byte damage)
    {
        byte previousLife = _currentLife;
        UpdateCurrentHealth(damage);

        TriggerHealthEvents(damage);
        LogDamageEvent(previousLife, damage);

        ApplyVisualFeedback();
        UpdateHealthUI();

        CheckForDeath();
    }

    #region Health Calculations
    private void UpdateCurrentHealth(byte damage)
    {
        _currentLife = (byte)Mathf.Clamp(_currentLife - damage, 0, _maxLife);
    }

    private void CheckForDeath()
    {
        if (_currentLife == 0)
        {
            HandleDeath();
        }
    }
    #endregion

    #region Visual Feedback
    private void ApplyVisualFeedback()
    {
        ApplyRecoilEffect();
        StartCoroutine(ShakeEffectRoutine());
    }

    private void ApplyRecoilEffect()
    {
        if (_target == null) return;

        Vector3 recoilDirection = (transform.position - _target.position).normalized;
        transform.position += recoilDirection * _recoilForce;
    }

    private IEnumerator ShakeEffectRoutine()
    {
        if (_isShaking) yield break;

        _isShaking = true;
        _originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < _shakeDuration)
        {
            elapsed += Time.deltaTime;
            ApplyRandomPositionOffset();
            yield return null;
        }

        ResetPosition();
        _isShaking = false;
    }

    private void ApplyRandomPositionOffset()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-_shakeIntensity, _shakeIntensity),
            Random.Range(-_shakeIntensity, _shakeIntensity),
            0
        );
        transform.position = _originalPosition + randomOffset;
    }

    private void ResetPosition()
    {
        transform.position = _originalPosition;
    }
    #endregion

    #region UI Management
    private void UpdateHealthUI()
    {
        if (_healthBarImage == null) return;

        _healthBarImage.fillAmount = (float)_currentLife / _maxLife;
    }
    #endregion

    #region Event System
    private void TriggerHealthEvents(byte damage)
    {
        OnHealthChanged?.Invoke(_currentLife, _maxLife);
        OnDamageTaken?.Invoke(damage);
    }
    #endregion

    #region Public API
    /// <summary>
    /// Gets the current life value (nom original conservé)
    /// </summary>
    public byte GetCurrentLife() => _currentLife;
    #endregion

    #region Logging System
    private void LogInitialHealthStatus()
    {
        Debug.Log($"<color=cyan>[SANTE]</color> {gameObject.name} initialisé avec {_currentLife}/{_maxLife} PV");
    }

    private void LogDamageEvent(byte previousLife, byte damage)
    {
        Debug.Log(
            $"<color=orange>[DEGATS]</color> {gameObject.name}\n" +
            $"Dégâts reçus: <color=red>{damage}</color>\n" +
            $"PV: {previousLife} → <color=green>{_currentLife}/{_maxLife}</color>"
        );
    }

    private void HandleDeath()
    {
        Debug.Log($"<color=red>[MORT]</color> {gameObject.name} est hors combat !");
        // Ajouter logique de mort ici
    }
    #endregion
}
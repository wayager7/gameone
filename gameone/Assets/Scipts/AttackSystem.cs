using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] private KeyCode _attackKey = KeyCode.Space;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;

    private float _nextAttackTime;
    private bool _canAttack = true;

    private void Update()
    {
        CheckAttackInput();
    }

    /// <summary>
    /// Handles attack input processing and cooldown management
    /// </summary>
    private void CheckAttackInput()
    {
        if (!_canAttack || Time.time < _nextAttackTime) return;

        if (Input.GetKeyDown(_attackKey))
        {
            TryExecuteAttack();
        }
    }

    /// <summary>
    /// Attempts to perform an attack if valid conditions are met
    /// </summary>
    private void TryExecuteAttack()
    {
        _canAttack = false;
        ExecuteAttack();
        UpdateCooldown();
    }

    /// <summary>
    /// Main attack execution logic
    /// </summary>
    private void ExecuteAttack()
    {
        LogAttackAttempt();
        var targets = DetectTargets();
        ProcessDetectedTargets(targets);
    }

    /// <summary>
    /// Detects valid targets within attack range
    /// </summary>
    private Collider[] DetectTargets()
    {
        return Physics.OverlapSphere(
            _attackPoint.position,
            _attackRange,
            _enemyLayer
        );
    }

    /// <summary>
    /// Processes all detected targets and applies damage
    /// </summary>
    private void ProcessDetectedTargets(Collider[] targets)
    {
        foreach (var target in targets)
        {
            if (TryGetHealthComponent(target, out var healthComponent))
            {
                ApplyDamage(healthComponent);
                LogDamageImpact(target, healthComponent);
            }
        }
    }

    /// <summary>
    /// Attempts to get health component from target
    /// </summary>
    private bool TryGetHealthComponent(Component target, out barredevie healthComponent)
    {
        healthComponent = target.GetComponent<barredevie>();
        return healthComponent != null;
    }

    /// <summary>
    /// Applies damage to a valid health component
    /// </summary>
    private void ApplyDamage(barredevie healthComponent)
    {
        healthComponent.TakeDamage((byte)_attackDamage);
    }

    /// <summary>
    /// Updates attack cooldown timer
    /// </summary>
    private void UpdateCooldown()
    {
        _nextAttackTime = Time.time + _attackCooldown;
        _canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        DrawAttackRangeGizmo();
    }

    /// <summary>
    /// Visualizes attack range in editor
    /// </summary>
    private void DrawAttackRangeGizmo()
    {
        if (!_attackPoint) return;

        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    #region Debug Methods
    private void LogAttackAttempt()
    {
        Debug.Log($"<color=green>[ATTACK]</color> {gameObject.name} attacking at {Time.time:F2}s");
    }

    private void LogDamageImpact(Component target, barredevie healthComponent)
    {
        Debug.Log(
            $"<color=yellow>[IMPACT]</color> Target hit: {target.name}\n" +
            $"Initial HP: {healthComponent.GetCurrentLife() + _attackDamage} | " +
            $"Damage dealt: {_attackDamage} | " +
            $"Remaining HP: {healthComponent.GetCurrentLife()}"
        );
    }
    #endregion
}
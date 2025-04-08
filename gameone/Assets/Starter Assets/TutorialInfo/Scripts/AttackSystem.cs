using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [Header("Paramètres de Base")]
    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Point d'Attaque")]
    [SerializeField] private Transform attackPoint;

    private float nextAttackTime;

    void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetKeyDown(attackKey))
        {
            LaunchAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void LaunchAttack()
    {
        Debug.Log($"<color=green>[ATTAQUE]</color> {gameObject.name} attaque à {Time.time:F2}s");

        Collider[] hitTargets = Physics.OverlapSphere(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider target in hitTargets)
        {
            ProcessHit(target);
        }
    }

    private void ProcessHit(Collider target)
    {
        barredevie targetHealth = target.GetComponent<barredevie>();
        if (targetHealth != null)
        {
            byte initialHealth = targetHealth.GetCurrentLife();
            targetHealth.TakeDamage((byte)attackDamage);

            Debug.Log(
                $"<color=yellow>[IMPACT]</color> Cible touchée: {target.name}\n" +
                $"PV initiaux: {initialHealth} | " +
                $"Dégâts infligés: {attackDamage} | " +
                $"PV restants: {targetHealth.GetCurrentLife()}"
            );
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
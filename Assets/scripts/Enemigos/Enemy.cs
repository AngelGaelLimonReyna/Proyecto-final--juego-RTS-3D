using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;

    void Start()
    {
        if (CompareTag("Player"))
        {
            UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
        }

        if (CompareTag("Enemy"))
        {
            UnitSelectionManager.Instance.unitEnemyList.Add(gameObject);
        }

        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            // lógica de muerte
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (CompareTag("Player"))
        {
            UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
        }

        if (CompareTag("Enemy"))
        {
            UnitSelectionManager.Instance.unitEnemyList.Remove(gameObject);
        }
    }

    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }
}


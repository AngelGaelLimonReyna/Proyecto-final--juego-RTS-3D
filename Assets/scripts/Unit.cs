
using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
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

        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            //logica de muerte
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (CompareTag("Player"))
        {
            UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
        }
    }

    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }
}

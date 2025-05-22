using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Para manejar botones

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private int totalResources = 0;

    [Header("Botones dependientes de recursos")]
    public Button buttonAt2Resources;
    public Button buttonAt6Resources;
    public Button buttonAt10Resources;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Asignar lógica a cada botón
        if (buttonAt2Resources != null)
            buttonAt2Resources.onClick.AddListener(() => TrySpendResources(2));

        if (buttonAt6Resources != null)
            buttonAt6Resources.onClick.AddListener(() => TrySpendResources(6));

        if (buttonAt10Resources != null)
            buttonAt10Resources.onClick.AddListener(() => TrySpendResources(10));
    }

    public void AddResources(int amount)
    {
        totalResources += amount;
        UIResourceDisplay.Instance?.UpdateDisplay(totalResources);
        UpdateButtons();
    }

    public void SubtractResources(int amount)
    {
        totalResources = Mathf.Max(0, totalResources - amount);
        UIResourceDisplay.Instance?.UpdateDisplay(totalResources);
        UpdateButtons();
    }

    public int GetResources()
    {
        return totalResources;
    }

    //Método que intenta gastar recursos
    private void TrySpendResources(int cost)
    {
        if (totalResources >= cost)
        {
            SubtractResources(cost);
            Debug.Log($"Gastaste {cost} recursos.");
        }
        else
        {
            Debug.Log("No tienes suficientes recursos.");
        }
    }

    // Habilitar/deshabilitar botones según cantidad de recursos
    private void UpdateButtons()
    {
        if (buttonAt2Resources != null)
            buttonAt2Resources.interactable = totalResources >= 2;

        if (buttonAt6Resources != null)
            buttonAt6Resources.interactable = totalResources >= 6;

        if (buttonAt10Resources != null)
            buttonAt10Resources.interactable = totalResources >= 10;
    }
}



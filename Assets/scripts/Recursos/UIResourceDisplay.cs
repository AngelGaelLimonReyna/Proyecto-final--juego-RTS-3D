using UnityEngine;
using TMPro;

public class UIResourceDisplay : MonoBehaviour
{
    public static UIResourceDisplay Instance;
    public TextMeshProUGUI resourceText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateDisplay(int amount)
    {
        resourceText.text = "RecursosCristales: " + amount.ToString();
    }
}


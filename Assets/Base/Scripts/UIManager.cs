using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button buildButton_CuyUnit;
    public Button buildButton_Ballesta;
    public Button buildButton_Chorizard;
    public PlacementSystem placement;

    private void Start()
    {
        buildButton_CuyUnit.onClick.AddListener(() => Construct(0)); //cambiando la ID cambiamos el prefab a colocar
        buildButton_Ballesta.onClick.AddListener(() => Construct(1)); //cambiando la ID cambiamos el prefab a colocar
        buildButton_Chorizard.onClick.AddListener(() => Construct(2)); //cambiando la ID cambiamos el prefab a colocar
    }

    private void Construct(int id)
    {
        Debug.Log("clicked");
        placement.StartPlacement(id);
    }
}

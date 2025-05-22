using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;

public class UnitEnemyManager : MonoBehaviour
{
    public static UnitEnemyManager Instance { get; private set; }
    public List<GameObject> allEnemyUnitsList = new List<GameObject>();
    public List<GameObject> unitEnemySelected = new List<GameObject>();

}


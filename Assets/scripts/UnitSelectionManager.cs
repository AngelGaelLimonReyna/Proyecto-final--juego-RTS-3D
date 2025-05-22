using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();
    public List<GameObject> unitEnemyList = new List<GameObject>();
    public List<GameObject> buildsActivas = new List<GameObject>(); // Lista de construcciones

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask attackable;

    public bool attackCursorVisible;
    public GameObject groundMarker;

    private Camera cam;

    [Header("Nombre de la escena Game Over")]
    public string nombreEscenaGameOver = "GameOver"; // Escena a cargar si no hay builds activas

    [Header("Intervalo de verificación en segundos")]
    public float tiempoVerificacion = 1.0f; // Intervalo de chequeo

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main;
        InvokeRepeating(nameof(ActualizarBuildsActivas), 0f, tiempoVerificacion); //Invoca cada cierto tiempo
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && unitSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }

        if (unitSelected.Count > 0 && AtleastOneOffensiveUnit(unitSelected))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                Debug.Log("Enemy Hovered with mouse");
                attackCursorVisible = true;

                if (Input.GetMouseButtonDown(1))
                {
                    Transform target = hit.transform;

                    foreach (GameObject unit in unitSelected)
                    {
                        if (unit.TryGetComponent(out AttackController attack))
                        {
                            attack.targetToAttack = target;
                        }
                    }
                }
            }
        }
        else
        {
            attackCursorVisible = false;
        }
    }

    private void ActualizarBuildsActivas()
    {
        GameObject[] builds = GameObject.FindGameObjectsWithTag("PlayerBuild");

        buildsActivas.Clear();
        buildsActivas.AddRange(builds);

        //Si no hay builds activas, cambiar de escena
        if (buildsActivas.Count == 0)
        {
            Debug.LogWarning("No quedan builds activas. Cambiando de escena a: " + nombreEscenaGameOver);
            SceneManager.LoadScene(nombreEscenaGameOver);
        }
    }

    private bool AtleastOneOffensiveUnit(List<GameObject> unitSelected)
    {
        foreach (GameObject unit in unitSelected)
        {
            if (unit.GetComponent<AttackController>())
                return true;
        }
        return false;
    }

    private void MultiSelect(GameObject unit)
    {
        if (!unitSelected.Contains(unit))
        {
            unitSelected.Add(unit);
            SelectUnit(unit, true);
        }
        else
        {
            SelectUnit(unit, false);
            unitSelected.Remove(unit);
        }
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();
        unitSelected.Add(unit);
        SelectUnit(unit, true);
    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        TrigerSelectionIndicator(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
    }

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            SelectUnit(unit, false);
        }

        groundMarker.SetActive(false);
        unitSelected.Clear();
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        if (unit.TryGetComponent(out UnitMovement movement))
        {
            movement.enabled = shouldMove;
        }
    }

    private void TrigerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

    internal void DragSelect(GameObject unit)
    {
        if (!unitSelected.Contains(unit))
        {
            unitSelected.Add(unit);
            SelectUnit(unit, true);
        }
    }
}


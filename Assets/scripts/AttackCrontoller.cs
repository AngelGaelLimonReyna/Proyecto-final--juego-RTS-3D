using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class AttackController : MonoBehaviour
{
    [Header("Tipo de Ataque (marca solo uno)")]
    public bool CortaDistancia;
    public bool LargaDistancia;
    public bool InAreaDistancia;

    [Header("Combat Settings")]
    [Tooltip("Velocidada de Ataque")]
    public float attackRate = 1f;
    public int unitDamage = 10;

    [Tooltip("Distancia mínima para comenzar a atacar")]
    public float attackDistance = 1f;

    [Tooltip("Distancia máxima para detener el ataque")]
    public float stopAttackingDistance = 1.2f;

    [Header("Prefab de Proyectiles")]
    public GameObject projectilePrefab;
    

    [Header("Materials")]
    public Material idleStatematerial;
    public Material followStatematerial;
    public Material attackStatematerial;

    [Header("State")]
    public bool isPlayer;
    public Transform targetToAttack;

    private void Awake()
    {
        ValidarTipoAtaque();
    }

    private void ValidarTipoAtaque()
    {
        int cont = 0;
        if (CortaDistancia) cont++;
        if (LargaDistancia) cont++;
        if (InAreaDistancia) cont++;

        if (cont > 1)
        {
            Debug.LogWarning($"{gameObject.name}: Solo uno de los modos de ataque debe estar activo. Se priorizará el primero que esté activo.");
            // Priorizar por orden
            if (CortaDistancia)
            {
                LargaDistancia = false;
                InAreaDistancia = false;
            }
            else if (LargaDistancia)
            {
                InAreaDistancia = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }

    public void SetIdleMaterial()
    {
        GetComponent<Renderer>().material = idleStatematerial;
    }

    public void SetFollowMaterial()
    {
        GetComponent<Renderer>().material = followStatematerial;
    }

    public void SetAttackMaterial()
    {
        GetComponent<Renderer>().material = attackStatematerial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance); // seguimiento
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance); // ataque
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, stopAttackingDistance); // dejar de atacar
    }

    // Método auxiliar para saber qué tipo de ataque tiene la unidad
    public string GetAttackType()
    {
        if (CortaDistancia) return "CortaDistancia";
        if (LargaDistancia) return "LargaDistancia";
        if (InAreaDistancia) return "InAreaDistancia";
        return "NoDefinido";
    }
}




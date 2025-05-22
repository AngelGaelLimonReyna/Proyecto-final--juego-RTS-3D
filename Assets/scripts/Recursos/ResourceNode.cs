using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [Header("Recolección")]
    public int resourceCapacity = 100;       // Recursos totales disponibles (vida)
    public int gatherAmountPerInterval = 5;  // Cuántos recursos da por ciclo
    public float gatherInterval = 1.0f;      // Tiempo entre cada recolección
    public float detectionRadius = 5.0f;     // Distancia a la que detecta unidades

    [Header("Materiales")]
    public Material depletedMaterial;        // Material para cuando está agotado

    private Material originalMaterial;
    private Renderer rend;
    private float gatherTimer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalMaterial = rend.material;
    }

    void Update()
    {
        if (resourceCapacity <= 0)
        {
            DepleteNode();
            return;
        }

        if (IsPlayerNearby())
        {
            gatherTimer += Time.deltaTime;
            if (gatherTimer >= gatherInterval)
            {
                GatherResources();
                gatherTimer = 0f;
            }
        }
        else
        {
            gatherTimer = 0f;
        }
    }

    void GatherResources()
    {
        int amountToGive = Mathf.Min(gatherAmountPerInterval, resourceCapacity);
        ResourceManager.Instance.AddResources(amountToGive);
        resourceCapacity -= amountToGive;
    }

    bool IsPlayerNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
                return true;
        }
        return false;
    }

    void DepleteNode()
    {
        if (rend != null && depletedMaterial != null)
            rend.material = depletedMaterial;

        enabled = false;
    }

    // Gizmo para visualizar el radio de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
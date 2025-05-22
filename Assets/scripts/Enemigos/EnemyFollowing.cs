using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    [Header("Ataque")]
    public float attackDistance = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;

    [Header("Detección")]
    public float targetCheckInterval = 1f;

    private float lastAttackTime = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogWarning($"{gameObject.name} no tiene NavMeshAgent. El seguimiento no funcionará.");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(FindTarget), 0f, targetCheckInterval);
    }

    private void Update()
    {
        if (target == null) return;

        agent.SetDestination(target.position);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackDistance && Time.time - lastAttackTime >= attackCooldown)
        {
            // Intenta hacerle daño si tiene el componente Unit
            Unit unit = target.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    private void FindTarget()
    {
        GameObject playerTarget = GameObject.FindWithTag("Player");
        if (playerTarget != null)
        {
            target = playerTarget.transform;
            return;
        }

        GameObject buildTarget = GameObject.FindWithTag("PlayerBuild");
        if (buildTarget != null)
        {
            target = buildTarget.transform;
        }
    }

    // Mostrar el radio de ataque como un gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}



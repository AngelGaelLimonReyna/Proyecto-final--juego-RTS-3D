using UnityEngine;

public class proyectilLogica : MonoBehaviour
{
    private Vector3 targetPosition;
    private int damage;
    public float speed = 10f;
    public float lifeTime = 5f;

    private bool initialized = false;

    public void Initialize(Vector3 targetPos, int damageAmount)
    {
        targetPosition = targetPos;
        damage = damageAmount;
        initialized = true;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (!initialized) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}


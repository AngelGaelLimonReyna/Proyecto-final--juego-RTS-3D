using System.Collections;
using UnityEngine;

public class BuildSpawner : MonoBehaviour
{
    [Header("Prefab que se instanciará")]
    public GameObject objetoAInstanciar;

    [Header("Cantidad de objetos a instanciar")]
    public int cantidad = 0;

    [Header("Distancia mínima desde el centro")]
    public float distanciaMinima = 1.0f;

    [Header("Distancia máxima desde el centro")]
    public float distanciaMaxima = 3.0f;

    private void OnEnable()
    {
        StartCoroutine(AddPointAndSpawn());
    }

    private IEnumerator AddPointAndSpawn()
    {
        yield return new WaitForSeconds(1f);
        cantidad += 1;
        Debug.Log($"Cantidad aumentada a: {cantidad}");
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        if (objetoAInstanciar == null)
        {
            Debug.LogWarning("¡No has asignado un prefab para instanciar!");
            return;
        }

        for (int i = 0; i < cantidad; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle.normalized;
            float distance = Random.Range(distanciaMinima, distanciaMaxima);
            Vector3 posicionFinal = transform.position + new Vector3(randomPoint.x, 0, randomPoint.y) * distance;

            Instantiate(objetoAInstanciar, posicionFinal, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanciaMaxima);
    }
}


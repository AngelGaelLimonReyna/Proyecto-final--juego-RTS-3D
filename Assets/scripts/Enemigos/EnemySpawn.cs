using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Prefab que se instanciará")]
    public GameObject objetoAInstanciar;

    [Header("Cantidad inicial de enemigos por ola")]
    public int cantidad = 1;

    [Header("Distancia mínima desde el centro")]
    public float distanciaMinima = 1.0f;

    [Header("Distancia máxima desde el centro")]
    public float distanciaMaxima = 3.0f;

    [Header("Tiempo entre spawns (segundos)")]
    public float tiempoEntreSpawns = 10f;

    [Header("¿Aumentar la cantidad con cada ola?")]
    public bool escalarCantidad = true;

    [Header("Límite total de enemigos activos")]
    public int maximoEnemigos = 15;

    [Header("Tag de los enemigos instanciados")]
    public string tagEnemigo = "Enemy";

    private void OnEnable()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);

            int enemigosVivos = GameObject.FindGameObjectsWithTag(tagEnemigo).Length;

            // Solo genera si faltan al menos 5 para el máximo
            if (enemigosVivos <= maximoEnemigos - 5)
            {
                int cantidadRestante = maximoEnemigos - enemigosVivos;
                int cantidadAInstanciar = Mathf.Min(cantidad, cantidadRestante);

                Debug.Log($"Instanciando {cantidadAInstanciar} enemigos (vivos: {enemigosVivos})");
                SpawnObjects(cantidadAInstanciar);

                if (escalarCantidad)
                    cantidad++;
            }
            else
            {
                Debug.Log("Suficientes enemigos vivos, no se genera en esta ola.");
            }
        }
    }

    public void SpawnObjects(int cantidad)
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

            GameObject nuevoEnemigo = Instantiate(objetoAInstanciar, posicionFinal, Quaternion.identity);
            nuevoEnemigo.tag = tagEnemigo; // Asegura que tenga el tag correcto
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


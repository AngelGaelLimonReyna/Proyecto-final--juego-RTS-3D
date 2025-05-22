using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Constructable : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    private BuildSpawner buildSpawner;

    public void ConstructableWasPlaced()
    {
        ActivateObstacle();
        ActivateBuildSpawner();
    }

    private void ActivateObstacle()
    {
        obstacle = GetComponentInChildren<NavMeshObstacle>();
        if (obstacle != null)
        {
            obstacle.enabled = true;
        }
        else
        {
            Debug.LogWarning("NavMeshObstacle no encontrado en hijos.");
        }
    }

    private void ActivateBuildSpawner()
    {
        buildSpawner = GetComponentInChildren<BuildSpawner>();
        if (buildSpawner != null)
        {
            buildSpawner.enabled = true;
            StartCoroutine(DelayedSpawn(buildSpawner, 1.5f));
        }
        else
        {
            Debug.LogWarning("BuildSpawner no encontrado en hijos.");
        }
    }

    private IEnumerator DelayedSpawn(BuildSpawner spawner, float delay)
    {
        yield return new WaitForSeconds(delay);
        spawner.SpawnObjects();
    }
}


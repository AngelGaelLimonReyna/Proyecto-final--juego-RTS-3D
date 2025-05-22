using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDestroy : MonoBehaviour
{
    [Header("Nombre de la escena a cargar al destruir este objeto")]
    public string nombreEscena = "GameOver";

    private void OnDestroy()
    {
        // Evitar que se intente cambiar de escena en modo de edición o si no hay nombre definido
        if (!string.IsNullOrEmpty(nombreEscena) && Application.isPlaying)
        {
            Debug.Log($"Objeto {gameObject.name} destruido. Cargando escena: {nombreEscena}");
            SceneManager.LoadScene(nombreEscena);
        }
    }
}


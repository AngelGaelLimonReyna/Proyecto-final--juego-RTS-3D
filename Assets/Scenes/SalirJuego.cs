using UnityEngine;
using UnityEngine.UI;

public class SalirJuego : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("El botón no está asignado en el inspector.");
        }
    }

    private void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

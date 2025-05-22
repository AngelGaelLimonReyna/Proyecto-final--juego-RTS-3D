using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button switchButton;

    private void Update()
    {
        if (switchButton != null)
        {
            switchButton.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogError("El bot�n no est� asignado en el inspector.");
        }
    }

    private void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("El nombre de la escena no est� definido.");
        }
    }
}

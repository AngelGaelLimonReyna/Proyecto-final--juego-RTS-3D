using UnityEngine;
using UnityEngine.InputSystem;

public class ControlCamara : MonoBehaviour
{
    public InputActionReference Movimiento;   // Movimiento WASD o joystick
    public InputActionReference Rotacion;     // Rotación horizontal
    public InputActionReference Zoom;         // Scroll del mouse para zoom

    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;
    public float velocidadZoom = 10f;

    [Header("Límites de Movimiento")]
    public Vector2 limiteX = new Vector2(-50f, 50f);
    public Vector2 limiteZ = new Vector2(-50f, 50f);

    private Transform yaw;
    private Camera fieldViewCamera;

    private float minFOV = 15f;
    private float maxFOV = 60f;

    void Start()
    {
        yaw = transform.Find("Yaw");
        fieldViewCamera = Camera.main;

        Movimiento.action.Enable();
        Rotacion.action.Enable();
        Zoom.action.Enable();
    }

    void Update()
    {
        Vector2 inputMovimiento = Movimiento.action.ReadValue<Vector2>();
        float inputRotacion = Rotacion.action.ReadValue<float>();
        float inputZoom = Zoom.action.ReadValue<Vector2>().y;

        // Rotación
        yaw.Rotate(0, inputRotacion * velocidadRotacion * Time.deltaTime, 0);

        // Movimiento con límites
        Vector3 movimientoRotado = yaw.rotation * new Vector3(inputMovimiento.x, 0, inputMovimiento.y);
        Vector3 nuevaPosicion = transform.position + movimientoRotado * velocidadMovimiento * Time.deltaTime;

        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteX.x, limiteX.y);
        nuevaPosicion.z = Mathf.Clamp(nuevaPosicion.z, limiteZ.x, limiteZ.y);

        transform.position = nuevaPosicion;

        // Zoom
        if (fieldViewCamera != null)
        {
            float nuevoFOV = fieldViewCamera.fieldOfView - inputZoom * velocidadZoom * Time.deltaTime;
            fieldViewCamera.fieldOfView = Mathf.Clamp(nuevoFOV, minFOV, maxFOV);
        }
    }
}

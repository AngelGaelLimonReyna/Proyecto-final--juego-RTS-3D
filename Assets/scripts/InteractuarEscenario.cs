using UnityEngine;

public class InteractuarEscenario : MonoBehaviour
{
    private Camera camara;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camara = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray raycastCamara = camara.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(raycastCamara.origin, raycastCamara.direction * 20, Color.magenta);
        if (Physics.Raycast(raycastCamara, out hit, 100)&& Input.GetMouseButtonDown(1)){

            Debug.Log(hit.collider.name);
        }
    }
}

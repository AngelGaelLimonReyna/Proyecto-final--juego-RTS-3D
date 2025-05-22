
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform localTrans;

    public Camera facingCamera;

    private void Start()
    {
        localTrans = GetComponent<Transform>();
        
        Transform camTransform = GameObject.Find("ControlCamara/Yaw/Pitch/Main Camera")?.transform;
        if (camTransform != null)
        {
            facingCamera = camTransform.GetComponent<Camera>();
        }
        else
        {
            Debug.LogWarning("No se encontró la ruta completa 'ControlCamara/Yaw/Pitch/Main Camera'.");
        }
    }

    private void Update()
    {
        if (facingCamera)
        {
            localTrans.LookAt(2 * localTrans.position - facingCamera.transform.position);
        }
    }
}
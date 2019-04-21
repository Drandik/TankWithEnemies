using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 cameraOffset = new Vector3(0, 20, -5);
    public Vector3 cameraRotation = new Vector3(90, 0, 0);

    private Transform _transform;
    public float smoothing = 5f;

    private void Start()
    {
        _transform = transform;
        _transform.position = Tank.TankTransform.position + cameraOffset;
        _transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = Tank.TankTransform.position + cameraOffset;
        _transform.position = Vector3.Lerp(_transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}

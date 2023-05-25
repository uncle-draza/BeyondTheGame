using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public float rotSpeed;

    private void OnMouseDrag()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotSpeed;

        transform.Rotate(Vector3.up, -XaxisRotation, Space.World);
        transform.Rotate(Vector3.forward, -YaxisRotation, Space.World);
    }

}


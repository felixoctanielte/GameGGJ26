using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target yang Diikuti")]
    public Transform target; 

    [Header("Pengaturan Kamera")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    
    void LateUpdate()
    {
        if (target == null) return; 

     
        Vector3 desiredPosition = target.position + offset;
        
        desiredPosition.z = transform.position.z;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Reference to Crunk's Transform
    public Transform target; 
    // Adjust to smooth the camera movement
    public float smoothSpeed = 0.125f; 
    // Offset to position the camera relative to Crunk
    public Vector3 offset; 

    // Camera movement constraints
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

     void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate between current and desired positions
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply boundary checks (clamp the camera position)
            float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // Set the camera position with boundaries
            transform.position = new Vector3(clampedX, clampedY, -10f);
        }
    }
}
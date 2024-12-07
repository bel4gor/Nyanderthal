using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    // Reference to the RectTransform of the UI cloud
    public RectTransform cloudTransform; 
    public float speed;

    void Start()
    {
        if (cloudTransform == null)
        {
            cloudTransform = GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        // Move the cloud horizontally by adjusting its anchored position
        cloudTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0f);
        if (cloudTransform.anchoredPosition.x > Screen.width)
        {
            cloudTransform.anchoredPosition = new Vector2(-Screen.width, cloudTransform.anchoredPosition.y);
        }
    }
}

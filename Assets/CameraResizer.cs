using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    public float minHeight, minWidth;
    
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float widthBasedHeight = minWidth / cam.aspect;
        GetComponent<Camera>().orthographicSize = Mathf.Max(minHeight, widthBasedHeight)/2 + 1;
    }
}

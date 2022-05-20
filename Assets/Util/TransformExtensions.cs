using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void Lerp(this Transform transform, Transform previous, Transform target, float progress)
    {
        progress = Mathf.Min(Mathf.Max(progress, 0), 1);
        transform.position = Vector3.Lerp(previous.position, target.position, progress);
        transform.localScale = Vector3.Lerp(previous.localScale, target.localScale, progress);
        transform.rotation = Quaternion.Lerp(previous.rotation, target.rotation, progress);
    }
}

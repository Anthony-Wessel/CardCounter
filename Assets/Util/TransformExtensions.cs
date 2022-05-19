using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    // TODO: Reconsider these
    public static Vector3 GetRelativePosition(this Transform transform, Transform reference)
    {
        return transform.position - reference.position;
    }

    public static void SetRelativePosition(this Transform transform, Vector3 position, Transform reference)
    {
        transform.position = position - reference.position;
    }

    public static Vector3 GetRelativeScale(this Transform transform, Transform reference)
    {
        return transform.lossyScale.DivideAxis(reference.lossyScale);
    }

    public static void SetRelativeScale(this Transform transform, Vector3 scale, Transform reference)
    {
        Vector3 lossyScale = scale.DivideAxis(reference.lossyScale);
        transform.localScale = lossyScale.DivideAxis(transform.parent.lossyScale);
    }

    public static Vector3 DivideAxis(this Vector3 top, Vector3 bottom)
    {
        float x = top.x / bottom.x;
        float y = top.y / bottom.y;
        float z = top.z / bottom.z;

        return new Vector3(x, y, z);
    }
}

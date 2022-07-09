using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static bool FloatIsZero(float value)
    {
        return Mathf.Abs(value) < float.Epsilon;
    }

    public static T GetScriptFromCollider<T>(Collider2D collider)
        where T : Component
    {
        return collider.GetComponent<T>();
    }

    public static float ScalarProjection(Vector2 a, Vector2 b)
    {
        return (a.x * b.x + a.y * b.y) / b.magnitude;
    }
}

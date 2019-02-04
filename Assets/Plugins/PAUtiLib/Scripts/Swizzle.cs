// Author(s): Paul Calande
// Replication of the swizzling functionality from GLSL.
// Swizzling is the rearrangement of the elements of a vector.
// This class is capable of compressing larger vectors into smaller ones.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Swizzle
{
    // Returns the value of the given component of the given vector.
    static float GetComponent(Vector4 vector, char component)
    {
        switch (component)
        {
            case 'x':
                return vector.x;
            case 'y':
                return vector.y;
            case 'z':
                return vector.z;
            case 'w':
                return vector.w;
            case '1':
                return 1.0f;
            default:
                return 0.0f;
        }
    }

    public static Vector2 Vec2(Vector4 vector, string components)
    {
        Vector2 result = Vector2.zero;
        for (int i = 0; i < 2; ++i)
        {
            result[i] = GetComponent(vector, components[i]);
        }
        return result;
    }

    public static Vector3 Vec3(Vector4 vector, string components)
    {
        Vector3 result = Vector3.zero;
        for (int i = 0; i < 3; ++i)
        {
            result[i] = GetComponent(vector, components[i]);
        }
        return result;
    }

    public static Vector4 Vec4(Vector4 vector, string components)
    {
        Vector4 result = Vector4.zero;
        for (int i = 0; i < 4; ++i)
        {
            result[i] = GetComponent(vector, components[i]);
        }
        return result;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Converter : MonoBehaviour
{
    public static Vector3 StringToVector3(string str)
    {
        str = str.Trim(new char[] { '(', ')' });
        string[] components = str.Split(',');

        float x = float.Parse(components[0]);
        float y = float.Parse(components[1]);
        float z = float.Parse(components[2]);

        return new Vector3(x, y, z);
    }

    public static string Vector3ToString(Vector3 vector)
    {
        return $"({vector.x}, {vector.y}, {vector.z})";
    }
}

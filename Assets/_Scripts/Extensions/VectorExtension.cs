using UnityEngine;


public static class VectorExtension
{
    public static Vector3 Rotate(this Vector3 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
    public static float ToAngle(this Vector3 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    public static Vector2 Rotate(this Vector2 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
    public static float ToAngle(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max)
    {
        float x = Mathf.Clamp(vector.x, min.x, max.x);
        float y = Mathf.Clamp(vector.y, min.y, max.y);

        return new Vector2(x, y);
    }

    public static Vector3 InverseX(this Vector3 vector)
    {
        return new Vector3(-vector.x, vector.y);
    }

    public static Vector2 Floor(this Vector2 vector)
    {
        return new Vector2(Mathf.Floor(vector.x), Mathf.Floor(vector.y));
    }

}


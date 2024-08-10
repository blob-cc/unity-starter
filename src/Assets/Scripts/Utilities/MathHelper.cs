using UnityEngine;

public static class MathHelper
{
    /// <summary>
    /// Clamps a value between a minimum and maximum range.
    /// </summary>
    public static float Clamp(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    /// <summary>
    /// Clamps a value between 0 and 1.
    /// </summary>
    public static float Clamp01(float value)
    {
        return Mathf.Clamp01(value);
    }

    /// <summary>
    /// Linearly interpolates between two values.
    /// </summary>
    public static float Lerp(float a, float b, float t)
    {
        return Mathf.Lerp(a, b, t);
    }

    /// <summary>
    /// Linearly interpolates between two Vector3 values.
    /// </summary>
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }

    /// <summary>
    /// Remaps a value from one range to another.
    /// </summary>
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    /// <summary>
    /// Returns the sign of a value.
    /// </summary>
    public static float Sign(float value)
    {
        return Mathf.Sign(value);
    }

    /// <summary>
    /// Returns the absolute value of a number.
    /// </summary>
    public static float Abs(float value)
    {
        return Mathf.Abs(value);
    }

    /// <summary>
    /// Returns the distance between two points.
    /// </summary>
    public static float Distance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    /// <summary>
    /// Calculates the magnitude of a vector.
    /// </summary>
    public static float Magnitude(Vector3 vector)
    {
        return vector.magnitude;
    }

    /// <summary>
    /// Normalizes a vector.
    /// </summary>
    public static Vector3 Normalize(Vector3 vector)
    {
        return vector.normalized;
    }

    /// <summary>
    /// Calculates the dot product of two vectors.
    /// </summary>
    public static float Dot(Vector3 a, Vector3 b)
    {
        return Vector3.Dot(a, b);
    }

    /// <summary>
    /// Calculates the cross product of two vectors.
    /// </summary>
    public static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return Vector3.Cross(a, b);
    }

    /// <summary>
    /// Returns a point on a circle given an angle.
    /// </summary>
    public static Vector2 PointOnCircle(float radius, float angleInDegrees, Vector2 origin = default)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians) * radius + origin.x;
        float y = Mathf.Sin(angleInRadians) * radius + origin.y;
        return new Vector2(x, y);
    }

    /// <summary>
    /// Returns a point on a sphere given spherical coordinates.
    /// </summary>
    public static Vector3 PointOnSphere(float radius, float latitude, float longitude, Vector3 origin = default)
    {
        float latRad = latitude * Mathf.Deg2Rad;
        float lonRad = longitude * Mathf.Deg2Rad;
        float x = radius * Mathf.Cos(latRad) * Mathf.Cos(lonRad) + origin.x;
        float y = radius * Mathf.Sin(latRad) + origin.y;
        float z = radius * Mathf.Cos(latRad) * Mathf.Sin(lonRad) + origin.z;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Calculates a smooth step between two values.
    /// </summary>
    public static float SmoothStep(float from, float to, float t)
    {
        t = Mathf.Clamp01(t);
        t = t * t * (3f - 2f * t);
        return from + (to - from) * t;
    }

    /// <summary>
    /// Returns a value that oscillates between 0 and 1 over a period of time.
    /// </summary>
    public static float PingPong(float value, float length)
    {
        return Mathf.PingPong(value, length);
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    public static float DegToRad(float degrees)
    {
        return degrees * Mathf.Deg2Rad;
    }

    /// <summary>
    /// Converts an angle from radians to degrees.
    /// </summary>
    public static float RadToDeg(float radians)
    {
        return radians * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Calculates the angle between two points in degrees.
    /// </summary>
    public static float AngleBetweenPoints(Vector2 pointA, Vector2 pointB)
    {
        return Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Checks if two float values are approximately equal.
    /// </summary>
    public static bool Approximately(float a, float b)
    {
        return Mathf.Approximately(a, b);
    }

    /// <summary>
    /// Returns a random point inside a unit sphere.
    /// </summary>
    public static Vector3 RandomPointInSphere()
    {
        return Random.insideUnitSphere;
    }

    /// <summary>
    /// Returns a random point inside a unit circle.
    /// </summary>
    public static Vector2 RandomPointInCircle()
    {
        return Random.insideUnitCircle;
    }

    /// <summary>
    /// Returns a random point on the surface of a unit sphere.
    /// </summary>
    public static Vector3 RandomPointOnSphere()
    {
        return Random.onUnitSphere;
    }

    /// <summary>
    /// Returns a random rotation.
    /// </summary>
    public static Quaternion RandomRotation()
    {
        return Random.rotation;
    }

    /// <summary>
    /// Returns a random rotation uniformly distributed.
    /// </summary>
    public static Quaternion RandomRotationUniform()
    {
        return Random.rotationUniform;
    }

    /// <summary>
    /// Converts a Vector3 to a Vector2 by dropping the z-axis.
    /// </summary>
    public static Vector2 ToVector2(Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    /// <summary>
    /// Converts a Vector2 to a Vector3 by adding a z-axis value.
    /// </summary>
    public static Vector3 ToVector3(Vector2 vector, float z = 0f)
    {
        return new Vector3(vector.x, vector.y, z);
    }
}
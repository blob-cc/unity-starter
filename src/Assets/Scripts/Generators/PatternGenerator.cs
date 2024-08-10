using UnityEngine;
using System.Collections.Generic;

public class PatternGenerator : MonoBehaviour
{
    public static PatternGenerator instance;

    private void Awake()
    {
        // Ensure there's only one instance of PatternGenerator
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Generates a grid of points.
    /// </summary>
    /// <param name="rows">Number of rows in the grid.</param>
    /// <param name="columns">Number of columns in the grid.</param>
    /// <param name="spacing">Spacing between points.</param>
    /// <returns>A list of Vector3 positions representing the grid points.</returns>
    public List<Vector3> GenerateGrid(int rows, int columns, float spacing)
    {
        List<Vector3> points = new List<Vector3>();

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 point = new Vector3(x * spacing, 0, y * spacing);
                points.Add(point);
            }
        }

        return points;
    }

    /// <summary>
    /// Generates a circular pattern of points.
    /// </summary>
    /// <param name="radius">Radius of the circle.</param>
    /// <param name="pointCount">Number of points in the circle.</param>
    /// <returns>A list of Vector3 positions representing the circular pattern.</returns>
    public List<Vector3> GenerateCircle(float radius, int pointCount)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < pointCount; i++)
        {
            float angle = i * Mathf.PI * 2 / pointCount;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            points.Add(new Vector3(x, 0, z));
        }

        return points;
    }

    /// <summary>
    /// Generates a spiral pattern of points.
    /// </summary>
    /// <param name="turns">Number of turns in the spiral.</param>
    /// <param name="radius">Initial radius of the spiral.</param>
    /// <param name="spacing">Spacing between points along the spiral.</param>
    /// <param name="pointCount">Number of points in the spiral.</param>
    /// <returns>A list of Vector3 positions representing the spiral pattern.</returns>
    public List<Vector3> GenerateSpiral(int turns, float radius, float spacing, int pointCount)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < pointCount; i++)
        {
            float angle = i * Mathf.PI * 2 / pointCount * turns;
            float currentRadius = radius + i * spacing;
            float x = Mathf.Cos(angle) * currentRadius;
            float z = Mathf.Sin(angle) * currentRadius;
            points.Add(new Vector3(x, 0, z));
        }

        return points;
    }

    /// <summary>
    /// Generates a line of points.
    /// </summary>
    /// <param name="start">Start position of the line.</param>
    /// <param name="end">End position of the line.</param>
    /// <param name="pointCount">Number of points along the line.</param>
    /// <returns>A list of Vector3 positions representing the line.</returns>
    public List<Vector3> GenerateLine(Vector3 start, Vector3 end, int pointCount)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            Vector3 point = Vector3.Lerp(start, end, t);
            points.Add(point);
        }

        return points;
    }

    /// <summary>
    /// Generates a grid of objects.
    /// </summary>
    /// <param name="rows">Number of rows in the grid.</param>
    /// <param name="columns">Number of columns in the grid.</param>
    /// <param name="spacing">Spacing between objects.</param>
    /// <param name="prefab">Prefab to instantiate at each grid point.</param>
    public void GenerateGridOfObjects(int rows, int columns, float spacing, GameObject prefab)
    {
        List<Vector3> gridPoints = GenerateGrid(rows, columns, spacing);

        foreach (var point in gridPoints)
        {
            Instantiate(prefab, point, Quaternion.identity);
        }
    }

    /// <summary>
    /// Generates a circular pattern of objects.
    /// </summary>
    /// <param name="radius">Radius of the circle.</param>
    /// <param name="pointCount">Number of objects in the circle.</param>
    /// <param name="prefab">Prefab to instantiate at each circle point.</param>
    public void GenerateCircleOfObjects(float radius, int pointCount, GameObject prefab)
    {
        List<Vector3> circlePoints = GenerateCircle(radius, pointCount);

        foreach (var point in circlePoints)
        {
            Instantiate(prefab, point, Quaternion.identity);
        }
    }

    /// <summary>
    /// Generates a spiral pattern of objects.
    /// </summary>
    /// <param name="turns">Number of turns in the spiral.</param>
    /// <param name="radius">Initial radius of the spiral.</param>
    /// <param name="spacing">Spacing between objects along the spiral.</param>
    /// <param name="pointCount">Number of objects in the spiral.</param>
    /// <param name="prefab">Prefab to instantiate at each spiral point.</param>
    public void GenerateSpiralOfObjects(int turns, float radius, float spacing, int pointCount, GameObject prefab)
    {
        List<Vector3> spiralPoints = GenerateSpiral(turns, radius, spacing, pointCount);

        foreach (var point in spiralPoints)
        {
            Instantiate(prefab, point, Quaternion.identity);
        }
    }

    /// <summary>
    /// Generates a line of objects.
    /// </summary>
    /// <param name="start">Start position of the line.</param>
    /// <param name="end">End position of the line.</param>
    /// <param name="pointCount">Number of objects along the line.</param>
    /// <param name="prefab">Prefab to instantiate at each line point.</param>
    public void GenerateLineOfObjects(Vector3 start, Vector3 end, int pointCount, GameObject prefab)
    {
        List<Vector3> linePoints = GenerateLine(start, end, pointCount);

        foreach (var point in linePoints)
        {
            Instantiate(prefab, point, Quaternion.identity);
        }
    }
}
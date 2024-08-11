using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private Mesh mesh;

    private void Awake()
    {
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }

    public void GeneratePlane(float width, float height, int widthSegments, int heightSegments)
    {
        mesh.Clear();

        int verticesCount = (widthSegments + 1) * (heightSegments + 1);
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[widthSegments * heightSegments * 6];
        Vector2[] uv = new Vector2[verticesCount];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int i = 0; i <= heightSegments; i++)
        {
            for (int j = 0; j <= widthSegments; j++)
            {
                float x = (float)j / widthSegments * width;
                float z = (float)i / heightSegments * height;

                vertices[vertexIndex] = new Vector3(x, 0, z);
                uv[vertexIndex] = new Vector2((float)j / widthSegments, (float)i / heightSegments);

                if (i < heightSegments && j < widthSegments)
                {
                    triangles[triangleIndex + 0] = vertexIndex;
                    triangles[triangleIndex + 1] = vertexIndex + widthSegments + 1;
                    triangles[triangleIndex + 2] = vertexIndex + 1;

                    triangles[triangleIndex + 3] = vertexIndex + 1;
                    triangles[triangleIndex + 4] = vertexIndex + widthSegments + 1;
                    triangles[triangleIndex + 5] = vertexIndex + widthSegments + 2;

                    triangleIndex += 6;
                }

                vertexIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

    public void GenerateCube(float width, float height, float depth)
    {
        mesh.Clear();

        Vector3[] vertices = new Vector3[8];
        int[] triangles = new int[36];
        Vector2[] uv = new Vector2[8];

        // Define the vertices of the cube
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(width, 0, 0);
        vertices[2] = new Vector3(width, height, 0);
        vertices[3] = new Vector3(0, height, 0);
        vertices[4] = new Vector3(0, height, depth);
        vertices[5] = new Vector3(width, height, depth);
        vertices[6] = new Vector3(width, 0, depth);
        vertices[7] = new Vector3(0, 0, depth);

        // Front face
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;

        // Back face
        triangles[6] = 5;
        triangles[7] = 7;
        triangles[8] = 6;
        triangles[9] = 5;
        triangles[10] = 4;
        triangles[11] = 7;

        // Top face
        triangles[12] = 3;
        triangles[13] = 5;
        triangles[14] = 2;
        triangles[15] = 3;
        triangles[16] = 4;
        triangles[17] = 5;

        // Bottom face
        triangles[18] = 1;
        triangles[19] = 6;
        triangles[20] = 0;
        triangles[21] = 0;
        triangles[22] = 6;
        triangles[23] = 7;

        // Left face
        triangles[24] = 7;
        triangles[25] = 4;
        triangles[26] = 0;
        triangles[27] = 0;
        triangles[28] = 4;
        triangles[29] = 3;

        // Right face
        triangles[30] = 6;
        triangles[31] = 2;
        triangles[32] = 1;
        triangles[33] = 6;
        triangles[34] = 5;
        triangles[35] = 2;

        // UV mapping (simple, can be adjusted)
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(0, 1);
        uv[4] = new Vector2(0, 1);
        uv[5] = new Vector2(1, 1);
        uv[6] = new Vector2(1, 0);
        uv[7] = new Vector2(0, 0);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

    public void GenerateGrid(int gridSize, float cellSize)
    {
        mesh.Clear();

        int verticesCount = (gridSize + 1) * (gridSize + 1);
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[gridSize * gridSize * 6];
        Vector2[] uv = new Vector2[verticesCount];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int i = 0; i <= gridSize; i++)
        {
            for (int j = 0; j <= gridSize; j++)
            {
                float x = j * cellSize;
                float z = i * cellSize;

                vertices[vertexIndex] = new Vector3(x, 0, z);
                uv[vertexIndex] = new Vector2((float)j / gridSize, (float)i / gridSize);

                if (i < gridSize && j < gridSize)
                {
                    triangles[triangleIndex + 0] = vertexIndex;
                    triangles[triangleIndex + 1] = vertexIndex + gridSize + 1;
                    triangles[triangleIndex + 2] = vertexIndex + 1;

                    triangles[triangleIndex + 3] = vertexIndex + 1;
                    triangles[triangleIndex + 4] = vertexIndex + gridSize + 1;
                    triangles[triangleIndex + 5] = vertexIndex + gridSize + 2;

                    triangleIndex += 6;
                }

                vertexIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

    // Example of calling the generator methods
    private void Start()
    {
        // Generate a 10x10 plane
        GeneratePlane(10f, 10f, 10, 10);

        // Generate a cube of size 1x1x1
        // GenerateCube(1f, 1f, 1f);

        // Generate a grid of size 5x5 with cell size of 1
        // GenerateGrid(5, 1f);
    }
}
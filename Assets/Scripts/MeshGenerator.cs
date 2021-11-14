using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathTools;

public class MeshGenerator : MonoBehaviour
{
    delegate float ComputeHeigthXDelegate(float kX);
    delegate float ComputeValueDelegate(float kX, float kZ);
    delegate Vector3 ComputeVertexPos(float k1, float k2);

    MeshFilter m_Mf;

    //[SerializeField] AnimationCurve m_GlassProfile;
    //[SerializeField] Texture2D m_HeightMap;
    [SerializeField] float m_longueurOcta;


    private void Awake()
    {
        m_Mf = GetComponent<MeshFilter>();
        //m_Mf.sharedMesh = GenerateTriangle();
        //Vector3 halfSize = new Vector3(5*0.5f, 0, 5*0.5f);

        //m_Mf.sharedMesh = GeneratePlane(5, 5, (kX, kZ) => new Vector3(Mathf.Lerp(-halfSize.x, halfSize.x, kX),
        //                                               0,
        //                                               Mathf.Lerp(-halfSize.z, halfSize.z, kZ)));

        //m_Mf.sharedMesh = GeneratePlane( 5,5, (kX,kZ) => new Vector3(Mathf.Lerp(-halfSize.x, halfSize.x, kX),
        //                                               0,
        //                                               Mathf.Lerp(-halfSize.z, halfSize.z, kZ)));//);// .125f * Mathf.Sin(kX* Mathf.PI * 2 * 4) * Mathf.Cos(kZ * Mathf.PI * 2 * 4));

        //m_Mf.sharedMesh = GenerateCylinder(40, 10, 2, 6, (kx,kZ)=>m_GlassProfile.Evaluate(kZ));

        m_Mf.sharedMesh = GenerateOctaedre(m_longueurOcta);

        //gameObject.AddComponent<MeshCollider>();
    }

    Mesh GenerateTerrainFromHeightFunction(int nSegmentsX, int nSegmentsZ, Vector3 size, ComputeValueDelegate heightFunc)
    {
        Vector3 halfSize = size * .5f;
        ComputeVertexPos terrainFunc = (kX, kZ) => new Vector3(
                            Mathf.Lerp(-halfSize.x, halfSize.x, kX),
                            size.y * heightFunc(kX, kZ),
                            Mathf.Lerp(-halfSize.z, halfSize.z, kZ));
        return GeneratePlane(nSegmentsX, nSegmentsZ, terrainFunc);
    }

    Mesh GenerateCylinder(int nSegmentsTheta, int nSegmentsY, float radius, float height, ComputeValueDelegate rhoFunc)
    {
        ComputeVertexPos cylFunc = (kX, kZ) =>
                                CoordConvert.CylindricalToCartesian(new Cylindrical(radius * rhoFunc(kX, kZ), kX * Mathf.PI * 2, height * kZ));
        return GeneratePlane(nSegmentsTheta, nSegmentsY, cylFunc);
    }

    


    Mesh GenerateTriangle()
    {
        Mesh mesh = new Mesh();
        mesh.name = "triangle";

        Vector3[] vertices = new Vector3[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(1, 0, 0);
        vertices[1] = new Vector3(0, 1, 0);
        vertices[2] = new Vector3(0, 0, 1);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }

    Mesh GenerateQuad(Vector3 size)
    {
        Mesh mesh = new Mesh();
        mesh.name = "quad";

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(-size.x * .5f, 0, -size.z * .5f);
        vertices[1] = new Vector3(-size.x * .5f, 0, size.z * .5f);
        vertices[2] = new Vector3(size.x * .5f, 0, size.z * .5f);
        vertices[3] = new Vector3(size.x * .5f, 0, -size.z * .5f);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }

    Mesh GenerateOctaedre(float size)
    {
        Mesh mesh = new Mesh();
        mesh.name = "octaedre";

        Vector3[] vertices = new Vector3[6];
        int[] triangles = new int[24];

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(size,0 ,0 );
        vertices[2] = new Vector3(size, 0, size);
        vertices[3] = new Vector3(0, 0, size);
        vertices[4] = new Vector3(size * 0.5f, size, size * 0.5f);
        vertices[5] = new Vector3(size * 0.5f, -size, size * 0.5f);
        
        int index = 0;
        for (int i = 0; i < 3; i++)
        {   
            //partie haute
            triangles[index] = i;
            triangles[index+1] = 4;
            triangles[index+2] = i+1;
            print(i);

            //partie basse
            triangles[index+3] = i;
            triangles[index+4] = i+1;
            triangles[index+5] = 5;

            index = index+6;
            
        }
        // //dernière partie haute
         triangles[18] = 3;
         triangles[19] = 4;
         triangles[20] = 0;
        // //dernière partie basse
         triangles[21] = 3;
         triangles[22] = 0;
         triangles[23] = 5;   

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;

    }

    Mesh GenerateStrip(Vector3 size, int nSegmentsX, ComputeHeigthXDelegate heightFunction)
    {
        Mesh mesh = new Mesh();
        mesh.name = "stripX";

        Vector3 halfSize = size * .5f;

        Vector3[] vertices = new Vector3[2 * (nSegmentsX + 1)];
        int[] triangles = new int[2 * 3 * nSegmentsX];
        Vector2[] uv = new Vector2[vertices.Length];
        //Vector3[] normals = new Vector3[vertices.Length];

        //vertices
        for (int i = 0; i < nSegmentsX + 1; i++)
        {
            float k = (float)i / nSegmentsX; // ratio

            float y = heightFunction(k);
            vertices[i] = Vector3.Lerp(new Vector3(-halfSize.x, y, -halfSize.z), new Vector3(halfSize.x, y, -halfSize.z), k); // bottom line
            vertices[i + nSegmentsX + 1] = Vector3.Lerp(new Vector3(-halfSize.x, y, halfSize.z), new Vector3(halfSize.x, y, halfSize.z), k); // top line

            uv[i] = new Vector2(k, 0);
            uv[i + nSegmentsX + 1] = new Vector2(k, 1);

            //normals[i] = Vector3.up; // new Vector3(0,1,0)          Vector3.right, Vectro3.forward
            //normals[i+nSegmentsX+1] = Vector3.up;
        }

        //triangles
        int index = 0;
        for (int i = 0; i < nSegmentsX; i++)
        {
            // 1st triangle of segment
            triangles[index++] = i;
            triangles[index++] = i + nSegmentsX + 1;
            triangles[index++] = i + nSegmentsX + 1 + 1;

            //2nd triangle of segment
            triangles[index++] = i;
            triangles[index++] = i + nSegmentsX + 1 + 1;
            triangles[index++] = i + 1;
        }



        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        //mesh.normals = normals;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    Mesh GeneratePlane(int nSegmentsX, int nSegmentsZ, ComputeVertexPos posFunction)
    {
        Mesh mesh = new Mesh();
        mesh.name = "3DWrappedPlaneObject";

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        //Vector3 halfSize = size * .5f;

        Vector3[] vertices = new Vector3[(nSegmentsX + 1) * (nSegmentsZ + 1)];
        int[] triangles = new int[2 * nSegmentsX * nSegmentsZ * 3];
        Vector2[] uv = new Vector2[vertices.Length];
        //Vector3[] normals = new Vector3[vertices.Length];

        //vertices
        for (int i = 0; i < nSegmentsX + 1; i++)
        {
            float kX = (float)i / nSegmentsX; // ratio
            int indexOffset = i * (nSegmentsZ + 1);

            for (int j = 0; j < nSegmentsZ + 1; j++)
            {
                float kZ = (float)j / nSegmentsZ; // ratio

                vertices[indexOffset + j] = posFunction(kX, kZ);                                                                                                                                             //new Vector3(halfSize.x, y, -halfSize.z), k); // bottom line

                uv[indexOffset + j] = new Vector2(kX, kZ);

            }

        }

        //triangles
        int index = 0;
        for (int i = 0; i < nSegmentsX; i++)
        {
            int indexOffset = (nSegmentsZ + 1) * i;
            for (int j = 0; j < nSegmentsZ; j++)
            {
                // 1st triangle of segment
                triangles[index++] = indexOffset + j;
                triangles[index++] = indexOffset + j + 1;
                triangles[index++] = indexOffset + j + 1 + nSegmentsZ + 1;

                //2nd triangle of segment
                triangles[index++] = indexOffset + j;
                triangles[index++] = indexOffset + j + 1 + nSegmentsZ + 1;
                triangles[index++] = indexOffset + j + nSegmentsZ + 1;
            }
        }



        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        //mesh.normals = normals;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
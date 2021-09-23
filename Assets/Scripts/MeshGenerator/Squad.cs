using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathTools;

public class Squad : MonoBehaviour
{
    MeshFilter m_Mf;
    [SerializeField] Vector3 sizeQuad;
    private void Awake()
    {
        m_Mf = GetComponent<MeshFilter>();
        m_Mf.sharedMesh = GenerateQuad(sizeQuad);
        gameObject.AddComponent<MeshCollider>();

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


}

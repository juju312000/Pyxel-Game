using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathTools;

public class CristauxGenerate : MonoBehaviour
{
    //MeshFilter m_Mf;

    //private List<GameObject> cristaux = new ArrayList();
    [SerializeField] float m_longueurOcta;
    float m_x;
    float m_y;
    float m_z;

    MeshFilter m_Mf;

    private List<GameObject> cristauxACombiner = new List<GameObject>();

    private void Awake()
    {
        m_Mf = GetComponent<MeshFilter>();
        
        m_x=Random.Range(-60,60);
        m_y=Random.Range(5,60);
        m_z=Random.Range(-60,60);

        m_Mf.sharedMesh = GenerateOctaedre(m_longueurOcta,m_x,m_y,m_z);
    }

    Mesh GenerateOctaedre(float size,float x,float y,float z)
    {
        Mesh mesh = new Mesh();
        mesh.name = "octaedre";

        Vector3[] vertices = new Vector3[6];
        int[] triangles = new int[24];

        vertices[0] = new Vector3(x, y, z);
        vertices[1] = new Vector3(x+size,y ,z );
        vertices[2] = new Vector3(x+size, y, z+size);
        vertices[3] = new Vector3(x, y, z+size);
        vertices[4] = new Vector3(x+size * 0.5f, y+size, z+size * 0.5f);
        vertices[5] = new Vector3(x+size * 0.5f, y-size, z+size * 0.5f);
        
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    //dimensione x e z del terreno
    public int xSize = 20;
    public int zSize = 20;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

    }

    void CreateShape ()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {   
                float y = Mathf.PerlinNoise(x * .3f, z) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        //generatore di triangoli
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int triang = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triang + 0] = vert + 0;
                triangles[triang + 1] = vert + xSize + 1;
                triangles[triang + 2] = vert + 1;
                triangles[triang + 3] = vert + 1;
                triangles[triang + 4] = vert + xSize + 1;
                triangles[triang + 5] = vert + xSize + 2;

                vert++;
                triang += 6;
            }
            vert++;
        }
        

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    //questo metodo serve per creare sfere su ogni vertice, non è essenziale, puoi toglierlo
    private void OnDrawGizmos()
    {

        if(vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

}

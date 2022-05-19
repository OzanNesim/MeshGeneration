using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TriangleGenerator : MonoBehaviour
{

    public uint Size = 1;

    public Vector3[] vertices;
    public int[] indices;

    private void OnValidate()
    {
        GenerateMesh();
    }

    [ContextMenu("GenerateMesh")]
    void GenerateMesh()
    {

        uint length = (Size + 1) * (Size + 2) / 2;
        float delta = 1f / Size;

        if (Size > 100)
        {
            Size = 100;
        }
        

        vertices = new Vector3[length];
        Vector2[] uvs = new Vector2[length];

        indices = new int[3*Size*(Size+1)/2 + 3 * Size * (Size - 1) / 2];

        int t = 0;
        int k = 0;

        for (int i = 0; i <= Size; i++)
        {
            for (int j = 0; j <= i; j++)
            {

                vertices[t] = new Vector3(i * delta,0, j * delta);
                uvs[t] = new Vector2(i * delta, j * delta);
                

                if (j != 0)
                {
                    indices[k] = t;
                    indices[k + 1] = t - 1;
                    indices[k + 2] = t - (i + 1);
                    k += 3;

                    
                    if (i != Size)
                    {
                        indices[k] = t;
                        indices[k + 1] = t + (i + 1);
                        indices[k + 2] = t - 1;
                        
                        k += 3;

                    }
                    

                    

                    
                }

                t++;


            }
        }

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = indices;

        GetComponent<MeshFilter>().mesh = mesh;



    }

}

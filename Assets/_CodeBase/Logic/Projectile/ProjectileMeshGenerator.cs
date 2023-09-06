using System;
using _CodeBase.StaticData;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CodeBase.Logic.Projectile
{
  public class ProjectileMeshGenerator : MonoBehaviour
  {
    public event Action<Vector3[]> Generated;
    
    [SerializeField] private MeshFilter _meshFilter;
    [Space(10)] 
    [SerializeField] private ProjectileData _data;

    private void Start()
    {
      _meshFilter.mesh.Clear();
      _meshFilter.mesh = GenerateMesh();
      Generated?.Invoke(_meshFilter.mesh.vertices);
    }

    private Mesh GenerateMesh()
    {
      Vector3[] vertices = GenerateVerts();
      int[] triangles = GenerateTries();
      
      for(int i = 0; i < vertices.Length; i++)
      {
        vertices[i] *= _data.Size;
        vertices[i] += Vector3.one * Random.Range(-_data.Offset, _data.Offset);
      }
      
      Mesh mesh = new Mesh
      {
        vertices = vertices,
        triangles = triangles,
        name = "GeneratedMesh"
      };

      mesh.RecalculateBounds();
      
      return mesh;
    }

    private Vector3[] GenerateVerts()
    {
      return new[] 
      {
        //bottom
        new Vector3 (-1, -1, -1),
        new Vector3 (1, -1, -1),
        new Vector3 (1, -1, 1),
        new Vector3 (-1, -1, 1),
        
        //top
        new Vector3 (-1, 1, 1),
        new Vector3 (1, 1, 1),
        new Vector3 (1, 1, -1),
        new Vector3 (-1, 1, -1)
      };
    }

    private int[] GenerateTries()
    {
      return new[]
      {
        0, 1, 2, //bottom
        0, 2, 3,
        2, 4, 3, //top
        2, 5, 4,
        1, 5, 2, //right
        1, 6, 5,
        0, 4, 7, //left
        0, 3, 4,
        5, 7, 4, //back
        5, 6, 7,
        0, 7, 6, //bottom
        0, 6, 1
      };
    }
  }
}
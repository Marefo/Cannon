using System;
using UnityEngine;

namespace _CodeBase.Logic.ProjectileCode
{
  public class Projectile : MonoBehaviour
  {
    public bool Available { get; private set; } = true;
    
    [field: SerializeField] public ProjectilePhysicsApplier Physics { get; private set; }
    [field: Space(10)]
    [SerializeField] public ProjectileMeshGenerator _meshGenerator;
    [SerializeField] private MeshRenderer _mesh;

    private void Awake() => Physics.Exploded += Disable;
    private void OnDestroy() => Physics.Exploded += Disable;
    
    public void Enable(Vector3 at)
    {
      Physics.enabled = true;
      _meshGenerator.Generate();
      _mesh.enabled = true;
      transform.position = at;
      Available = false;
    }

    private void Disable()
    {
      Physics.enabled = false;
      _mesh.enabled = false;
      Available = true;
    }
  }
}
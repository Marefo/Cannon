using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "ProjectileData", menuName = "StaticData/Projectile")]
  public class ProjectileData : ScriptableObject
  {
    [Header("MESH")]
    public float Size;
    public float Offset;
    [Header("PHYSICS")]
    [Range(0, 1)] public float BounceDamping;
    public float MinDistanceToEdgeForMark;
    public float ExplosionVelocity;
    public float Lifetime;
  }
}
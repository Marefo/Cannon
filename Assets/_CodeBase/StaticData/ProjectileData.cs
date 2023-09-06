using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "ProjectileData", menuName = "StaticData/Projectile")]
  public class ProjectileData : ScriptableObject
  {
    [Range(0, 1)] public float BounceDamping;
    public float ExplosionVelocity;
    public float Lifetime;
  }
}
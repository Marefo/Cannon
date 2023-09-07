using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Logic.ProjectileCode;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class ProjectilesPool : MonoBehaviour
  {
    public bool HasProjectiles => _projectiles.Count(projectile => projectile.Available) > 0;
      
    [SerializeField] private int _poolSize;
    [Space(10)]
    [SerializeField] private Projectile _projectilePrefab;

    private List<Projectile> AvailableProjectiles => _projectiles.Where(projectile => projectile.Available).ToList();
    
    private readonly List<Projectile> _projectiles = new List<Projectile>();

    private void Start() => Initialize();

    public Projectile Get() => AvailableProjectiles.First();

    private void Initialize()
    {
      GameObject parentObj = new GameObject("ProjectilesPool");
      
      for (int i = 0; i < _poolSize; i++)
        Spawn(parentObj.transform);
    }

    private void Spawn(Transform parent)
    {
      Projectile projectile = Instantiate(_projectilePrefab, Vector3.zero, Quaternion.identity, parent.transform);
      _projectiles.Add(projectile);
    }
  }
}
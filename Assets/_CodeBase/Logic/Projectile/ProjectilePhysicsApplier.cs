using System.Collections;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Logic.Projectile
{
  public class ProjectilePhysicsApplier : MonoBehaviour
  {
    [SerializeField] private ProjectileData _projectileData;
    [SerializeField] private GlobalData _globalData;

    private Vector3 _velocity;
    private float _lifetime;

    public void Launch(Vector3 initialVelocity)
    {
      _velocity = initialVelocity;
      StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
      while (true)
      {
        _lifetime += Time.deltaTime;
        
        if(_lifetime >= _projectileData.Lifetime)
          Explode();
        else
          ApplyPhysics();
        
        yield return null;
      }
    }

    private void ApplyPhysics()
    {
      Debug.Log(_velocity.magnitude);
      if (_velocity.magnitude <= _projectileData.ExplosionVelocity)
      {
        Explode();
        return;
      }
      
      _velocity += Vector3.down * _globalData.Gravity * Time.deltaTime;
      Vector3 currentStartPosition = transform.position;
      Vector3 lineCastEndPosition = currentStartPosition + _velocity * Time.deltaTime;
      bool isHit = Physics.Linecast(currentStartPosition, lineCastEndPosition, out RaycastHit hit);

      if (isHit) 
        Bounce(hit);

      transform.position += _velocity * Time.deltaTime;
    }

    private void Bounce(RaycastHit hit)
    {
      _velocity = Vector3.Reflect(_velocity * _projectileData.BounceDamping, hit.normal);
      transform.position = hit.point;
    }

    private void Explode()
    {
      Destroy(gameObject);
    }
  }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Infrastructure;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Logic.ProjectileCode
{
  public class ProjectilePhysicsApplier : MonoBehaviour
  {
    public event Action Exploded;
    
    public Vector3[] Vertices { get; private set; }
    public List<RaycastHit> Collisions { get; private set; } = new List<RaycastHit>();

    [SerializeField] private ParticleSystem _explosionVfx;
    [SerializeField] private LayerMask _markableLayer;
    [SerializeField] private Mark _explosionMark;
    [Space(10)]
    [SerializeField] private ProjectileMeshGenerator _meshGenerator;
    [Space(10)]
    [SerializeField] private ProjectileData _projectileData;
    [SerializeField] private GlobalData _globalData;

    private Vector3 _velocity;
    private float _lifetime;
    private Coroutine _moveCoroutine;

    private void OnEnable() => _meshGenerator.Generated += OnMeshGenerate;
    
    private void OnDisable()
    {
      if(_moveCoroutine != null)
        StopCoroutine(_moveCoroutine);

      _lifetime = 0;
      _velocity = Vector3.zero;
      
      _meshGenerator.Generated -= OnMeshGenerate;
    }

    public void Launch(Vector3 initialVelocity)
    {
      _velocity = initialVelocity;
      _moveCoroutine = StartCoroutine(MovementCoroutine());
    }

    private void OnMeshGenerate(Vector3[] vertices) => Vertices = vertices;

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
      if (_velocity.magnitude <= _projectileData.ExplosionVelocity)
      {
        Explode();
        return;
      }
      
      _velocity += Vector3.down * _globalData.Gravity * Time.deltaTime;

      Vector3 newPosition = GetNewPosition();
      Vector3 nextFramePosition = newPosition + _velocity.normalized * _projectileData.Size * 2;
      List<RaycastHit> collisions = TryGetCollisions(newPosition);
      List<RaycastHit> nextFrameCollisions = TryGetCollisions(nextFramePosition);
      
      Collisions.Clear();
      Collisions = collisions;

      if (collisions.Count > 0) 
        Bounce(collisions.First());
      else if(nextFrameCollisions.Count > 0)
        Bounce(nextFrameCollisions.First());

      transform.position += _velocity * Time.deltaTime;
    }
    
    private List<RaycastHit> TryGetCollisions(Vector3 at)
    {
      List<RaycastHit> collisions = new List<RaycastHit>();
        
      if(Vertices == null || Vertices.Length == 0) return collisions;
      List<Vector3> vertices = Vertices.ToList().Select(vertex => at + vertex).ToList();
      
      //bottom
      Physics.Linecast(vertices[0], vertices[1], out RaycastHit bottomBackCollision);
      Physics.Linecast(vertices[1], vertices[2], out RaycastHit bottomRightCollision);
      Physics.Linecast(vertices[2], vertices[3], out RaycastHit bottomFrontCollision);
      Physics.Linecast(vertices[3], vertices[0], out RaycastHit bottomLeftCollision);
      //sides
      Physics.Linecast(vertices[3], vertices[4], out RaycastHit frontLeftCollision);
      Physics.Linecast(vertices[0], vertices[7], out RaycastHit backLeftCollision);
      Physics.Linecast(vertices[1], vertices[6], out RaycastHit backRightCollision);
      Physics.Linecast(vertices[2], vertices[5], out RaycastHit frontRightCollision);
      //top
      Physics.Linecast(vertices[4], vertices[5], out RaycastHit topFrontCollision);
      Physics.Linecast(vertices[5], vertices[6], out RaycastHit topRightCollision);
      Physics.Linecast(vertices[6], vertices[7], out RaycastHit topBackCollision);
      Physics.Linecast(vertices[4], vertices[7], out RaycastHit topLeftCollision);

      if(IsColliding(bottomBackCollision))
        collisions.Add(bottomBackCollision);
      if(IsColliding(bottomRightCollision))
        collisions.Add(bottomRightCollision);
      if(IsColliding(bottomFrontCollision))
        collisions.Add(bottomFrontCollision);
      if(IsColliding(bottomLeftCollision))
        collisions.Add(bottomLeftCollision);
      if(IsColliding(frontLeftCollision))
        collisions.Add(frontLeftCollision);
      if(IsColliding(backLeftCollision))
        collisions.Add(backLeftCollision);
      if(IsColliding(backRightCollision))
        collisions.Add(backRightCollision);
      if(IsColliding(frontRightCollision))
        collisions.Add(frontRightCollision);
      if(IsColliding(topFrontCollision))
        collisions.Add(topFrontCollision);
      if(IsColliding(topRightCollision))
        collisions.Add(topRightCollision);
      if(IsColliding(topBackCollision))
        collisions.Add(topBackCollision);
      if(IsColliding(topLeftCollision))
        collisions.Add(topLeftCollision);
      
      return collisions;
    }

    private bool IsColliding(RaycastHit hit) => hit.transform != null;

    private Vector3 GetNewPosition() => transform.position + _velocity * Time.deltaTime;

    private void Bounce(RaycastHit hit)
    {
      TryToMarkSurface(hit);
      _velocity = Vector3.Reflect(_velocity * _projectileData.BounceDamping, hit.normal);
    }

    private void TryToMarkSurface(RaycastHit hit)
    {
      bool isMarkable = Helpers.CompareLayers(hit.transform.gameObject.layer, _markableLayer);

      float distanceToMaxX = Mathf.Abs(hit.collider.bounds.max.x - hit.point.x);
      float distanceToMinX = Mathf.Abs(hit.point.x - hit.collider.bounds.min.x);

      bool isFarFromEdge = distanceToMaxX > _projectileData.MinDistanceToEdgeForMark &&
                           distanceToMinX > _projectileData.MinDistanceToEdgeForMark;

      if (isMarkable && isFarFromEdge)
      {
        Vector3 spawnVfxPoint = hit.point + hit.normal * 0.2f;
        SpawnExplosionVfx(spawnVfxPoint, hit.normal);
      }
    }

    private void Explode()
    {
      Instantiate(_explosionVfx, transform.position, Quaternion.identity);
      Exploded?.Invoke();
    }

    private void SpawnExplosionVfx(Vector3 at, Vector3 normal)
    {
      Mark explosionMark = Instantiate(_explosionMark, at, Quaternion.identity);
      explosionMark.Initialize(_projectileData.Size);      
      explosionMark.transform.rotation = Quaternion.LookRotation(normal);
    }
  }
}
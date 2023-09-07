using System.Collections.Generic;
using System.Linq;
using _CodeBase.Logic.Projectile;
using UnityEditor;
using UnityEngine;

namespace _CodeBase.Editor
{
  [CustomEditor(typeof(ProjectilePhysicsApplier))]
  public class ProjectilesEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(ProjectilePhysicsApplier projectile, GizmoType gizmo)
    {
      DrawMeshRaycastEdges(projectile);
      DrawCollisionPoints(projectile);
    }

    private static void DrawMeshRaycastEdges(ProjectilePhysicsApplier projectile)
    {
      if (projectile.Vertices == null || projectile.Vertices.Length == 0) return;

      Gizmos.color = Color.red;

      List<Vector3> vertices = projectile.Vertices.ToList()
        .Select(vertex => projectile.transform.position + vertex).ToList();

      //bottom
      Gizmos.DrawLine(vertices[0], vertices[1]);
      Gizmos.DrawLine(vertices[1], vertices[2]);
      Gizmos.DrawLine(vertices[2], vertices[3]);
      Gizmos.DrawLine(vertices[3], vertices[0]);
      //sides
      Gizmos.DrawLine(vertices[3], vertices[4]);
      Gizmos.DrawLine(vertices[0], vertices[7]);
      Gizmos.DrawLine(vertices[1], vertices[6]);
      Gizmos.DrawLine(vertices[2], vertices[5]);
      //top
      Gizmos.DrawLine(vertices[4], vertices[5]);
      Gizmos.DrawLine(vertices[5], vertices[6]);
      Gizmos.DrawLine(vertices[6], vertices[7]);
      Gizmos.DrawLine(vertices[4], vertices[7]);
    }

    private static void DrawCollisionPoints(ProjectilePhysicsApplier projectile)
    {
      if(projectile.Collisions == null || projectile.Collisions.Count == 0 ) return;
      Gizmos.color = Color.yellow;

      foreach (RaycastHit hit in projectile.Collisions)
      {
        Gizmos.DrawSphere(hit.point, 0.1f);
      }
    }
  }
}
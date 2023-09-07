using UnityEngine;

namespace _CodeBase.Logic
{
  public class Mark : MonoBehaviour
  {
    [SerializeField] private Transform _visual;

    public void Initialize(float size)
    {
      _visual.localScale = Vector3.one * size;
    }
  }
}
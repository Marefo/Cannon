using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "GlobalData", menuName = "StaticData/Global")]
  public class GlobalData : ScriptableObject
  {
    public float Gravity;
  }
}
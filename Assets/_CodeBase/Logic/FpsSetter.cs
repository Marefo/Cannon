using System;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class FpsSetter : MonoBehaviour
  {
    private void Awake() => Application.targetFrameRate = 60;
  }
}
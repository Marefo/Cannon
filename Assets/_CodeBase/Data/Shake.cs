using System;

namespace _CodeBase.Data
{
  [Serializable]
  public class Shake
  {
    public float Force;
    public float Duration;

    public Shake(float force, float duration)
    {
      Force = force;
      Duration = duration;
    }
  }
}
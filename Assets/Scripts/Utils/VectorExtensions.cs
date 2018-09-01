using UnityEngine;

public static class MathUtils
{
  public static Vector3 Flatten(this Vector3 input)
  {
    return new Vector3(input.x, 0, input.z);
  }
}
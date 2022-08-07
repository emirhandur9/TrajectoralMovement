using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrajectoralMovementBase : ScriptableObject
{
    public abstract Vector3 Movement(Vector3 startPos, Vector3 endPos, float time, float maxHeight);
}

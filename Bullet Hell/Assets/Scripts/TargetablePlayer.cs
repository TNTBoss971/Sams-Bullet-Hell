using System.Collections.Generic;
using UnityEngine;

public class TargetablePlayer : MonoBehaviour
{
    public static readonly HashSet<TargetablePlayer> Entities = new HashSet<TargetablePlayer>();

    void Awake()
    {
        Entities.Add(this);
    }

    void OnDestroy()
    {
        Entities.Remove(this);
    }
}

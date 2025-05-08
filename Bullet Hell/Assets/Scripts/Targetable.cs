using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public static readonly HashSet<Targetable> Entities = new HashSet<Targetable>();

    void Awake()
    {
        Entities.Add(this);
    }

    void OnDestroy()
    {
        Entities.Remove(this);
    }
}

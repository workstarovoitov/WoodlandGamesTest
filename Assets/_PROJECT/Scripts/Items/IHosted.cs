using UnityEngine;
using System.Collections.Generic;

public interface IHosted
{
    bool IsNeedToAvoid(Vector3 position, List<AvoidSettings> settings);
}

[System.Serializable]
public struct AvoidSettings
{
    [SerializeField] internal float avoidRadius;
    [SerializeField] internal LayerMask skipLayer;
}


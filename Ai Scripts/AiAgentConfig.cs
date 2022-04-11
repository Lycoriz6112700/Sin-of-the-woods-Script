using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 0.1f;
    public float maxDistance = 0.1f;
    public float maxSightDistance = 5.0f;
}

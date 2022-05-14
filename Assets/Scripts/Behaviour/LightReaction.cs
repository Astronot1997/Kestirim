using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReaction
{

    public IReactsToLight Actor;
    public GameLight Light;
    public float Distance;
    public Vector3 Direction;

    public LightReaction(IReactsToLight actor, GameLight light, float distance, Vector3 direction)
    {
        Actor = actor;
        Light = light;
        Distance = distance;
        Direction = direction;
    }
}

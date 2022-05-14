using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactsToLight
{

    public GameLightType Color { get; set; }
    public float Range { get; set; }
    public void React(LightReaction[] Reaction) { }
    public Vector3 Position { get; }

}

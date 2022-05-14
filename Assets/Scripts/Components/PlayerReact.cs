using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReact : MonoBehaviour, IReactsToLight
{

    public GameLightType m_Color = GameLightType.None;

    [Range(0f,20f)]
    public float m_Range = 1;

    public GameLightType Color
    {
        get
        {
            return m_Color;
        }
        set
        {
            m_Color = value;
        }
    }

    public float Range
    {
        get
        {
            return m_Range;
        }
        set
        {
            m_Range = value;
        }
    }

    public Vector3 Position => transform.position;

    public void React(LightReaction[] Reaction)
    {

        Vector3 Direction = Vector3.zero;


        for (int i=0; i<Reaction.Length;i++)
        {

            Debug.Log(Reaction[i].Distance);

            if (Reaction[i].Light.Inverse)
            {
                Direction -= Reaction[i].Direction * Mathf.Sqrt(Reaction[i].Distance) * Reaction[i].Light.Strength;
            }
            else
            {
                Direction += Reaction[i].Direction * Mathf.Sqrt(Reaction[i].Distance) * Reaction[i].Light.Strength;
            }           

        }

        Direction.Normalize();
       // Direction = Vector3.Cross(Vector3.up, Direction);

        transform.Translate(Direction / 100);

    }

}

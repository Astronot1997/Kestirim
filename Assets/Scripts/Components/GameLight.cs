using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameLightType { All, None, White, Red, Blue, Green }

public class GameLight : MonoBehaviour
{

    UnityEvent LightChangeEvent;

    private GameLightType m_Type = GameLightType.None;

    [Range(0f, 20f)]
    private float m_Range = 1f;


    public GameLightType Type
    {
        get
        {
            return m_Type;
        }
        set
        {
            m_Type = value;
            OnLightChange();
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
            m_Range = Range;
            OnLightChange();
        }
    }


    private void Update()
    {
        
        if(transform.hasChanged)
        {
            OnLightChange();
        }
               
    }

    private void Awake()
    {
        if (LightChangeEvent == null)
        {
            LightChangeEvent = new UnityEvent();
        }
    }
    private void Start()
    {
        if (LightChangeEvent == null)
        {
            LightChangeEvent = new UnityEvent();
        }

        OnLightChange();

        Game GameInstance = FindObjectOfType<Game>();

    }

    private void OnValidate()
    {
        OnLightChange();
    }

    private void OnLightChange()
    {
        LightChangeEvent.Invoke();
    }


}

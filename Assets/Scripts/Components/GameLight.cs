using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameLightType { All, None, White, Red, Blue, Green }

public class GameLight : MonoBehaviour
{

    public UnityEvent LightChangeEvent;

    [SerializeField]
    private GameLightType m_Type = GameLightType.None;
    [SerializeField] [Range(0f, 20f)]
    private float m_Range = 1f;
    [SerializeField] [Range(0f, 20f)]
    private float m_Strength = 1f;
    [SerializeField]
    private bool m_Inverse = false;

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
    public float Strength
    {
        get
        {
            return m_Strength;
        }
        set
        {
            m_Strength = value;
        }
    }
    public bool Inverse
    {
        get
        {
            return m_Inverse;
        }
        set
        {
            m_Inverse = value;
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

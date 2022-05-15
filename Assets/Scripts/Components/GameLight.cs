using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameLightType { All, None, White, Red, Blue, Green }

public class GameLight : MonoBehaviour
{

    public float ChargeCooldown = 1f;
    public float ChargeRate = 0.5f;
    public float ChargeMax = 4f;

    public float PassiveStrength = 8;
    public float FullStrength = 36;

    public bool OnCooldown = false;
    public float CooldownCounter = 0f;

    public float Charge;


    public UnityEvent LightChangeEvent;

    [SerializeField]
    private GameLightType m_Type = GameLightType.None;
    [SerializeField]
    private float m_Strength = 5f;
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

    [SerializeField][HideInInspector] Light m_light;

    private void Update()
    {
        
        if(transform.hasChanged)
        {
            OnLightChange();
        }

        float TargetStrength;
        float DeltaStrength;

        if (Input.GetMouseButton(0) && Charge > 0f)
        {
            TargetStrength = FullStrength;
            CooldownCounter = 0f;
            OnCooldown = true;
            Charge -= Time.deltaTime;
            if (Charge <= 0.25)
            {
                Charge = 0f;
            }

        }
        else
        {
            if (OnCooldown)
            {
                CooldownCounter += Time.deltaTime;
            }
            else
            {
                CooldownCounter = 0f;
            }
            

            if(CooldownCounter > ChargeCooldown)
            {
                OnCooldown = false;
            }

            TargetStrength = PassiveStrength * Mathf.Clamp01(Charge / ChargeMax);

            /*
            if(Charge / ChargeMax > 0.5f)
            {
                TargetStrength = PassiveStrength;
            }
            else
            {
                TargetStrength = PassiveStrength * (2*Charge / ChargeMax);
            }
            */
        }

        if (!OnCooldown && Charge < ChargeMax)
        {
            Charge += ChargeRate*Time.deltaTime;
        }

        DeltaStrength = Time.deltaTime * (TargetStrength-Strength)/4;
        DeltaStrength = Mathf.Max(0.25f, DeltaStrength)*Mathf.Sign(DeltaStrength);

        Strength += DeltaStrength;

        if (TargetStrength - Strength < 1f)
        {
            Strength = TargetStrength;
        }


        float LightFactor = Strength;

       

        if ((TargetStrength - Strength < 1f) & Input.GetMouseButton(0))
        {
            //LightFactor *= Random.Range(0.9f, 1.4f);
        }

        m_light.intensity =  LightFactor * 1.8f;
        m_light.range = 10 + LightFactor * 2.5f;

    }
    private void Awake()
    {
        if (LightChangeEvent == null)
        {
            LightChangeEvent = new UnityEvent();
        }

        m_light = GetComponent<Light>();
    }
    private void Start()
    {
        Charge = ChargeMax;
        OnCooldown = false;
        CooldownCounter = 0f;
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

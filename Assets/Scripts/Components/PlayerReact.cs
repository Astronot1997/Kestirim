using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReact : MonoBehaviour, IReactsToLight
{


    public GameObject OnSleepSpawn;
    public GameObject OnAngrySpawn;

    public GameObject OnOuchSpawn;
    public GameObject OnKillSpawn;

    public float spawnCounter = 0f;

    public float MinEffectMult = 0.25f;
    [Range(0f,1f)]
    public float FullEffectAt = 0.25f;

    public GameLightType m_Color = GameLightType.None;

    [Range(0f,20f)]
    public float m_Range = 5;

    public float Awake = 0f;

    public int State
    {
        get
        {
            return m_State;
        }
        set
        {
            if(value != m_State)
            {
                m_State = value;

                switch (State)
                {
                    case 0:
                        //Debug.Log("0");
                        //GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", 0.25f);
                        //GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.25f);
                        GetComponent<Rigidbody>().velocity *= 0.20f;
                        GetComponent<Rigidbody>().drag = 5f;
                        //GetComponent<MeshRenderer>().material.SetColor("_BaseColor", UnityEngine.Color.black);
                        //GetComponent<MeshRenderer>().material.
                        break;
                    case 1:
                        //Debug.Log("1");
                        //GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", 0.5f);
                        //GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
                        GetComponent<Rigidbody>().drag = 2f;
                        //GetComponent<MeshRenderer>().material.SetColor("_BaseColor", UnityEngine.Color.blue);
                        break;
                    case 2:
                        //Debug.Log("2");
                        //GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", 1f);
                        //GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 1f);
                        //GetComponent<MeshRenderer>().material.SetColor("_BaseColor", UnityEngine.Color.green);
                        GetComponent<Rigidbody>().velocity *= 2f;
                        //GetComponent<Rigidbody>().drag = 0.5f;
                        break;

                }
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        KaranlikAdam hedef;
        if (TryGetComponent<KaranlikAdam>(out hedef))
        {
            if (Awake == 1)
            {
                var dir = transform.position - hedef.transform.position;

                hedef.GetComponent<Rigidbody>().AddForce(dir.normalized * -1000f);

                if (OnOuchSpawn)
                {
                    var letter = Instantiate(OnOuchSpawn);
                    letter.transform.position = transform.position;
                }
            }
            else
            {
                var dir = transform.position - hedef.transform.position;

                GetComponent<Rigidbody>().AddForce(dir.normalized * 1000f);

                if (OnKillSpawn)
                {
                    var letter = Instantiate(OnKillSpawn);
                    letter.transform.position = transform.position;
                }

            }
        }
    }


    [SerializeField]
    private int m_State = 0;
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
        float MaxStrength = 0;

        for (int i=0; i<Reaction.Length;i++)
        {

            Vector3 iDirection = Reaction[i].Direction * Reaction[i].Light.Strength;

            float ScaleFactor = Mathf.InverseLerp(Reaction[i].Actor.Range, Reaction[i].Actor.Range * FullEffectAt, Reaction[i].Distance);

            //Debug.Log(ScaleFactor);

            iDirection *= MinEffectMult + (1 - MinEffectMult) * ScaleFactor;

            if (Reaction[i].Light.Strength <= 4f)
            {
                iDirection *= 0f;
            }


            if (Reaction[i].Light.Inverse)
            {
                Direction -= iDirection;
            }
            else
            {
                Direction += iDirection;
            }

            MaxStrength = Mathf.Max(MaxStrength, Reaction[i].Light.Strength);

        }
        
        if (MaxStrength < 7)
        {
            State = 0;
        }
        else if (MaxStrength <= 12)
        {
            State = 1;
        }
        else
        {
            State = 2;
        }
        
      

        Direction.y = 0;

        var RB = GetComponent<Rigidbody>();
        RB.AddForce(Direction);

    }

    void Update()
    {
        var TargetAwake = 0f;
        switch (m_State)
        {
            case 0:
                TargetAwake = 0f;
                break;
            case 1:
                TargetAwake = 0.5f;
                break;
            case 2:
                TargetAwake = 1f;
                break;
        }

        if (Awake != TargetAwake)
        {
            var Delta = (TargetAwake - Awake) * Time.deltaTime;
            Delta = Mathf.Max(0.33f*Time.deltaTime, Mathf.Abs(Delta)) * Mathf.Sign(Delta);
            Awake += Delta;

            if (Mathf.Abs(TargetAwake-Awake) <= 0.1f)
             {
                Awake = TargetAwake;
            }

            if (Awake > 0.5f)
            {
               // GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", Awake*2f);
            }
            else
            {
                //GetComponent<MeshRenderer>().material.SetFloat("_Metallic", Awake);
            }

        }

        Color TargetColor;


        if (Awake > 0.5)
        {

            TargetColor = UnityEngine.Color.Lerp(UnityEngine.Color.blue, UnityEngine.Color.red, (Awake-0.5f) / 0.5f);

            if (spawnCounter < 0)
            {

                var letter = Instantiate(OnAngrySpawn);
                letter.transform.position = transform.position;
                spawnCounter = 0.6f;

                var comp = letter.GetComponent<DyingLetter>();

                if (comp.sound_src)
                {
                    spawnCounter = comp.sound_src.length;
                }
                else
                {
                    spawnCounter = 1f;
                }



                
            }

        }
        else if (Awake!=0)
        {

            TargetColor = UnityEngine.Color.Lerp(UnityEngine.Color.black, UnityEngine.Color.blue, Awake / 0.5f);
        }

        else
        {

            if(spawnCounter < 0)
            {
                var letter = Instantiate(OnSleepSpawn);
                letter.transform.position = transform.position;
                spawnCounter = 0.6f;

                var comp = letter.GetComponent<DyingLetter>();

                if (comp.sound_src)
                {
                    spawnCounter = comp.sound_src.length;
                }
                else
                {
                    spawnCounter = 1f;
                }
            }
            

            TargetColor = UnityEngine.Color.black;

        }
        spawnCounter -= Time.deltaTime;
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", TargetColor);
    }
}

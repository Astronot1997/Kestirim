using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KaranlikAdam : MonoBehaviour
{

    [SerializeField]
    [HideInInspector]
    PlayerReact oyuncu;
    GameLight isik;

    [SerializeField]
    [HideInInspector]
    Rigidbody m_rigidbody;

    public bool IsAfraid = false;
    public float Delay = 5f;

    public Vector2 ForceRange = new Vector2(10f,20f);
    public Vector2 DelayRange = new Vector2(1f, 5f);
    public Vector2 NoiseRange = new Vector2(-0.25f, 0.25f);

    public GameObject SpawnOnJump;
    public GameObject SpawnOnScared;

    public float spawnCount = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        oyuncu = FindObjectOfType<PlayerReact>();
        m_rigidbody = GetComponent<Rigidbody>();
        isik = FindObjectOfType<GameLight>();
    }

    // Update is called once per frame
    void Update()
    {

        var dpos = isik.transform.position - transform.position;

        dpos.y = 0f;

        //Debug.Log(oyuncu.transform.position);

        if (dpos.magnitude<5 & isik.Strength > 6f)
        {
            if (!IsAfraid)
            {
                //spawnCount = 0f;
            }
            IsAfraid = true;
            
        }
        else
        {
            if (IsAfraid)
            {
                //spawnCount = 0f;
            }
            IsAfraid = false;
        }


        if (!IsAfraid)
        {
            
            if(Delay < 0)
            {
                if (spawnCount < 0)
                {
                    var letter = Instantiate(SpawnOnJump);
                    letter.transform.position = transform.position;

                    var comp = letter.GetComponent<DyingLetter>();

                    if (comp.sound_src)
                    {
                        spawnCount = comp.sound_src.length + 12;
                    }
                    else
                    {
                        spawnCount = 1f;
                    }

                }
                m_rigidbody.drag = 1;
                var Dir = (oyuncu.transform.position - transform.position);

                Dir.x *= (1f + Random.Range(NoiseRange.x, NoiseRange.y));
                Dir.z *= (1f + Random.Range(NoiseRange.x, NoiseRange.y));

                //Dir.y = Random.Range(NoiseRange.x, NoiseRange.y) / 2f;

                Dir.y = 0f;

                Dir.Normalize();

                Dir *= Random.Range(ForceRange.x, ForceRange.y);
                Dir.y = Random.Range(ForceRange.x, ForceRange.y)/3f;

                m_rigidbody.AddForce(Dir);

                Delay = Random.Range(DelayRange.x, DelayRange.y);


            }
            else
            {
                if (Delay < Mathf.Min(1f,DelayRange.x))
                {
                    m_rigidbody.drag = 25;
                }

                Delay -= Time.deltaTime;
            }

        }
        else
        {

            if (spawnCount < 0)
            {
                var letter = Instantiate(SpawnOnScared);
                letter.transform.position = transform.position;

                var comp = letter.GetComponent<DyingLetter>();

                if (comp.sound_src)
                {
                    spawnCount = comp.sound_src.length;
                }
                else
                {
                    spawnCount = 1f;
                }
            }
            

            m_rigidbody.drag = 5;
            var Dir = (isik.transform.position - transform.position);
            Dir.x *= 1f;
            Dir.z *= 1f;
            Dir.y = 0;
            Dir.Normalize();
            if (isik.Strength > 20f)
            {
                Dir *= 25f;
            }
            else
            {
                Dir *= 12f;
            }

            m_rigidbody.AddForce(-Dir);

        }

        spawnCount -= Time.deltaTime;

    }
}

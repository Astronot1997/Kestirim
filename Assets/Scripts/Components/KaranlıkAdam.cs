using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KaranlıkAdam : MonoBehaviour
{

    [SerializeField]
    [HideInInspector]
    PlayerReact oyuncu;
    GameLight isik;

    [SerializeField]
    [HideInInspector]
    Rigidbody rigidbody;

    public bool IsAfraid = false;
    public float Delay = 5f;

    public Vector2 ForceRange = new Vector2(10f,20f);
    public Vector2 DelayRange = new Vector2(1f, 5f);
    public Vector2 NoiseRange = new Vector2(-0.25f, 0.25f);




    // Start is called before the first frame update
    void Start()
    {
        oyuncu = FindObjectOfType<PlayerReact>();
        rigidbody = FindObjectOfType<Rigidbody>();
        isik = FindObjectOfType<GameLight>();
    }

    // Update is called once per frame
    void Update()
    {

        var dpos = isik.transform.position - transform.position;

        dpos.y = 0f;

        if (dpos.magnitude<5)
        {
            IsAfraid = true;
        }
        else
        {
            IsAfraid = false;
        }


        if (!IsAfraid)
        {
            if(Delay < 0)
            {
                rigidbody.drag = 1;
                var Dir = (oyuncu.transform.position - transform.position);

                Dir.x *= (1f + Random.Range(NoiseRange.x, NoiseRange.y));
                Dir.z *= (1f + Random.Range(NoiseRange.x, NoiseRange.y));

                //Dir.y = Random.Range(NoiseRange.x, NoiseRange.y) / 2f;

                Dir.y = 0f;

                Dir.Normalize();

                Dir *= Random.Range(ForceRange.x, ForceRange.y);
                Dir.y = Random.Range(ForceRange.x, ForceRange.y)/3f;

                rigidbody.AddForce(Dir);

                Delay = Random.Range(DelayRange.x, DelayRange.y);


            }
            else
            {
                if (Delay < Mathf.Min(1f,DelayRange.x))
                {
                    rigidbody.drag = 25;
                }

                Delay -= Time.deltaTime;
            }

        }
        else
        {
            rigidbody.drag = 5;
            var Dir = (isik.transform.position - transform.position);
            Dir.x *= 1f + (Random.Range(-0.4f, 0.4f));
            Dir.z *= 1f + (Random.Range(-0.4f, 0.4f));
            Dir.y = 0;
            Dir.Normalize();
            Dir *= 5f;
            rigidbody.AddForce(-Dir);

        }


    }
}

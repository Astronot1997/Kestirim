using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshPro))]
[RequireComponent(typeof(AudioSource))]
public class DyingLetter : MonoBehaviour
{

    public Vector2 LifeRange;
    public float Speed;
    public AudioClip sound_src;

    private float Life;
    Camera m_camera;
    AudioSource src;

    private void Awake()
    {
        m_camera = Camera.main;
        src = GetComponent<AudioSource>();
    }

    void Start()
    {
        Life = Random.Range(LifeRange.x, LifeRange.y);
        transform.Translate(0, 3, 0, Space.World);

        if (sound_src)
        {
            src.clip = sound_src;
            src.Play();
            Life = src.clip.length;
        }
        
    }

    private void Update()
    {
        Life -= Time.deltaTime;

        if (Life <= 0)
        {
            Destroy(gameObject);    
        }

        var pos = transform.position;
        pos.y += Speed/100f;

        transform.position = pos;

        transform.LookAt(m_camera.transform);
    }

}

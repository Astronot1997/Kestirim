using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshPro))]
public class DyingLetter : MonoBehaviour
{

    public string letter;
    public Color color;
    public Vector2 LifeRange;
    public float Speed;

    private float Life;
    Camera m_camera;
    TMPro.TextMeshPro Text;

    private void Awake()
    {
        Text = GetComponent<TMPro.TextMeshPro>();

    }


    void Start()
    {
        Life = Random.Range(LifeRange.x, LifeRange.y);
        m_camera = Camera.main;
        Text.text = letter;
        Text.color = color;
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

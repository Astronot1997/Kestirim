using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCamera : MonoBehaviour
{

    public GameObject Target;

    public float d;
    public float h;

    void Update()
    {

        float horval = Input.GetAxis("Horizontal");
        float verval = Input.GetAxis("Vertical");
        float pageval = Input.GetAxis("Mouse ScrollWheel");

        d += verval/4;
        h -= pageval*10;

        if (Target)
        {
            transform.position = new Vector3(Target.transform.position.x+d, Target.transform.position.y+h, Target.transform.position.z + d);
            transform.LookAt(Target.transform);

        }

        

    }
}

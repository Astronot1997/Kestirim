using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCamera : MonoBehaviour
{
    public GameObject Environment;
    public GameObject Target;

    void Update()
    {

        float horval = Input.GetAxis("Horizontal");
        float verval = Input.GetAxis("Vertical");
        float pageval = Input.GetAxis("Mouse ScrollWheel");

        transform.position += new Vector3(horval/10f, -pageval*3f, verval / 10f);

        if (Target)
        {
            transform.position = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
        }

        

    }
}

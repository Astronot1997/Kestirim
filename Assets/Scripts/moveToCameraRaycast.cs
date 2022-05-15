using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToCameraRaycast : MonoBehaviour
{
    public Camera camera;
    public float height=1;
    public Transform cursor;
    void Update()
    {
        int layerMask = 1 << 3;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit,1000,layerMask))
        {
            //cursor.position = hit.point+Vector3.up*height;

            cursor.position = hit.point + Vector3.up * height;


            //cursor.position += hit.normal * height;

           // Debug.Log(hit.point);
           //Debug.DrawLine(camera.transform.position, hit.point);
           // Do something with the object that was hit by the raycast.
        }
    }
}
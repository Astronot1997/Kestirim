using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScriptOne : MonoBehaviour
{
 
    // Update is called once per frame
    void Update()
    {

        KaranlikAdam[] ka = FindObjectsOfType<KaranlikAdam>(true);

        if (ka.Length == 5)
        {
            ka[1].gameObject.SetActive(true);
            ka[1].gameObject.SetActive(true);

            Debug.Log("A?ama 1");
        }   
        else if(ka.Length == 3)
        {
            ka[1].gameObject.SetActive(true);
            ka[2].gameObject.SetActive(true);
            ka[3].gameObject.SetActive(true);

            Debug.Log("A?ama 2");
        }
        else
        {
            Debug.Log("Kazand?k yeni sahne geç");
        }
    }
}

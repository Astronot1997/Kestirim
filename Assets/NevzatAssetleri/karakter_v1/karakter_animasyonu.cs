using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class karakter_animasyonu : MonoBehaviour
{
    public int state;
    public GameObject sleep;
    public GameObject run;
    public GameObject walk;
    public ParticleSystem transition;
    private int oldState=-1;
    // Start is called before the first frame update
    void Start()
    {
        oldState = stateUpdate(state, oldState);

    }

    // Update is called once per frame
    void Update()
    {

        oldState = stateUpdate(state, oldState);
    }
    private int stateUpdate(int newState, int oldStatus)
    {
        if (newState == oldStatus)
        {
            if (newState == 0)
            {
                run.SetActive(false);

                walk.SetActive(false);

                sleep.SetActive(true);
            }
            else if (newState == 1)
            {
                run.SetActive(false);

                sleep.SetActive(false);

                walk.SetActive(true);

            }
            else if (newState == 2)
            {

                walk.SetActive(false);

                sleep.SetActive(false);

                run.SetActive(true);
            }
        }
        else
        {

            transition.Emit(100);
            oldStatus = newState;
        }

        return oldStatus;

    }
}

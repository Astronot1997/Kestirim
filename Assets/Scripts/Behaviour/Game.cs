using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public GameLight[] GameLights;
    public IReactsToLight[] ReactingObjects;


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        ReactingObjects = FindObjectsOfType<PlayerReact>();
        GameLights = FindObjectsOfType<GameLight>();

        for (int i =0; i<ReactingObjects.Length;i++)
        {

            List<LightReaction> Reactions = new();

            for (int j=0; j<GameLights.Length;j++)
            {

                Vector3 Distance = GameLights[j].transform.position - ReactingObjects[i].Position;

               // Debug.Log(Distance);

                Vector3 DistanceNoZ = Distance;
                DistanceNoZ.y = 0;



                float Magnitude = DistanceNoZ.magnitude;

                //Debug.Log(Magnitude);

                if (Distance.magnitude <= ReactingObjects[i].Range)
                {
                    Reactions.Add(new LightReaction(ReactingObjects[i], GameLights[j],  Magnitude, Distance.normalized));
                }

            }

            if (Reactions.Count > 0)
            {
                LightReaction[] ReactionArray = Reactions.ToArray();
                ReactingObjects[i].React(ReactionArray);
            }
            else
            {
                ((PlayerReact)ReactingObjects[i]).State = 0;
            }   

        }



    }
}

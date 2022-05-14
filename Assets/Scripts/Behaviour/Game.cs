using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameLight[] GameLights;
    public IReactsToLight[] ReactingObjects;


    // Update is called once per frame
    void Update()
    {
        ReactingObjects = FindObjectsOfType<PlayerReact>();
        GameLights = FindObjectsOfType<GameLight>();

        for (int i =0; i<ReactingObjects.Length;i++)
        {

            List<LightReaction> Reactions = new();

            for (int j=0; j<GameLights.Length;j++)
            {

                Vector3 Distance = GameLights[j].transform.position - ReactingObjects[i].Position;
                Vector3 DistanceNoZ = Distance;
                DistanceNoZ.z = 0;

                float Magnitude = DistanceNoZ.magnitude;

                if (Distance.magnitude > (GameLights[j].Range + ReactingObjects[i].Range)/2)
                {
                    Reactions.Add(new LightReaction(ReactingObjects[i], GameLights[j],  Magnitude, Distance.normalized));
                }

            }

            if (Reactions.Count > 0)
            {


                LightReaction[] ReactionArray = Reactions.ToArray();
                ReactingObjects[i].React(ReactionArray);
            }

        }



    }
}

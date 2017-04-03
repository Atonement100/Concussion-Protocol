using UnityEngine;
using System.Collections;

public class footballManager : MonoBehaviour {
    public GameObject cardboard, cubeOne, cubeTwo;

    private Transform movingPlayer;
    private dataManager gameState;
    private int choice = 1;

	void Start () {
        gameState = Object.FindObjectOfType<dataManager>();
        int loads = gameState.getLevelLoads();

        switch (loads)
        {
            case 1:
                choice = gameState.getChoice(0);
                break;
            case 4:
                choice = gameState.getChoice(2);
                break;
            default:
                choice = 1;
                break;
        }

        movingPlayer = GameObject.FindWithTag("footballImpact").transform;
      
	}

    void Update()
    {
        movingPlayer = GameObject.FindWithTag("footballImpact").transform;
        if (choice == 1)
        {
            cardboard.transform.position = movingPlayer.position + movingPlayer.forward * 15 + new Vector3 (0.0f,25.0f);
        }
  //      else
  //      {
  //          cubeOne.transform.position = movingPlayer.position + movingPlayer.forward * 20 + new Vector3(0.0f, 25.0f);
  //          cubeTwo.transform.position = cubeOne.transform.position - movingPlayer.forward*5 - new Vector3(0.0f,10.0f);
  //      }
    }	
}

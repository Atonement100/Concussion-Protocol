using UnityEngine;
using System.Collections;

public class footballManager : MonoBehaviour {
    private dataManager gameState;

	void Start () {
        gameState = Object.FindObjectOfType<dataManager>();
        int loads = gameState.getLevelLoads(), choice;

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

        if (choice == 0)
        {
            GameObject.FindWithTag("primaryCardboard").transform.position = GameObject.FindWithTag("alternateCardboard").transform.position;
            GameObject.FindWithTag("primaryCardboard").transform.rotation = GameObject.FindWithTag("alternateCardboard").transform.rotation;
        }
        
	}
	
}

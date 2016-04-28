using UnityEngine;
using System.Collections;

public class levelManager : MonoBehaviour {
    private dataManager gameState;

	void Start () {
        gameState = Object.FindObjectOfType<dataManager>();
    }
	
	void Update () {
	    
	}
}

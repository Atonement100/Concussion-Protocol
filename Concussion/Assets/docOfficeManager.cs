using UnityEngine;
using System.Collections;

public class docOfficeManager : MonoBehaviour {
    public AudioClip docOfficeOne, docOfficeTwo;

    private dataManager gameState;

	void Start () {
        AudioSource audioHandler = GetComponent<AudioSource>();
        gameState = Object.FindObjectOfType<dataManager>();
        if (gameState.getLevelLoads() > 3 && gameState.getChoice(1) == 2)
        {
            audioHandler.clip = docOfficeTwo;
            audioHandler.Play();
        }
        else
        {
            audioHandler.clip = docOfficeOne;
            audioHandler.Play();
        }
	}
	

}

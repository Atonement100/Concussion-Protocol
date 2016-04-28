using UnityEngine;
using System.Collections;

public class officeManager : MonoBehaviour {
    public AudioClip officeOne, officeTwo, officeIllegal;

    private dataManager gameState;

	void Start () {
        AudioSource audioHandler = GetComponent<AudioSource>();
        gameState = Object.FindObjectOfType<dataManager>();
        int loads = gameState.getLevelLoads();
        switch (loads)
        {
            case 0:
                audioHandler.clip = officeOne;
                audioHandler.Play();
                break;
            case 2:
            case 5:
                GameObject.FindWithTag("primaryCardboard").transform.position = GameObject.FindWithTag("alternateCardboard").transform.position;
                GameObject.FindWithTag("primaryCardboard").transform.rotation = GameObject.FindWithTag("alternateCardboard").transform.rotation;
                audioHandler.clip = officeIllegal;
                audioHandler.Play();
                break;
            case 3:
                GameObject.FindWithTag("disableMe").SetActive(false);
                audioHandler.clip = officeTwo;
                audioHandler.Play();
                break;
            default:
                audioHandler.clip = officeOne;
                audioHandler.Play();
                break;
        }
    }
	
}

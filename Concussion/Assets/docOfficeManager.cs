using UnityEngine;
using System.Collections;

public class docOfficeManager : MonoBehaviour {
    public AudioClip docOfficeOne, docOfficeTwo;

    private dataManager gameState;
    private bool clipOne;
    private float time;

	void Start () {
        AudioSource audioHandler = GetComponent<AudioSource>();
        gameState = Object.FindObjectOfType<dataManager>();
        if (gameState.getLevelLoads() > 3 && gameState.getChoice(1) == 2)
        {
            audioHandler.clip = docOfficeTwo;
            audioHandler.Play();
            clipOne = false;
        }
        else
        {
            audioHandler.clip = docOfficeOne;
            audioHandler.Play();
            clipOne = true;
        }
        time = Time.time;
	}
	
    void Update()
    {
        if (Time.time - time > 30 && Time.time-time < 31)
        {
            FindObjectOfType<playerController>().disorientSetup();
        }
        else if (Time.time - time > 40 && Time.time - time < 41)
        {
            FindObjectOfType<sceneTransition>().causeBlackout();
        }
        if (clipOne)
        {
            if (Time.time - time > docOfficeOne.length)
            {

                FindObjectOfType<sceneTransition>().levelTransferNow = true;
            }
        }
        else
        {
            if (Time.time - time > docOfficeTwo.length)
            {

                FindObjectOfType<sceneTransition>().levelTransferNow = true;
            }
        }
    }

}

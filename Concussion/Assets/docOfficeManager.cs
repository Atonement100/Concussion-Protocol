using UnityEngine;
using System.Collections;

public class docOfficeManager : MonoBehaviour {
    public AudioClip docOfficeOne, docOfficeTwo;

    private dataManager gameState;
    private bool clipOne, disSetupDone = false, boSetupDone = false, transferStarted = false;
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
        gameState.nextLevel = "Office";
	}
	
    void Update()
    {
        if (Time.time - time > 30 && Time.time-time < 31 && !disSetupDone)
        {
            FindObjectOfType<playerController>().disorientSetup();
            FindObjectOfType<playerController>().blurSetup();
            disSetupDone = true;
        }
        else if (Time.time - time > 40 && Time.time - time < 41 && !boSetupDone)
        {
            FindObjectOfType<sceneTransition>().causeBlackout();
            boSetupDone = true;
        }
        if (clipOne)
        {
            if (Time.time - time > docOfficeOne.length && !transferStarted)
            {

                FindObjectOfType<sceneTransition>().levelTransferNow = true;
                transferStarted = true;
            }
        }
        else
        {
            if (Time.time - time > docOfficeTwo.length && !transferStarted)
            {

                FindObjectOfType<sceneTransition>().levelTransferNow = true;
                transferStarted = true;
            }
        }
    }

}

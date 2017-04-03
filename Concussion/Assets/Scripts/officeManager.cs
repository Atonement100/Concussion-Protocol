using UnityEngine;
using System.Collections;

public class officeManager : MonoBehaviour {
    public AudioClip officeOne, officeTwo, officeIllegal;

    private dataManager gameState;
    private float time;
    private bool illegal;

	void Start () {
        AudioSource audioHandler = GetComponent<AudioSource>();
        gameState = Object.FindObjectOfType<dataManager>();
        int loads = gameState.getLevelLoads();
        switch (loads)
        {
            case 0:
                audioHandler.clip = officeOne;
                audioHandler.Play();
                GameObject.FindWithTag("disableWife").SetActive(false);
                GameObject.FindWithTag("disableWife").SetActive(false); //keep two in here.
                break;
            case 2:
            case 5:
                GameObject.FindWithTag("primaryCardboard").transform.position = GameObject.FindWithTag("alternateCardboard").transform.position;
                GameObject.FindWithTag("primaryCardboard").transform.rotation = GameObject.FindWithTag("alternateCardboard").transform.rotation;
                audioHandler.clip = officeIllegal;
                audioHandler.Play();
                time = Time.time;
                illegal = true;
                break;
            case 3:
                GameObject.FindWithTag("disableMe").SetActive(false);
                GameObject.FindWithTag("disableWife").SetActive(false);
                GameObject.FindWithTag("disableWife").SetActive(false); //keep two in here.
                audioHandler.clip = officeTwo;
                audioHandler.Play();
                break;
            default:
                audioHandler.clip = officeOne;
                audioHandler.Play();
                break;
        }
    }
	
    void Update()
    {
        if (illegal)
        {
            if (Time.time - time > officeIllegal.length) 
            {
                FindObjectOfType<sceneTransition>().levelTransferNow = true;
            }
        }
    }
}

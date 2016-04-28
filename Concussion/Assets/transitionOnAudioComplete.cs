using UnityEngine;
using System.Collections;

public class transitionOnAudioComplete : MonoBehaviour {
    public AudioClip clip;
    private float time;

    void Start()
    {
        GetComponent<AudioSource>().Play();
        time = Time.time;
    }

    void Update () {
        if (Time.time - time > clip.length)
        {
            FindObjectOfType<dataManager>().nextLevel = "Office";
            FindObjectOfType<sceneTransition>().levelTransferNow = true;
        }
	}
}

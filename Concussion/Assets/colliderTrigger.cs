using UnityEngine;
using System.Collections;

public class colliderTrigger : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip scream;



	// Use this for initialization
	void Start () {
	
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = scream;
		//audio.Pause ();

	}

    void level()
    {
        FindObjectOfType<dataManager>().storyControl(2);
        FindObjectOfType<sceneTransition>().levelTransferNow = true;
    }

	void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "footballImpact"){
			audioSource.Play ();
            Invoke("level", scream.length);
		}
	}
}

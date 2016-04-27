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

	void OnTriggerEnter(Collider other){


		if(other.gameObject.tag == "footballImpact"){
			//print ("hi");
			audioSource.Play ();
		}
	}
}

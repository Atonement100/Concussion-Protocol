using UnityEngine;
using System.Collections;

public class startMovieOnPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

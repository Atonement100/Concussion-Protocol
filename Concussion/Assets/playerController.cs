using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Cardboard.SDK.Triggered)
        {
            //whatever trigger logic we want here
            GetComponent<Transform>().parent.transform.position += new Vector3 (GetComponent<Transform>().transform.forward.x * Time.deltaTime * 10, 0.0f, GetComponent<Transform>().forward.z*Time.deltaTime*10);
        }
	}
}
/*
float tiltAngle = Cardboard.SDK.HeadPose.Orientation.eulerAngles.z;
		if (GetComponent<Rigidbody>().transform != null)
		{
			GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, tiltAngle);
		}

		if (tiltAngle > 180) { tiltAngle = -(360 - tiltAngle); }

		tiltAngle = -tiltAngle/45;

		float moveHorizontal = Input.GetAxis("Horizontal");
//float moveVertical = Input.GetAxis ("Vertical");

Vector3 movement = new Vector3(tiltAngle, 0.0f, 0.0f);

		if (GetComponent<Rigidbody>().transform != null)
		{
			GetComponent<Rigidbody>().velocity = movement* speed;

GetComponent<Rigidbody>().position = new Vector3
    (
        Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
					0.0f,
					Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
				);
		}
        */
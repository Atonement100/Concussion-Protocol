using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {
    public float speed = 50.0f;
    public bool isMoving = true;
    public GameObject[] destination;

    private float before, after;
    private int i = 0;
    private Vector3[] destinationV = new Vector3[10];

    void Start()
    {
        
        for (int i = 0; i < destination.Length; i++)
        {
            destinationV[i] = new Vector3(destination[i].transform.position.x, transform.position.y, destination[i].transform.position.z);
        }
    }

	void Update () {
	    if (isMoving)
        {
            before = transform.position.magnitude;

            float dist = speed * Time.deltaTime;
            transform.LookAt(destination[i].transform);
            transform.position = Vector3.MoveTowards(transform.position, destinationV[i], dist);

            after = transform.position.magnitude;

            if((after - before) == 0)
            {
                if ((i + 1) < destination.Length) { i++; }
                else { isMoving = false; }
            }


        }
	}
}

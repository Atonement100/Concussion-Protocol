using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {
    public float speed = 50.0f;
    public bool isMoving = true;
    public GameObject[] destination;

    private float before, after;
    private int i = 0;

    void Start()
    {
        for (int i = 0; i < destination.Length; i++)
        {
            destination[i] = new Vector3(destination[i].x, transform.position.y, destination[i].z);
        }
    }

	void Update () {
	    if (isMoving)
        {
            before = transform.position.magnitude;

            float dist = speed * Time.deltaTime;
            transform.LookAt(destination[i].transform);
            transform.position = Vector3.MoveTowards(transform.position, destination[i].transform.position, dist);

            after = transform.position.magnitude;
            print(after - before);


            if((after - before) == 0)
            {
                if ((i + 1) < destination.Length) { i++; }
                else { isMoving = false; }
            }


        }
	}
}

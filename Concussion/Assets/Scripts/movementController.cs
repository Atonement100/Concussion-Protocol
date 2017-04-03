using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour
{
    public float speed = 50.0f, timeToStart = 5.0f;
    public bool isMoving = false;
    public GameObject[] destination;

    private float before, after, timeStarted;
    private int i = 0;
    private Vector3[] destinationV = new Vector3[10];

    void Start()
    {
        timeStarted = Time.time;
        for (int i = 0; i < destination.Length; i++)
        {
            destinationV[i] = new Vector3(destination[i].transform.position.x, transform.position.y, destination[i].transform.position.z);
        }
    }

    void Update()
    {
        if (!isMoving && (Time.time - timeStarted) > timeToStart)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            before = transform.position.magnitude;

            float dist = speed * Time.deltaTime;
			transform.LookAt(new Vector3(destination[i].transform.position.x, 0.0f, destination[i].transform.position.z));
            transform.position = Vector3.MoveTowards(transform.position, destinationV[i], dist);

            after = transform.position.magnitude;

            if ((after - before) == 0)
            {
                if ((i + 1) < destination.Length) { i++; }
                else { isMoving = false; }
            }
        }
    }
}
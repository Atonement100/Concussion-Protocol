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

            //movement
            //GetComponent<Transform>().parent.transform.position += new Vector3 (GetComponent<Transform>().transform.forward.x * Time.deltaTime * 10, 0.0f, GetComponent<Transform>().forward.z*Time.deltaTime*10);

            //raycast for selection of options
            RaycastHit hit;
            if (Physics.Raycast(GetComponent<Transform>().parent.transform.position, GetComponent<Transform>().transform.forward, out hit))
            {
                GameObject wasHit = hit.transform.gameObject;
                print("hit");
                if(wasHit.tag == "decisionTrigger") {
                    choiceTrigger picked = wasHit.GetComponent("choiceTrigger") as choiceTrigger;
                    choiceTrigger unpicked = picked.otherTrigger.GetComponent("choiceTrigger") as choiceTrigger;
                    if (picked != null)
                    {
                        if (picked.choiceDegree == 0)
                        {
                            picked.choiceDegree++;
                            unpicked.choiceDegree++;
                            picked.firstChoice = picked.choiceVar;
                            unpicked.firstChoice = picked.choiceVar;

                            if(picked.firstChoice == 0)
                            {
                                print("first choice was A");
                            }
                            else
                            {
                                print("first choice was B");
                            }
                        }
                        else
                        {
                            if (picked.firstChoice == 0)
                            {
                                if(picked.choiceVar == 0)
                                {
                                    print("first choice was A, second choice was A");
                                }
                                else
                                {
                                    print("first choice was A, second choice was B");
                                }
                            }
                            else
                            {
                                if (picked.choiceVar == 1)
                                {
                                    print("first choice was B, second choice was B");
                                }
                                else
                                {
                                    print("first choice was B, second choice was A");
                                }
                            }
                        }
                    }
                }
            }
            else { print("miss"); }
        }
	}
}

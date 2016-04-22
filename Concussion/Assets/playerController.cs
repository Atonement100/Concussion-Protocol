using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
    private bool startTimeSet = false, lerpTo = true;
    private float startTime, timeDiff;
    private Quaternion keep, temp;
    private int lerpRate = 5;

    void Start()
    {
        keep = GetComponent<Transform>().parent.transform.rotation;
        temp = keep;
    }

    void headShakeSetup()
    {
        temp = GetComponent<Transform>().parent.transform.rotation;
        keep = temp;

        temp.eulerAngles = new Vector3(
           Random.Range((float)(temp.eulerAngles.x - 50), (float)(temp.eulerAngles.x + 50)),
           //temp.eulerAngles.x,
           Random.Range((float)(temp.eulerAngles.y - 50), (float)(temp.eulerAngles.y + 50)),
           //temp.eulerAngles.y,
           temp.eulerAngles.z);
           //Mathf.Clamp(Random.Range((float)(temp.eulerAngles.z - 50), (float)(temp.eulerAngles.z + 50)),-50,50));

        if (!startTimeSet)
        {
            startTime = Time.time;
            startTimeSet = true;
        }
    }

	void Update () {

        #region CardboardTrigger
        if (Cardboard.SDK.Triggered)
        {

            //whatever trigger logic we want here

            //movement
            //GetComponent<Transform>().parent.transform.position += new Vector3 (GetComponent<Transform>().transform.forward.x * Time.deltaTime * 10, 0.0f, GetComponent<Transform>().forward.z*Time.deltaTime*10);

            #region Raycasting
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
            #endregion 
        }
        #endregion

        if (Input.GetKeyDown("a"))
        {
            headShakeSetup();
        }
        if (Input.GetKeyDown("z"))
        {
            sceneTransition tempST = FindObjectOfType<sceneTransition>();
            tempST.transferNow = true;
        }


        #region headShakeHandler
        timeDiff = (Time.time - startTime)*lerpRate;

        if (timeDiff < 1.1 && startTimeSet)
        {
            if (lerpTo)
            {
                GetComponent<Transform>().parent.transform.rotation = Quaternion.Lerp(keep, temp, timeDiff*3);
            }
            else
            {
                GetComponent<Transform>().parent.transform.rotation = Quaternion.Lerp(temp, keep, timeDiff*3);
            }
        }
        else if (lerpTo && startTimeSet)
        {
            lerpTo = false;
            startTime = Time.time;
        }
        else
        {
            lerpTo = true;
            startTimeSet = false;
        }
        #endregion
    }
}

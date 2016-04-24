using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
    public float disorientLength = 10.0f;
    [Range(5.0f, 89.0f)]
    public float minDisorientAngle = 10.0f, maxDisorientAngle = 30.0f;

    private bool startTimeSet = false, disorientTimeSet = false, lerpTo = true, rotationFixed = true;
    private float startTime, timeDiff,
        disorientTime, disorientDiff, diffSum = 0;
    private Quaternion keep, temp, disKeep, disTemp;
    private Quaternion[] tempRotations;
    private int lerpRate = 5, tRotIt = 0;


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

    void disorientSetup()
    {
        disTemp = GetComponent<Transform>().parent.transform.rotation;
        disKeep = disTemp;

        tempRotations = new Quaternion[6];

        tempRotations[0] = disTemp;
        tempRotations[1] = disTemp;
        float disorientAngle = Random.Range(10, 30);
        tempRotations[0].eulerAngles += new Vector3(disTemp.eulerAngles.x, disTemp.eulerAngles.y, disTemp.eulerAngles.z - disorientAngle);
        tempRotations[1].eulerAngles += new Vector3(disTemp.eulerAngles.x, disTemp.eulerAngles.y, disTemp.eulerAngles.z + disorientAngle);

        /*
        //used for disorient rotation cycle, not in use presently

        for (int i = 0; i < tempRotations.Length; i++)
        {
            tempRotations[i] = disTemp;
            tempRotations[i].eulerAngles += new Vector3(disTemp.eulerAngles.x,disTemp.eulerAngles.y, disTemp.eulerAngles.z + (Random.Range(-30,30)));
        }
        */

        if (!disorientTimeSet)
        {
            disorientTime = Time.time;
            disorientTimeSet = true;
        }
    }

    void Update()
    {

        #region CardboardTrigger
        if (Cardboard.SDK.Triggered)
        {
            #region removable binds (used for testing)
            if (Input.GetKeyDown("a"))
            {
                headShakeSetup();
            }
            if (Input.GetKeyDown("q"))
            {
                disorientSetup();
            }
            if (Input.GetKeyDown("z"))
            {
                sceneTransition tempST = FindObjectOfType<sceneTransition>();
                tempST.transferNow = true;
            }
            #endregion

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
                if (wasHit.tag == "decisionTrigger")
                {
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

                            if (picked.firstChoice == 0)
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
                                if (picked.choiceVar == 0)
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

        #region headShakeHandler
        timeDiff = (Time.time - startTime) * lerpRate;

        if (timeDiff < 1.1 && startTimeSet)
        {
            if (lerpTo)
            {
                GetComponent<Transform>().parent.transform.rotation = Quaternion.Lerp(keep, temp, timeDiff * 3);
            }
            else
            {
                GetComponent<Transform>().parent.transform.rotation = Quaternion.Lerp(temp, keep, timeDiff * 3);
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

        #region disorientHandler
        disorientDiff = (Time.time - disorientTime);

        if (disorientDiff < disorientLength && disorientTimeSet)
        {
            //set up rotation of rotations
            GetComponent<Transform>().parent.transform.rotation = Quaternion.Lerp(tempRotations[tRotIt], tempRotations[tRotIt + 1], (Mathf.Sin(disorientDiff + diffSum) + 1) / 2);
            rotationFixed = false;

            /*

            //will review this to find a better way to make it work with multiple rotations nicely. easy to do with linear lerp but sinusoidal causes conflict
            //however sin(time) etc lerp looks nicer than linear lerp

            if (disorientDiff > disorientLength/tempRotations.Length && tRotIt+2 < tempRotations.Length)
            {
                diffSum += disorientDiff;
                disorientTime = Time.time;
                tRotIt++;
                tempRotations[tRotIt] = GetComponent<Transform>().parent.transform.rotation;
            }
            else if (tRotIt+1 > tempRotations.Length)
            {
                disorientTimeSet = false;
            }
            */
        }
        else
        {
            if (!rotationFixed)
            {
                disTemp = GetComponent<Transform>().parent.transform.rotation;
                GetComponent<Transform>().parent.transform.rotation = Quaternion.Lerp(disTemp, disKeep, (disorientDiff - disorientLength) / 3);
                if (((disorientDiff - disorientLength) / 3) > 1)
                {
                    rotationFixed = true;
                }
            }
            disorientTimeSet = false;
            tRotIt = 0;
            diffSum = 0;
        }
        #endregion
    }
}

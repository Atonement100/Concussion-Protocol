using UnityEngine;
using System.Collections;



public class dataManager : MonoBehaviour
{
    public GameObject self;
    public string nextDate = "March 12th 2016", nextLevel = "Office", currentLevel = "Office";

    private string[] dates =
        { "March 12th 2016",
        "November 2nd 2016",
        "November 6th 2016",
        "March 17th 2017",
        "November 1st 2016",
        "November 8th 2016"};
    private int levelLoads = 0, choicesMade = 0;
    private int[] choices = new int[5];
    private bool firstSupport, secondSupport, firstHitWasIllegal;
    private static string football = "Football Stadium", office = "Office", home = "Home", docOffice = "DocOffice"; // defines for level loading
    private static int noSupport = 0, hitWasIllegal = 1;

    public int getChoice(int level)
    {
        return choices[level];
    }

    public int getLevelLoads()
    {
        return levelLoads;
    }


    public void incrementProgress()
    {
        levelLoads++;
        nextDate = dates[levelLoads];
    }

    public void storyControl(int choice)
    {
        print("CHOICE GIVEN WAS: " + choice);
        //Could switch this to a switch statement instead for minor performance improvement
        if (choicesMade == 0)
        {
            if (choice == noSupport)
            {
                nextLevel = football;
                firstSupport = false;
                choices[0] = 0;
            }                                              //Either decision goes same place but saves first choice
            else if (choice == 1)
            {
                nextLevel = football;
                firstSupport = true;
                choices[0] = 1;
            }                                               //for reference later. 0 is NO SUPPORT. Support is 1.
        }
        else if (choicesMade == 1)
        {
            if (choice == hitWasIllegal)
            {
                nextLevel = office;
                firstHitWasIllegal = true;
                choices[1] = 1;
            }
            else if (choice == 0)
            {
                nextLevel = home;
                firstHitWasIllegal = false;
                choices[1] = 0;
            }
            else if (choice == 2) //i.e. first choice was no support
            {
                nextLevel = docOffice;
                choices[1] = 2;
            }
        }
        else if (choicesMade == 2)
        {
            if (choice == noSupport)
            {
                nextLevel = football;
                secondSupport = false;
                choices[2] = 0;
            }
            else if (choice == 1)
            {
                nextLevel = football;
                secondSupport = true;
                choices[2] = 1;
            }
        }
        else if (choicesMade == 3)
        {

            if (choice == hitWasIllegal)
            {
                if (choices[1] == hitWasIllegal)    //override for double illegal hit
                {
                    nextLevel = home;
                    choices[3] = 0;
                }
                else
                {
                    nextLevel = office;
                    choices[3] = 1;
                }
            }
            else if (choice == 0)
            {
                if (choices[1] == 0) //override for double legal hit
                {
                    nextLevel = office;
                    choices[3] = 1;
                }
                else
                {
                    nextLevel = home;
                    choices[3] = 0;
                }
            }
            else if (choice == 2)
            {
                nextLevel = docOffice;
                choices[3] = 2;
            }
        }


        choicesMade++;
    }

    void Start()
    {
        dataManager[] dMholder = FindObjectsOfType<dataManager>();
        if (dMholder.Length > 1) { Destroy(self); }
        nextDate = dates[levelLoads];
        Object.DontDestroyOnLoad(Object.FindObjectOfType<dataManager>());
        choices.SetValue(2, 0, choices.Length - 1);
    }

    void Update()
    {
        print(levelLoads);
    }
}
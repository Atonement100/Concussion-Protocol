using UnityEngine;
using System.Collections;



public class dataManager : MonoBehaviour
{
    public GameObject self;
    public string nextDate = "March 12th 2016", nextLevel = "Football Stadium", currentLevel = "Office";

    private string[] dates = 
        { "March 12th 2016",
        "November 2nd 2016",
        "November 6th 2016",
        "March 17th 2017" };
    private int levelLoads = 0, firstChoice, choicesMade=0;
    private static string football = "Football Stadium", office = "Office", home = "Home", docOffice = "DocOffice"; // defines for level loading
    private static int noSupport = 0, illegalHit = 0;

    public void incrementProgress()
    {
        levelLoads++;
        nextDate = dates[levelLoads];
    }

    public void storyControl(int choice)
    {
        //Could switch this to a switch statement instead for minor performance improvement
        if (choicesMade == 0)
        {
            firstChoice = choice;
            if (choice == noSupport) nextLevel = football; //Either decision goes same place but saves first choice
            else nextLevel = football;                     //for reference later. 0 is NO SUPPORT. Support is 1.
        }
        else if(choicesMade == 1)
        {
            if (choice == illegalHit) nextLevel = office;
            else nextLevel = home;
        }


        choicesMade++;
    }

    void Start()
    {
        nextDate = dates[levelLoads];
        Object.DontDestroyOnLoad(Object.FindObjectOfType<dataManager>());
    }

    void Update()
    {
        print(levelLoads);
    }
}
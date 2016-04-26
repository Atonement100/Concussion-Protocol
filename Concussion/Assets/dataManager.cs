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
    private int levelLoads = 0;

    public void incrementProgress()
    {
        levelLoads++;
        nextDate = dates[levelLoads];
    }

    void Start()
    {
        nextDate = dates[levelLoads];
        Object.DontDestroyOnLoad(Object.FindObjectOfType<dataManager>());
    }
}
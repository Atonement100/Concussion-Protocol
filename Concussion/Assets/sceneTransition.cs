using UnityEngine;
using System.Collections;

public class sceneTransition : MonoBehaviour {
    public bool transferNow = true, levelTransferNow = false;
    public GameObject player;//, destination;
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.2f, blackoutLength = 10.0f;
    public updateText targetTextScript;
    public dataManager gameState;

    private string nextDate = "January 2 2016", nextLevel;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    void Start()
    {
        gameState = Object.FindObjectOfType<dataManager>(); 
    }

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }

    public void causeBlackout()
    {
        StartCoroutine(blackout());
    }

    void Update()
    {
        if (transferNow) // put the appropriate trigger for this event. probably best to call sceneTransfer() or transferNow = true from wherever necessary
        {
            StartCoroutine(sceneTransfer());
        }
        else
        {
            StopCoroutine(sceneTransfer());
        }

        if (levelTransferNow)
        {
            StartCoroutine(levelTransfer());
        }
    }
/*
    void movePlayer(){
        player.transform.position = new Vector3(destination.transform.position.x, player.transform.position.y, destination.transform.position.z);
    }
*/
    IEnumerator sceneTransfer()
    {
        BeginFade(1);
        yield return new WaitForSeconds(1);
        //movePlayer();
        BeginFade(-1);
        targetTextScript.changeText(gameState.nextDate);
        transferNow = false;
        yield break;
    }

    IEnumerator levelTransfer()
    {
        BeginFade(1);
        yield return new WaitForSeconds(3);
        gameState.incrementProgress();
        Application.LoadLevel(gameState.nextLevel);
        yield break;
    }

    IEnumerator blackout()
    {
        BeginFade(1);
        yield return new WaitForSeconds(blackoutLength);
        BeginFade(-1);
        StopCoroutine(blackout());
    }

}
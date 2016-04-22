using UnityEngine;
using System.Collections;

public class sceneTransition : MonoBehaviour {
    public bool transferNow = false;
    public GameObject player, destination;
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.0f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

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

    void Update()
    {
        if (transferNow) // put the appropriate trigger for this event. probably best to call sceneTransfer() or transferNow = true from wherever necessary
        {
            StartCoroutine(sceneTransfer());
        }
    }

    void movePlayer(){
        player.transform.position = new Vector3(destination.transform.position.x, player.transform.position.y, destination.transform.position.z);
    }

    IEnumerator sceneTransfer()
    {
        BeginFade(1);
        yield return new WaitForSeconds(1);
        movePlayer();
        BeginFade(-1);
        yield break;
    }

}


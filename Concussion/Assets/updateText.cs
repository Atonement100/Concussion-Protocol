using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class updateText : MonoBehaviour {
    public Text text;

    public void changeText(string newText)
    {
        StartCoroutine(changeTextHandler(newText));    
    }

    IEnumerator changeTextHandler(string newText)
    {
        text.text = newText;
        yield return new WaitForSeconds(2.0f);
        text.text = " ";
        yield break;
    }
}

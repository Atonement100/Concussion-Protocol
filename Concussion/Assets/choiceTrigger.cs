using UnityEngine;
using System.Collections;

public class choiceTrigger : MonoBehaviour {
    private int choiceVar;

    public int getChoiceVar()
    {
        return choiceVar;
    }
    public void setChoiceVar(int newChoiceVar)
    {
        this.choiceVar = newChoiceVar;
    }
}

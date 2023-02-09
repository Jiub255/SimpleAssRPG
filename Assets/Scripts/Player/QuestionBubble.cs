using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBubble : MonoBehaviour
{
    public GameObject questionBubble;
    public bool questionMarkActive;

    public void ToggleQuestionMark()
    {
        questionMarkActive = !questionMarkActive;
        if (questionMarkActive)
        {
            questionBubble.SetActive(true);
        }
        else
        {
            questionBubble.SetActive(false);
        }
    }

    /*
    public void Enable()
    {
        questionBubble.SetActive(true);
    }

    public void Disable()
    {
        questionBubble.SetActive(false);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject branchingCanvas;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject responsePrefab;
    [SerializeField] private TextAssetValue dialogValue;
    [SerializeField] private Story myStory;
    [SerializeField] private GameObject dialogContent;
    [SerializeField] private GameObject responseContent;
    [SerializeField] private ScrollRect dialogScroll;

    public void EnableCanvas()
    {
        branchingCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void SetStory()
    {
        if (dialogValue.value)
        {
            myStory = new Story(dialogValue.value.text);
        }
        else
        {
            Debug.Log("fucked up");
        }
    }

    public void RefreshView()
    {
        while (myStory.canContinue)
        {
            MakeNewDialog(myStory.Continue());
        }

        if (myStory.currentChoices.Count > 0)
        {
            MakeNewChoices();
        }
        else
        {
            branchingCanvas.SetActive(false);
        }
        StartCoroutine(ScrollCo());
    }

    IEnumerator ScrollCo()
    {
        yield return null;
        dialogScroll.verticalNormalizedPosition = 0f;
    }

    void MakeNewDialog(string newDialog)
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab,
            dialogContent.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    void MakeNewResponse(string newDialog, int choiceValue)
    {
        ResponseObject newResponseObject = Instantiate(responsePrefab,
            responseContent.transform).GetComponent<ResponseObject>();
        newResponseObject.Setup(newDialog, choiceValue);
        Button responseButton = newResponseObject.gameObject.GetComponent<Button>();
        if (responseButton)
        {
            responseButton.onClick.AddListener(delegate { ChooseChoice(choiceValue); });
        }
    }

    void MakeNewChoices()
    {
        for (int i = 0; i < responseContent.transform.childCount; i++)
        {
            Destroy(responseContent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < myStory.currentChoices.Count; i++)
        {
            MakeNewResponse(myStory.currentChoices[i].text, i);
        }
    }

    void ChooseChoice(int choice)
    {
        myStory.ChooseChoiceIndex(choice);
        RefreshView();
    }
}

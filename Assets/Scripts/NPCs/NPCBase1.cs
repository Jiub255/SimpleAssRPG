using UnityEngine;

public class NPCBase1 : Interactable
{
    //ref to intermediate dialog value
    [SerializeField] private TextAssetValue dialogValue;
    //ref to npc's dialog
    [SerializeField] private TextAsset myDialog;
    //send signal to canvases
    [SerializeField] private Signal branchingDialogSignal;

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("attack"))
            {
                dialogValue.value = myDialog;
                branchingDialogSignal.Raise();
            }
        }
    }
}

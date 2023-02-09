using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerClickHandler
{
    public static event UnityAction<int> newGamePushed;
    public static event UnityAction<int> savePushed;
    public static event UnityAction<int> loadPushed;
    public static event UnityAction<int> resetPushed;

    public int saveFileNumber;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left && CompareTag("New Game"))
        {
            newGamePushed?.Invoke(saveFileNumber);
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left && CompareTag("Save Game"))
        {
            savePushed?.Invoke(saveFileNumber);
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left && CompareTag("Load Game"))
        {
            loadPushed?.Invoke(saveFileNumber);
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left && CompareTag("Reset Game"))
        {
            resetPushed?.Invoke(saveFileNumber);
        }

        Debug.Log(name + " Game Object Left Clicked!");
    }
}
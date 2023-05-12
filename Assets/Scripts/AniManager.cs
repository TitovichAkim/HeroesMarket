using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AniManager: MonoBehaviour
{
    public GameObject productPanelsScrollContent;
    public RoomsPanelManager roomsPanelManager;
    public GameObject targetPanelToOpen;
    public Button openRoomButton;

    public void OnAmimationEnd ()
    {
        if (targetPanelToOpen != null)
        {
            targetPanelToOpen.GetComponent<Animator>().SetBool("Active", true);
        }
        //openRoomButton.interactable = true;
        targetPanelToOpen = null;
    }

    public void  Change≈heStateOfTheButtons ()
    {
        openRoomButton.interactable = true;
        if (targetPanelToOpen != null)
        {
            targetPanelToOpen.GetComponent<AniManager>().openRoomButton.interactable = false;
        }
    }

    public void InactiveAnimation ()
    {
        roomsPanelManager.animationActive = false;
    }
}
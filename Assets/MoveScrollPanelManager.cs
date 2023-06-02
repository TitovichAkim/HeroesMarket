using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveScrollPanelManager:MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip tapAudioClip;

    public RoomsPanelManager[] roomPanelManagers;

    public LayoutElement[] scrollPanelLayoutElements;
    public GameObject[] scrollPanelTabs;
    public TextMeshProUGUI[] scrollPanelTabsText;

    public int panelIndex;
    public void OpenTargetPanelScroll (GameObject panelToOpen)
    {
        foreach(RoomsPanelManager room in roomPanelManagers)
        {
            for(int i = 0; i < room.childPanelsGOArray.Length; i++)
            {
                if(room.childPanelsGOArray[i] == panelToOpen)
                {
                    panelIndex = i;
                    break;
                }
            }
        }

        for(int i = 0; i < roomPanelManagers.Length; i++)
        {
            roomPanelManagers[i].SwitchPanel(roomPanelManagers[i].childPanelsGOArray[panelIndex]);
        }
    }

    public void RedrawButtons (int index)
    {
        audioSource.PlayOneShot(tapAudioClip);

        for(int i = 0; i < scrollPanelTabs.Length; i++)
        {
            if(i != index)
            {
                scrollPanelTabs[i].GetComponent<Button>().interactable = transform;
                scrollPanelTabsText[i].color = new Color(0.55f, 0.643f, 0.773f, 0.39f);
                scrollPanelLayoutElements[i].ignoreLayout = true;
                roomPanelManagers[i].MoveThePanel();
            }
            else
            {
                scrollPanelTabs[i].GetComponent<Button>().interactable = false;
                scrollPanelTabsText[i].color = new Color(0.55f, 0.643f, 0.773f, 1f);
                scrollPanelLayoutElements[i].ignoreLayout = false;
            }
        }
    }
}
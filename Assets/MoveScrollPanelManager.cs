using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScrollPanelManager : MonoBehaviour
{
    public RoomsPanelManager[] roomPanelManagers;
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

        for (int i = 0; i < roomPanelManagers.Length; i++)
        {
            roomPanelManagers[i].SwitchPanel(roomPanelManagers[i].childPanelsGOArray[panelIndex]);
        }
    }
}
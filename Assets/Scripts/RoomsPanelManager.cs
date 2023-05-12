using UnityEngine;
using UnityEngine.UI;

public class RoomsPanelManager: MonoBehaviour
{
    public GameObject leftPanelGO;
    public GameObject rightPanelGO;
    public GameObject[] childPanelsGOArray; // Массив, сохраняющий все существующие подчиненные этой комнате скроллы. Его величина определяется в WorldFactory
    public bool animationActive = false;
    public void OpenFirstPnel ()
    {
        animationActive = false;
        if(!childPanelsGOArray[0].GetComponent<Animator>().GetBool("Active"))
        {
            SwitchPanel(childPanelsGOArray[0]);
        }
    }

    public void MoveThePanel ()
    {
        this.transform.localPosition += new Vector3(0, 10000, 0);
    }
    public void SwitchPanel (GameObject panelToOpen)
    {
        panelToOpen.GetComponent<Animator>().SetBool("DefaultState", false);
        if (!animationActive)
        {
            animationActive = true;
            GameObject activeProductPanels = null;
            foreach(GameObject GO in childPanelsGOArray)
            {
                if(GO.GetComponent<Animator>().GetBool("Active"))
                {
                    activeProductPanels = GO;
                    Debug.Log("Нашел!");
                    break;
                }
            }
            if(activeProductPanels != null)
            {
                activeProductPanels.GetComponent<AniManager>().targetPanelToOpen = panelToOpen;
                activeProductPanels.GetComponent<Animator>().SetBool("Active", false);
            } 
            else
            {
                panelToOpen.GetComponent<Animator>().SetBool("Active", true);
                panelToOpen.GetComponent<AniManager>().openRoomButton.interactable = false;
            }
        }
    }
}

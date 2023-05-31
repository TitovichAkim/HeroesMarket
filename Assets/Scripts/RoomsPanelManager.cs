using UnityEngine;
using UnityEngine.UI;

public class RoomsPanelManager: MonoBehaviour
{
    public GameObject leftPanelGO;
    public GameObject rightPanelGO;
    public GameObject[] childPanelsGOArray; // Массив, сохраняющий все существующие подчиненные этой комнате скроллы. Его величина определяется в WorldFactory
    public MoveScrollPanelManager moveScrollPanelManager;
    public bool animationActive = false;

    public void OpenFirstPnel ()
    {
        animationActive = false;
        //foreach(GameObject panel in childPanelsGOArray)
        //{
        //    if (panel.GetComponent<Animator>().GetBool("Active"))
        //    {
        //        panel.GetComponent<Animator>().SetBool("DefaultState", true);
        //        panel.GetComponent<Animator>().SetBool("Active", false);
        //    }
        //}
        ////childPanelsGOArray[NumberFormatter.activeProductsRoom[NumberFormatter.activeRoom]].GetComponent<LayoutElement>().ignoreLayout = false;
        //Debug.Log("Загружаю " + NumberFormatter.activeProductsRoom[NumberFormatter.activeRoom]);
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
        for (int i = 0; i < childPanelsGOArray.Length; i++)
        {
            if(childPanelsGOArray[i] == panelToOpen)
            {
                NumberFormatter.activeProductsRoom[NumberFormatter.activeRoom] = i;
                Debug.Log("Сохранили " + NumberFormatter.activeProductsRoom[NumberFormatter.activeRoom]);
                break;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour
{
    public static bool Training = false;
    public DialogueTextsSO dialog;

    public GameObject traderDialogBox;
    public GameObject trainingPanelsGO;
    public TextMeshProUGUI traderDialogText;

    public CanvasGroup shopCanvasGroup;
    public CanvasGroup traderCanvasGroup;
    public CanvasGroup traderDialogBoxCanvasGroup;
    public CanvasGroup openProductsButtonCanvasGroup;
    public CanvasGroup productBuyButtonCanvasGroup;
    public CanvasGroup productPanelCanvasGroup;
    public CanvasGroup openManagersButtonCanvasGroup;
    public CanvasGroup managerBuyButtonCanvasGroup;
    public CanvasGroup managerTrainingPanelCanvasGroup;
    public CanvasGroup openImprovementsButtonCanvasGroup;
    public CanvasGroup improvementTrainingPanelCanvasGroup;
    public CanvasGroup improvementBuyButtonCanvasGroup;
    //public CanvasGroup  тут будет для фактории 

    [SerializeField]private int _trainingStatus = 1;    // 0 - Обучение выключено
                                        // 1 - Обучение с самого начала

    public int trainingStatus
    {
        get
        {
            return (_trainingStatus);
        }
        set
        {
            _trainingStatus = value;
            PlayerPrefs.SetInt("TrainingStatus", trainingStatus);
        }
    }

    public void ShowTraining ()
    {
        if(trainingStatus != 0)
        {
            Training = true;
            shopCanvasGroup.alpha = 0.1f;
            shopCanvasGroup.interactable = false;
            traderCanvasGroup.ignoreParentGroups = true;
            traderDialogBox.SetActive(true);
            traderDialogBoxCanvasGroup.ignoreParentGroups = true;


            switch(trainingStatus)
            {

                case 1:
                    traderDialogText.text = dialog.trainigTextBase[0];
                    productBuyButtonCanvasGroup.ignoreParentGroups = true;
                    productBuyButtonCanvasGroup.GetComponent<Button>().onClick.AddListener(() => NextStep());
                    // купить первый товар
                    break;
                case 2:
                    productBuyButtonCanvasGroup.GetComponent<Button>().onClick.RemoveAllListeners();
                    traderDialogText.text = dialog.trainigTextBase[1];
                    productBuyButtonCanvasGroup.ignoreParentGroups = false;
                    productPanelCanvasGroup.ignoreParentGroups = true;
                    productPanelCanvasGroup.GetComponent<Button>().onClick.AddListener(() => NextStep());
                    // Начать продавать товар
                    break;
                case 3:
                    productPanelCanvasGroup.GetComponent<Button>().onClick.RemoveAllListeners();
                    traderDialogText.text = dialog.trainigTextBase[2];
                    productPanelCanvasGroup.ignoreParentGroups = true;
                    TrainingBreak();
                    // Заработайте 1000
                    break;
                case 4:
                    if(openManagersButtonCanvasGroup.gameObject.GetComponent<Button>().interactable)
                    {
                        openManagersButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => NextStep());
                        traderDialogText.text = dialog.trainigTextBase[3];
                        productPanelCanvasGroup.ignoreParentGroups = false;
                        openManagersButtonCanvasGroup.ignoreParentGroups = true;
                        // Откройте менеджеров
                    }
                    else
                    {
                        NextStep();
                    }
                    break;
                case 5:
                    openManagersButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    traderDialogText.text = dialog.trainigTextBase[4];
                    openManagersButtonCanvasGroup.ignoreParentGroups = false;
                    managerBuyButtonCanvasGroup.ignoreParentGroups = true;
                    managerBuyButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => NextStep());
                    // Купите менеджера
                    break;
                case 6:
                    managerBuyButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    managerBuyButtonCanvasGroup.ignoreParentGroups = false;
                    traderCanvasGroup.ignoreParentGroups = false;
                    traderDialogBox.SetActive(false);
                    trainingPanelsGO.SetActive(true);
                    managerTrainingPanelCanvasGroup.gameObject.SetActive(true);
                    managerTrainingPanelCanvasGroup.ignoreParentGroups = true;
                    managerTrainingPanelCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => TrainingBreak());
                    // Заработайте 10000
                    break;
                case 7:
                    if(openImprovementsButtonCanvasGroup.gameObject.GetComponent<Button>().interactable)
                    {
                        openImprovementsButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => NextStep());
                        traderDialogText.text = dialog.trainigTextBase[5];
                        managerTrainingPanelCanvasGroup.ignoreParentGroups = false;
                        openImprovementsButtonCanvasGroup.ignoreParentGroups = true;
                        // Откройте улучшения
                    }
                    else
                    {
                        NextStep();
                    }
                    break;
                case 8:
                    openImprovementsButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    traderDialogText.text = dialog.trainigTextBase[6];
                    openImprovementsButtonCanvasGroup.ignoreParentGroups = false;
                    improvementBuyButtonCanvasGroup.ignoreParentGroups = true;
                    improvementBuyButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => NextStep());
                    // Купите Улучшение
                    break;
                case 9:
                    improvementBuyButtonCanvasGroup.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    improvementBuyButtonCanvasGroup.ignoreParentGroups = false;
                    traderCanvasGroup.ignoreParentGroups = false;
                    traderDialogBox.SetActive(false);
                    trainingPanelsGO.SetActive(true);
                    improvementTrainingPanelCanvasGroup.gameObject.SetActive(true);
                    improvementTrainingPanelCanvasGroup.ignoreParentGroups = true;
                    improvementTrainingPanelCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => TrainingBreak());
                    // Заработайте миллиард
                    break;
                case 10:
                    trainingStatus = 0;
                    TrainingBreak();
                    break;
            }
        }
    }

    public void NextStep ()
    {
        TrainingBreak();
        trainingStatus++;
        ShowTraining();
    }

    public void TrainingBreak ()
    {
        Training = false;
        shopCanvasGroup.alpha = 1f;
        shopCanvasGroup.interactable = true;
        traderCanvasGroup.ignoreParentGroups = false;
        traderDialogBox.SetActive(false);
        traderDialogBoxCanvasGroup.ignoreParentGroups = false;
    }
}

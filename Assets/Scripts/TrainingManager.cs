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

    private void Start ()
    {
        if (PlayerPrefs.HasKey("TrainingStatus"))
        {
            Debug.Log("Существует ключ TrainingStatus");
            trainingStatus = PlayerPrefs.GetInt("TrainingStatus");
        }
    }

    public void ShowTraining ()
    {
        Debug.Log("Началась тренировка");
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
                    trainingPanelsGO.SetActive(true);
                    managerTrainingPanelCanvasGroup.gameObject.SetActive(true);
                    managerTrainingPanelCanvasGroup.gameObject.GetComponent<Button>().onClick.AddListener(() => NextStep());
                    // Заработайте 10000
                    break;
                case 7:
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

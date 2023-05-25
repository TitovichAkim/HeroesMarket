using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager:MonoBehaviour
{
    public GameObject roomsPanelGO;
    public GameObject productsPanel;
    public GameObject managersPanel;
    public GameObject improvementsPanel;
    public GameObject factoryesPanel;
    public GameObject traderGO;
    public GameObject traderDialogBoxGO;
    public GameObject traderDialogText;

    public GameObject openProductsButtonGO;
    public GameObject openManagersButtonGO;
    public GameObject openImprovementsButtonGO;
    public GameObject trainingPanelsGO;
    public GameObject managerTrainingPanel;
    public GameObject improvementTrainingPanel;


    public TextMeshProUGUI numbersOfCoinsFloatText;
    public Text upgradesNumberText;
    public Image[] ProductIconsOnTheShelf;


    public TrainingManager trainingManager;
    public ProductPanelManager[][] productPanelsArray;
    public ManagerPanelManager[][] managerPanelsArray;
    public ImprovementPanelManager[][] improvementPanelArray;

    [SerializeField]private bool[][] _managersStatesArray;
    [SerializeField]private bool[][] _improvementsStatesArray;

    private int[] _upgradeNumbers = {1, 10, 25, 100};
    private int _upgradeIndex = 0;

    private float _coins;

    public float coins
    {
        get
        {
            return (_coins);
        }
        set
        {
            _coins = value;
            NumberFormatter.FormatAndRedraw(_coins, numbersOfCoinsFloatText);
            PlayerPrefs.SetFloat("Coin", _coins);
            _RedrawUpgradeButtons();
            CheckTheTraining();
        }
    }
    public bool[][] managersStatesArray
    {
        get
        {
            return (_managersStatesArray);
        }
        set
        {
            _managersStatesArray = value;

        }
    }
    public bool[][] improvementsStatesArray
    {
        get
        {
            return (_improvementsStatesArray);
        }
        set
        {
            _improvementsStatesArray = value;
            SaveImprovementsStates();
        }
    }

    public void StartShop ()
    {
        coins = PlayerPrefs.GetFloat("Coin");
        if(coins == 0)
        {
            coins = 5;
        }
        _managersStatesArray = new bool[managerPanelsArray.Length][];
        _improvementsStatesArray = new bool[improvementPanelArray.Length][];
        for(int i = 0; i < managerPanelsArray.Length; i++)
        {
            _managersStatesArray[i] = new bool[managerPanelsArray[i].Length];
            _improvementsStatesArray[i] = new bool[improvementPanelArray[i].Length];
        }

        _RedrawUpgradeButtons();
        _LoadManagersState();
        _LoadImprovementsStates();
    }

    public void UpgradeIndexUp ()
    {
        _upgradeIndex++;
        if(_upgradeIndex > 3)
        {
            _upgradeIndex = 0;
        }
        upgradesNumberText.text = $"x {_upgradeNumbers[_upgradeIndex]}";
        //foreach(ProductPanelManager prodMan in productPanelsArray)
        //{
        //    prodMan.upgradesNumber = _upgradeNumbers[_upgradeIndex];
        //}
    }

    public void SaveManagersStates () // Сохранить состояние менеджеров (куплен/не куплен)
    {
        for(int i = 0; i < managersStatesArray.Length; i++)
        {
            for(int j = 0; j < managersStatesArray[i].Length; j++)
            {
                int state = 0;
                if(managersStatesArray[i][j])
                {
                    state = 1;
                }
                PlayerPrefs.SetInt($"{managerPanelsArray[i][j].managerSO.managersName}.Manager", state);
            }
        }
    }
    public void SaveImprovementsStates () // Сохранить состояния улучшений (куплен/не куплен)
    {
        for(int i = 0; i < improvementsStatesArray.Length; i++)
        {
            for(int j = 0; j < improvementsStatesArray[i].Length; j++)
            {
                int state = 0;
                if(improvementsStatesArray[i][j])
                {
                    state = 1;
                }
                PlayerPrefs.SetInt($"{improvementPanelArray[i][j].improvementSO.improvementsName}.Improvement", state);
            }
        }
    }

    public void RedrawIconsOnTheShelf ()
    {
        //for(int i = 0; i < ProductIconsOnTheShelf.Length; i++)
        //{
        //    if(productPanelsArray[i] != null)
        //    {
        //        ProductIconsOnTheShelf[i].enabled = productPanelsArray[i].productInvestments > 0;
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}
    }
    public void ApplyImprovementState (int type, int target, int indexI, int indexJ)
    {
        float improvementsValue = improvementPanelArray[indexI][indexJ].improvementSO.improvementsValue;
        switch(type)
        {
            case 0:
                productPanelsArray[indexI][target].multiplierProductRevenue *= improvementsValue;
                break;
            case 1:
                productPanelsArray[indexI][target].multiplierInitialTime *= improvementsValue;
                break;
        }
        improvementsStatesArray[indexI][indexJ] = true;
        improvementPanelArray[indexI][indexJ].buyButtonСurtain.gameObject.SetActive(true);
        improvementPanelArray[indexI][indexJ].improvementBackgroundImage.color = Color.white;
        improvementPanelArray[indexI][indexJ].closedCardBody.SetActive(false);
    }
    private void _RedrawUpgradeButtons () // Проверить все параметры и перерисовать панельки
    {
        foreach(ProductPanelManager[] prodMans in productPanelsArray)
        {
            foreach(ProductPanelManager prodMan in prodMans)
            {
                if(prodMan != null)
                {
                    prodMan.RedrawUpgradeButton();
                }
                else
                {
                    break;
                }
            }

        }
        foreach(ManagerPanelManager[] managerPanels in managerPanelsArray)
        {
            foreach(ManagerPanelManager managerPanel in managerPanels)
            {
                if(managerPanel != null)
                {
                    managerPanel.RedrawThePanel();
                }
                else
                {
                    break;
                }
            }
        }
        foreach(ImprovementPanelManager[] improveMans in improvementPanelArray)
        {
            foreach(ImprovementPanelManager improveMan in improveMans)
            {
                if(improveMan)
                {
                    improveMan.RedrawThePanel();
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void _LoadManagersState ()
    {
        for(int i = 0; i < managersStatesArray.Length; i++)
        {
            for(int j = 0; j < managersStatesArray[i].Length; j++)
            {
                int state = PlayerPrefs.GetInt($"{managerPanelsArray[i][j].managerSO.managersName}.Manager");
                if(state == 1 && j < managerPanelsArray[i].Length)
                {
                    managerPanelsArray[i][j].managerState = true;
                    if(j < productPanelsArray[i].Length)
                    {
                        productPanelsArray[i][j].manager = true;
                    }
                }
            }
        }
    }
    private void _LoadImprovementsStates ()
    {
        for(int i = 0; i < improvementsStatesArray.Length; i++)
        {
            for(int j = 0; j < improvementsStatesArray[i].Length; j++)
            {
                int state = PlayerPrefs.GetInt($"{improvementPanelArray[i][j].improvementSO.improvementsName}.Improvement");
                if(state == 1 && i < improvementPanelArray.Length)
                {
                    int type = improvementPanelArray[i][j].improvementSO.improvementsType;
                    int targetIndex = improvementPanelArray[i][j].improvementSO.improvementsTargetIndex;

                    ApplyImprovementState(type, targetIndex, i, j);
                }
            }
        }
    }

    public void CheckTheTraining ()
    {
        if(trainingManager.trainingStatus != 0)
        {
            switch(trainingManager.trainingStatus)
            {
                case 3:
                    if(coins >= 1000)
                    {
                        trainingManager.NextStep();
                    }
                    break;
                case 6:
                    if(coins >= 10000)
                    {
                        trainingManager.NextStep();
                    }
                    break;
            }
        }
    }
}
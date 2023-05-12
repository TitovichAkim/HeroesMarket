using UnityEngine;
using UnityEngine.UI;

public class ShopManager:MonoBehaviour
{
    public GameObject roomsPanelGO;
    public GameObject productsPanel;
    public GameObject managersPanel;
    public GameObject improvementsPanel;
    public GameObject factoryesPanel;

    public Text numbersOfCoinsFloatText;
    public Text upgradesNumberText;
    public Image[] ProductIconsOnTheShelf;


    public ProductPanelManager[][] productPanelsArray;
    public ManagerPanelManager[][] managerPanelsArray;
    public ImprovementPanelManager[] improvementPanelArray;

    [SerializeField]private bool[][] _managersStatesArray;
    [SerializeField]private bool[] _improvementsStatesArray;

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
    public bool[] improvementsStatesArray
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
        _managersStatesArray = new bool[managerPanelsArray.Length][];
        for(int i = 0; i < managerPanelsArray.Length; i++)
        {
            _managersStatesArray[i] = new bool[managerPanelsArray[i].Length];
        }
        _improvementsStatesArray = new bool[improvementPanelArray.Length];
        _RedrawUpgradeButtons();
        _LoadManagersState();
        //_LoadImprovementsStates();
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

    public void SaveManagersStates ()
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
    public void SaveImprovementsStates ()
    {
        for(int i = 0; i < improvementsStatesArray.Length; i++)
        {
            int state = 0;
            if(improvementsStatesArray[i])
            {
                state = 1;
            }
            PlayerPrefs.SetInt($"{improvementPanelArray[i].improvementSO.improvementsName}.Improvement", state);
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
    public void ApplyImprovementState (int type, int target, int index)
    {
        float improvementsValue = improvementPanelArray[index].improvementSO.improvementsValue;
        switch(type)
        {
            case 0:
                //productPanelsArray[target].multiplierProductRevenue *= improvementsValue;
                break;
            case 1:
                //productPanelsArray[target].multiplierInitialTime *= improvementsValue;
                break;
        }
        improvementsStatesArray[index] = true;
        improvementPanelArray[index].gameObject.SetActive(false);
    }
    private void _RedrawUpgradeButtons ()
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
        //    foreach(ImprovementPanelManager improveMan in improvementPanelArray)
        //    {
        //        if(improveMan)
        //        {
        //            improveMan.RedrawThePanel();
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
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
            int state = PlayerPrefs.GetInt($"{improvementPanelArray[i].improvementSO.improvementsName}.Improvement");
            if(state == 1 && i < improvementPanelArray.Length)
            {
                int type = improvementPanelArray[i].improvementSO.improvementsType;
                int targetIndex = improvementPanelArray[i].improvementSO.improvementsTargetIndex;

                ApplyImprovementState(type, targetIndex, i);
            }
        }
    }
}
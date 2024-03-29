using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelManager:MonoBehaviour
{

    public ShopManager shopManager;

    public Image managersIconImage;
    public Image buyButton�urtain;
    public Image backgroundImage;
    public TextMeshProUGUI managerNameText;
    public TextMeshProUGUI managerClassText;
    //public Text managersDescriptionText;
    public TextMeshProUGUI managerActionText;
    //public Text ManagerActionTargetText;
    public TextMeshProUGUI managerCostText;
    //public Text buyButtonText;
    public Button buyButton;
    public GameObject buyButtonGO;
    public GameObject closedCardBody;

    public ManagersSO _managerSO;


    private bool _managerState;

    public ManagersSO managerSO
    {
        get
        {
            return (_managerSO);
        }
        set
        {
            _managerSO = value;
            StartPanel();
        }
    }
    public bool managerState
    {
        get
        {
            return (_managerState);
        }
        set
        {
            _managerState = value;
            ActivateManager();
        }
    }

    public void StartPanel ()
    {
        LocalisationTexts();
        managersIconImage.sprite = managerSO.managersIcon;
        managerNameText.text = managerSO.managersPublicName;
        managerClassText.text = managerSO.managersClass;

        NumberFormatter.FormatAndRedraw(managerSO.managersCost, managerCostText);
    }
    public void RedrawThePanel ()
    {
        buyButton.interactable = shopManager.coins >= managerSO.managersCost;
    }

    public void BuyManager ()
    {
        if(!managerState)
        {
            if(shopManager.coins >= managerSO.managersCost)
            {
                shopManager.coins -= managerSO.managersCost;
                managerState = true;
                shopManager.SaveManagersStates();
            }
        }
    }
    public void ActivateManager ()
    {
        if(managerState)
        {
            //this.gameObject.SetActive(false);
            buyButton.enabled = false;
            buyButton�urtain.gameObject.SetActive(true);
            this.gameObject.GetComponent<Image>().color = Color.white;
            _UpdateManagersStateArray(this);
            closedCardBody.SetActive(false);
        }
    }

    private void _UpdateManagersStateArray (ManagerPanelManager panelManager)
    {
        for(int i = 0; i < shopManager.managerPanelsArray.Length; i++)
        {
            for(int j = 0; j < shopManager.managerPanelsArray[i].Length; j++)
            {
                if(shopManager.managerPanelsArray[i][j] == panelManager)
                {
                    shopManager.productPanelsArray[i][j].manager = true;
                    shopManager.managersStatesArray[i][j] = managerState;
                    shopManager.managersStatesArray = shopManager.managersStatesArray;
                }
            }
        }
    }

    private void LocalisationTexts ()
    {
        //Localizator.LocalizedText(buyButtonText, $"General.Buy");
        //Localizator.LocalizedText(managerNameText, $"ManagerName.{managerSO.managersName}");
        //Text[] descriprionTexts = {managersDescriptionText, managerActionText, ManagerActionTargetText};
        //for(int i = 0; i < descriprionTexts.Length; i++)
        //{
        //    Localizator.LocalizedText(descriprionTexts[i], $"ManagerDescription.{managerSO.managersName}", i);
        //}
        //Localizator.LocalizedText(managersDescriptionText, $"ManagerDescription.{managerSO.managersName}");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelManager : MonoBehaviour
{

    public ShopManager shopManager;

    public Image managersIconImage;
    public Text managerNameText;
    public Text managersDescriptionText;
    public Text managerActionText;
    public Text ManagerActionTargetText;
    public Text managerCostText;
    public Text buyButtonText;
    public Button buyButton;

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

        NumberFormatter.FormatAndRedraw(managerSO.managersCost, managerCostText);
    }
    public void RedrawThePanel ()
    {
        buyButton.interactable = shopManager.coins >= managerSO.managersCost;
    }

    public void BuyManager ()
    {
        if (!managerState)
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
        if (managerState)
        {
            this.gameObject.SetActive(false);
            _UpdateManagersStateArray(this);
        }
    }

    private void _UpdateManagersStateArray (ManagerPanelManager panelManager)
    {
    for(int i = 0; i < shopManager.managerPanelsArray.Length; i++)
    {
        if (shopManager.managerPanelsArray[i] == panelManager)
        {
            shopManager.productPanelsArray[i].manager = true;
            shopManager.managersStatesArray[i] = managerState;
            shopManager.managersStatesArray = shopManager.managersStatesArray;
        }
        }
    }

    private void LocalisationTexts ()
    {
        Localizator.LocalizedText(buyButtonText, $"General.Buy");
        Localizator.LocalizedText(managerNameText, $"ManagerName.{managerSO.managersName}");
        Text[] descriprionTexts = {managersDescriptionText, managerActionText, ManagerActionTargetText};
        for(int i = 0; i < descriprionTexts.Length; i++)
        {
            Localizator.LocalizedText(descriprionTexts[i], $"ManagerDescription.{managerSO.managersName}", i);
        }
        //Localizator.LocalizedText(managersDescriptionText, $"ManagerDescription.{managerSO.managersName}");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ImprovementPanelManager: MonoBehaviour
{

    public ShopManager shopManager;

    public Image improvementIconImage;
    public Image improvementBackgroundImage;
    public Text improvementNameText;
    public Text improvementDescriptionText;
    public Text improvementTypeText;
    public Text improvementCostText;
    public Text buyButtonText;
    public Button buyButton;

    public ImprovementSO _improvementSO;

    private bool _improvementState;

    public ImprovementSO improvementSO
    {
        get
        {
            return (_improvementSO);
        }
        set
        {
            _improvementSO = value;
            StartPanel();
        }
    }
    public bool improvementState
    {
        get
        {
            return (_improvementState);
        }
        set
        {
            _improvementState = value;
        }
    }

    public void StartPanel ()
    {
        improvementIconImage.sprite = improvementSO.improvementsIcon;

        Localizator.LocalizedText(buyButtonText, $"General.Buy");
        Localizator.LocalizedText(improvementNameText, $"ImprovementName.{improvementSO.improvementsName}");
        Text[] descriptionTexts = { improvementDescriptionText, improvementTypeText };
        for(int i = 0; i < descriptionTexts.Length; i++)
        {
            Localizator.LocalizedText(descriptionTexts[i], $"ImprovementDescription.{improvementSO.improvementsName}", i);
        }
        NumberFormatter.FormatAndRedraw(improvementSO.improvementsCost, improvementCostText);
    }

    public void RedrawThePanel ()
    {
        buyButton.interactable = shopManager.coins >= improvementSO.improvementsCost;
    }

    public void BuyImprovement ()
    {
        if(!improvementState)
        {
            if(shopManager.coins >= improvementSO.improvementsCost)
            {
                shopManager.coins -= improvementSO.improvementsCost;
                improvementState = true;
                shopManager.SaveImprovementsStates();
                _UpdateImprovementStateArray(this);
            }
        }
    }

    private void _UpdateImprovementStateArray (ImprovementPanelManager panelManager)
    {
        for(int i = 0; i < shopManager.improvementPanelArray.Length; i++)
        {
            if(shopManager.improvementPanelArray[i] == panelManager)
            {
                shopManager.improvementsStatesArray[i] = improvementState;
                shopManager.ApplyImprovementState(improvementSO.improvementsType, improvementSO.improvementsTargetIndex, i);
                shopManager.improvementsStatesArray = shopManager.improvementsStatesArray;
            }
        }
    }
}

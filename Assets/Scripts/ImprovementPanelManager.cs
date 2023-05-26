using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementPanelManager: MonoBehaviour
{

    public ShopManager shopManager;

    public Image improvementIconImage;
    public Image improvementBackgroundImage;
    public Image improvementTypeImage;
    public Image buyButton—urtain;
    public TextMeshProUGUI improvementNameText;
    public TextMeshProUGUI improvementTypeText;
    public TextMeshProUGUI improvementCostText;
    public Button buyButton;
    public GameObject closedCardBody;
    public GameObject buyButtonGO;

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
        //improvementNameText.text = improvementSO.improvementsName;
        //Localizator.LocalizedText(improvementNameText, $"ImprovementName.{improvementSO.improvementsName}");
        //Text[] descriptionTexts = { improvementDescriptionText, improvementTypeText };
        //for(int i = 0; i < descriptionTexts.Length; i++)
        //{
        //    Localizator.LocalizedText(descriptionTexts[i], $"ImprovementDescription.{improvementSO.improvementsName}", i);
        //}
        NumberFormatter.FormatAndRedraw(NumberFormatter.StringToDecimal(improvementSO.improvementsCost), improvementCostText);

    }

    public void RedrawThePanel ()
    {
        buyButton.interactable = shopManager.coins >= NumberFormatter.StringToDecimal(improvementSO.improvementsCost);
    }

    public void BuyImprovement ()
    {
        if(!improvementState)
        {
            if(shopManager.coins >= NumberFormatter.StringToDecimal(improvementSO.improvementsCost))
            {
                shopManager.coins -= NumberFormatter.StringToDecimal(improvementSO.improvementsCost);
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
            for(int j = 0; j < shopManager.improvementPanelArray[i].Length; j++)
            {
                if(shopManager.improvementPanelArray[i][j] == panelManager)
                {
                    shopManager.improvementsStatesArray[i][j] = improvementState;
                    shopManager.ApplyImprovementState(improvementSO.improvementsType, improvementSO.improvementsTargetIndex, i, j);
                    shopManager.improvementsStatesArray = shopManager.improvementsStatesArray;
                }
            }
        }
    }
}

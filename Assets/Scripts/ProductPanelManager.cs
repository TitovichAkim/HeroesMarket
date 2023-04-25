using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProductPanelManager : MonoBehaviour
{

    public ShopManager shopManager;

    public Image productIcon;
    public Image productBackground;
    public Image progressBar;
    public GameObject cardBackground;

    public Button sellProductButton;
    public Button upgradeButton;

    public Text productLevelText;
    public Text coinsPerSecondText;
    public Text buyNumberText;
    public Text costFloatText;
    public Text costStringText;
    public Text timerText;

    public ProductsSO _productSO;

    private int _productInvestments;
    private int _upgradesNumber = 1;

    private float _productRevenue;
    private float _upgradeCost;
    private float _multiplierProductRevenue = 1;
    private float _multiplierInitialTime = 1;

    private float _timerStart;
    private bool _manager;
    private bool _timer;
    private bool _sellProcess;

    public ProductsSO productSO
    {
        get
        {
            return (_productSO);
        }
        set
        {
            _productSO = value;
            StartPanel();
        }
    }
    public int productInvestments
    {
        get
        {
            return (_productInvestments);
        }
        set
        {
            _productInvestments = value;
            PlayerPrefs.SetInt($"{productSO.productName}productInvestments", _productInvestments);
            RedrawThePanel();
        }
    }
    public int upgradesNumber
    {
        get
        {
            return(_upgradesNumber);
        }
        set
        {
            _upgradesNumber = value;
            Localizator.LocalizedText(buyNumberText, $"General.Buy", 0, $"\nx {_upgradesNumber}");
            RedrawThePanel();
        }
    }
    public float productRevenue
    {
        get
        {
            return (_productRevenue);
        }
        set
        {
            _productRevenue = value;
        }
    }
    public float multiplierProductRevenue
    {
        get
        {
            return (_multiplierProductRevenue);
        }
        set
        {
            _multiplierProductRevenue = value;
            RedrawThePanel();
        }
    }
    public float multiplierInitialTime
    {
        get
        {
            return (_multiplierInitialTime);
        }
        set
        {
            _multiplierInitialTime = value;
            RedrawThePanel();
        }
    }
    public bool manager
    {
        get
        {
            return (_manager);
        }
        set
        {
            _manager = value;
            if (manager && !_sellProcess)
            {
                StartSellProduct();
            }
        }
    }

    public void StartPanel ()
    {
        productIcon.sprite = productSO.icon;
        cardBackground.GetComponent<Image>().sprite = productSO.cardsBackground;


        if(PlayerPrefs.GetInt($"{productSO.productName}productInvestments") == 0 && productSO.productName == "TarotCards")
        {
            productInvestments = 1;
        }
        else
        {
            productInvestments = PlayerPrefs.GetInt($"{productSO.productName}productInvestments");
            if(productInvestments == 0)
            {
                productBackground.enabled = false;
                sellProductButton.interactable = false;
            }
        }

        if(manager && productInvestments > 0)
        {
            StartSellProduct();
        }

        RedrawUpgradeButton();
        shopManager.RedrawIconsOnTheShelf();
    }
 
    private void Update ()
    {
        if (_timer)
        {
            float timer = productSO.initialTime / multiplierInitialTime - Mathf.FloorToInt((Time.time - _timerStart) % 60);
            progressBar.fillAmount = (Time.time - _timerStart) / (productSO.initialTime / multiplierInitialTime);
            timerText.text = $"{timer}";
        }
    }
    public void StartSellProduct ()
    {
        if (!_sellProcess)
        {
            StartCoroutine("SellProduct");
        }
    }
    public IEnumerator SellProduct ()
    {
        if (productInvestments > 0)
        {
            do
            {
                _sellProcess = true;
                _timerStart = Time.time;
                _timer = true;
                productBackground.enabled = false;
                sellProductButton.interactable = false;
                yield return new WaitForSeconds(productSO.initialTime / multiplierInitialTime);
                _SellProduct();
                _sellProcess = false;
            } while(manager);
        }
    }

    private void _SellProduct ()
    {
        shopManager.coins += productRevenue;
        _timer = false;
        productBackground.enabled = true;
        sellProductButton.interactable = true;
        timerText.text = $"{productSO.initialTime / multiplierInitialTime}";
    }

    public void ProductLevelUp ()
    {
        if (shopManager.coins >= _upgradeCost)
        {
            if (productInvestments == 0)
            {
                productBackground.enabled = true;
                sellProductButton.interactable = true;

                if(manager)
                {
                    StartSellProduct();
                }
            }
            shopManager.coins -= _upgradeCost;
            productInvestments += _upgradesNumber;
            RedrawThePanel();
            shopManager.RedrawIconsOnTheShelf();
        }
    }

    private void RedrawThePanel ()
    {
        productLevelText.text = productInvestments.ToString();
        timerText.text = $"{productSO.initialTime / multiplierInitialTime}";

        productRevenue = _productInvestments * productSO.initialRevenue * multiplierProductRevenue;


        float totalCost = 0;

        for (int i = 0; i < _upgradesNumber; i++)
        {
            totalCost += productSO.initialCost * Mathf.Pow(productSO.costMultiplier, productInvestments + i);
        }
        _upgradeCost = totalCost;
        NumberFormatter.FormatAndRedraw(_upgradeCost, costFloatText, costStringText);
        NumberFormatter.FormatAndRedraw(productRevenue, coinsPerSecondText);
    }

    public void RedrawUpgradeButton ()
    {
        Localizator.LocalizedText(buyNumberText, $"General.Buy", 0, $"\nx {_upgradesNumber}");
        upgradeButton.interactable = shopManager.coins >= _upgradeCost;
    }
}

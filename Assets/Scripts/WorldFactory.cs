using UnityEngine;
using UnityEngine.UI;

public class WorldFactory:MonoBehaviour
{
    public GameObject shopCanvasPrefab;
    public GameObject productsPanelPrefab;
    public GameObject managersPanelPrefab;
    public GameObject improvementsPanelPrefab;
    public GameObject menuPrefab;

    public ProductsSO[] productsSOArray;
    public ManagersSO[] managersSOArray;
    public ImprovementSO[] improvementSOArray;

    public Sprite[] improvementBackgroundImages;

    private ShopManager _ShopManager;

    public void Awake ()
    {
        GameObject shop = Instantiate(shopCanvasPrefab);

        _ShopManager = shop.GetComponent<ShopManager>();
    }
    public void Start ()
    {
        AssembleShop();
    }

    public async void AssembleShop ()
    {

        InitializeArrays();

        CollectProducts();

        CollectManagers();

        CollectImprovement();

        CollectMenu();

        _ShopManager.StartShop();
    }
    public void InitializeArrays ()
    {
        _ShopManager.productPanelsArray = new ProductPanelManager[productsSOArray.Length];
        _ShopManager.managerPanelsArray = new ManagerPanelManager[managersSOArray.Length];
        _ShopManager.improvementPanelArray = new ImprovementPanelManager[improvementSOArray.Length];
    }
    public void CollectProducts ()
    {
        for(int i = 0; i < productsSOArray.Length; i++)
        {
            GameObject panel = Instantiate(productsPanelPrefab, _ShopManager.productsPanel.transform);
            ProductPanelManager product = panel.GetComponent<ProductPanelManager>();
            product.shopManager = _ShopManager;
            _ShopManager.productPanelsArray[i] = product;

            product.productSO = productsSOArray[i];
        }
    }

    public void CollectManagers ()
    {
        for(int i = 0; i < managersSOArray.Length; i++)
        {
            GameObject panel = Instantiate(managersPanelPrefab, _ShopManager.managersPanel.transform);
            ManagerPanelManager manager = panel.GetComponent<ManagerPanelManager>();
            manager.shopManager = _ShopManager;
            _ShopManager.managerPanelsArray[i] = manager;

            manager.managerSO = managersSOArray[i];
        }
    }

    public void CollectImprovement ()
    {
        for(int i = 0; i < improvementSOArray.Length; i++)
        {
            GameObject panel = Instantiate(improvementsPanelPrefab, _ShopManager.improvementsPanel.transform);
            panel.GetComponent<ImprovementPanelManager>().improvementSO = improvementSOArray[i];
            ImprovementPanelManager improvement = panel.GetComponent<ImprovementPanelManager>();
            improvement.shopManager = _ShopManager;
            _ShopManager.improvementPanelArray[i] = improvement;
            improvement.improvementBackgroundImage.sprite = improvementBackgroundImages[improvementSOArray[i].improvementsType];
            improvement.improvementSO = improvementSOArray[i];
        }
    }

    public void CollectMenu ()
    {
        GameObject menu = Instantiate(menuPrefab);
        menu.GetComponent<MenuManager>().shopManager = _ShopManager;
    }
}

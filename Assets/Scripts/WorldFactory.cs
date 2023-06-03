using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class WorldFactory:MonoBehaviour
{
    public GameObject shopCanvasPrefab;
    public GameObject productPanelsScrollViewPrefab;
    public GameObject openRoomButtonPrefab;
    public GameObject productsPanelPrefab;
    public GameObject managersPanelPrefab;
    public GameObject improvementsPanelPrefab;
    public GameObject indastryesPanelPrefab;
    public GameObject menuPrefab;

    public Sprite[] improvementTypeImagesArray;

    public TrainingManager trainingManager;
    public RoomSetSO[] roomSetSOArray;
    public ProductsSO[] productsSOArray;
    public ManagersSO[] managersSOArray;
    public ImprovementSO[] improvementSOArray;



    public Sprite[] improvementBackgroundImages;

    private ShopManager _ShopManager;
    [SerializeField]private RoomsPanelManager[] _roomsPanelManager = new RoomsPanelManager[4];


    public void Awake ()
    {
        GameObject shop = Instantiate(shopCanvasPrefab);

        _ShopManager = shop.GetComponent<ShopManager>();
        _roomsPanelManager[0] = _ShopManager.roomsPanelGO.GetComponent<RoomsPanelManager>();
        _roomsPanelManager[1] = _ShopManager.managersPanel.GetComponent<RoomsPanelManager>();
        _roomsPanelManager[2] = _ShopManager.improvementsPanel.GetComponent<RoomsPanelManager>();
        _roomsPanelManager[3] = _ShopManager.factoryesPanel.GetComponent<RoomsPanelManager>();

        _ShopManager.trainingManager = trainingManager;
        trainingManager.trainingPanelsGO = _ShopManager.trainingPanelsGO;
        trainingManager.openProductsButtonCanvasGroup = _ShopManager.openProductsButtonGO.GetComponent<CanvasGroup>();
        trainingManager.openManagersButtonCanvasGroup = _ShopManager.openManagersButtonGO.GetComponent<CanvasGroup>();
        trainingManager.managerTrainingPanelCanvasGroup = _ShopManager.managerTrainingPanel.GetComponent<CanvasGroup>();
        trainingManager.openImprovementsButtonCanvasGroup = _ShopManager.openImprovementsButtonGO.GetComponent<CanvasGroup>();
        trainingManager.improvementTrainingPanelCanvasGroup = _ShopManager.improvementTrainingPanel.GetComponent<CanvasGroup>();

        trainingManager.traderDialogBox = _ShopManager.traderDialogBoxGO;
        trainingManager.traderDialogText = _ShopManager.traderDialogText.GetComponent<TextMeshProUGUI>();
        trainingManager.traderDialogBox.SetActive(false);
        trainingManager.shopCanvasGroup = shop.GetComponent<CanvasGroup>();
        trainingManager.traderCanvasGroup = _ShopManager.traderGO.GetComponent<CanvasGroup>();
        trainingManager.traderDialogBoxCanvasGroup = _ShopManager.traderDialogBoxGO.GetComponent<CanvasGroup>();
    }
    public void Start ()
    {
        AssembleShop();
    }

    public async void AssembleShop ()
    {

        await InitializeArrays();       // Инициализировать все массивы

        await CollectProducts();        // Собрать комнаты с продуктами

        //await CollectManagers();        // Загружает менеджеров

        //await CollectImprovement();     // Собрать комнаты с улучшениями

        await CollectMenu();            // Собрать меню

        await _ShopManager.StartShop();

        _ShopManager.RedrawIconsOnTheShelf();

        _StartTraining();
    }
    public async Task InitializeArrays ()
    {
        _ShopManager.productPanelsArray = new ProductPanelManager[roomSetSOArray.Length][];
        _ShopManager.managerPanelsArray = new ManagerPanelManager[roomSetSOArray.Length][];
        _ShopManager.improvementPanelArray = new ImprovementPanelManager[roomSetSOArray.Length][];
        for(int i = 0; i < roomSetSOArray.Length; i++)
        {
            _ShopManager.productPanelsArray[i] = new ProductPanelManager[roomSetSOArray[i].products.Length];
            _ShopManager.managerPanelsArray[i] = new ManagerPanelManager[roomSetSOArray[i].managers.Length];
            _ShopManager.improvementPanelArray[i] = new ImprovementPanelManager[roomSetSOArray[i].improvements.Length];
        }


        foreach(RoomsPanelManager panelManager in _roomsPanelManager)
        {
            panelManager.childPanelsGOArray = new GameObject[roomSetSOArray.Length];
        }
    }
    public async Task CollectProducts ()
    {
        for(int s = 0; s < _roomsPanelManager.Length; s++)
        {
            for(int i = 0; i < roomSetSOArray.Length; i++)
            {
                GameObject productsRoomGO = Instantiate(productPanelsScrollViewPrefab, _roomsPanelManager[s].rightPanelGO.transform);
                _roomsPanelManager[s].childPanelsGOArray[i] = productsRoomGO;
                CreateRoomButton(productsRoomGO, roomSetSOArray[i], _roomsPanelManager[s].leftPanelGO.transform, _roomsPanelManager[s]);
                productsRoomGO.GetComponent<AniManager>().roomsPanelManager = _roomsPanelManager[s];
                Transform roomTransform = productsRoomGO.GetComponent<AniManager>().productPanelsScrollContent.transform;
                int length = 0;
                switch(s)
                {
                    case 0:
                        length = roomSetSOArray[i].products.Length;
                        break;
                    case 1:
                        length = roomSetSOArray[i].managers.Length;
                        break;
                    case 2:
                        length = roomSetSOArray[i].improvements.Length;
                        break;
                    case 3:

                        break;
                }

                for(int j = 0; j < length; j++)
                {
                    switch(s)
                    {
                        case 0:
                            GameObject productPanel = Instantiate(productsPanelPrefab, roomTransform);
                            ProductPanelManager product = productPanel.GetComponent<ProductPanelManager>();
                            product.shopManager = _ShopManager;
                            _ShopManager.productPanelsArray[i][j] = product;
                            product.productSO = roomSetSOArray[i].products[j];

                            if (i == 0 && j == 0)
                            {
                                trainingManager.productBuyButtonCanvasGroup = product.buyButtonGO.GetComponent<CanvasGroup>();
                                trainingManager.productPanelCanvasGroup = productPanel.GetComponent<CanvasGroup>();
                            }
                            break;
                        case 1:
                            GameObject managerPanel = Instantiate(managersPanelPrefab, roomTransform);
                            ManagerPanelManager manager = managerPanel.GetComponent<ManagerPanelManager>();
                            manager.shopManager = _ShopManager;
                            _ShopManager.managerPanelsArray[i][j] = manager;
                            manager.managerSO = roomSetSOArray[i].managers[j];
                            manager.backgroundImage.sprite = manager.managerSO.managersBackIcon;

                            if(i == 0 && j == 0)
                            {
                                trainingManager.managerBuyButtonCanvasGroup = manager.buyButtonGO.GetComponent<CanvasGroup>();
                            }
                            break;
                        case 2:
                            GameObject improvementPanel = Instantiate(improvementsPanelPrefab, roomTransform);
                            ImprovementPanelManager improvement = improvementPanel.GetComponent<ImprovementPanelManager>();
                            improvement.shopManager = _ShopManager;
                            _ShopManager.improvementPanelArray[i][j] = improvement;
                            improvement.improvementSO = roomSetSOArray[i].improvements[j];
                            improvement.improvementTypeImage.sprite = improvementTypeImagesArray[improvement.improvementSO.improvementsType];
                            improvement.improvementBackgroundImage.sprite = improvement.improvementSO.cardBackImage;

                            if(i == 0 && j == 0)
                            {
                                trainingManager.improvementBuyButtonCanvasGroup = improvement.buyButtonGO.GetComponent<CanvasGroup>();
                            }
                            break;
                        case 3:

                            break;
                    }
                }
            }
            _roomsPanelManager[s].SwitchPanel(_roomsPanelManager[s].childPanelsGOArray[0]);
            CreateCostylFlags(_roomsPanelManager[s].leftPanelGO.transform); // Удалить вместе с чистым костылем!!!
        }
    }
    public void CreateRoomButton (GameObject room, RoomSetSO set, Transform parentTransfotm, RoomsPanelManager roomsPM)
    {
        GameObject openRoomButton = Instantiate(openRoomButtonPrefab, parentTransfotm);
        room.GetComponent<AniManager>().openRoomButton = openRoomButton.GetComponent<Button>();
        openRoomButton.GetComponentInChildren<TextMeshProUGUI>().text = set.roomName;
        Button button =  openRoomButton.GetComponent<Button>();
        button.onClick.AddListener(() => roomsPM.moveScrollPanelManager.OpenTargetPanelScroll(room));
        button.image.sprite = set.closedButtonSprite;


        // Назначаем спрайты для каждого состояния кнопки
        SpriteState spriteState = new SpriteState()
        {
            highlightedSprite = set.closedButtonSprite,
            pressedSprite = set.closedButtonSprite,
            selectedSprite = set.closedButtonSprite,
            disabledSprite = set.openButtonSprite
        };

        button.spriteState = spriteState;
    }

    public async Task CollectMenu ()
    {
        GameObject menu = Instantiate(menuPrefab);
        menu.GetComponent<MenuManager>().shopManager = _ShopManager;
    }
    private void _StartTraining ()
    {
        if(PlayerPrefs.HasKey("TrainingStatus"))
        {
            Debug.Log("Существует ключ TrainingStatus");
            trainingManager.trainingStatus = PlayerPrefs.GetInt("TrainingStatus");
        }
        if(trainingManager.trainingStatus == 1)
        {
            Debug.Log("Тренировка равна 1");
            trainingManager.ShowTraining();
        }
    }

    #region ЧИСТОЙ ВОДЫ КОСТЫЛЬ!!! 
    public GameObject flag1Prefab;
    public GameObject flag2Prefab;

    void CreateCostylFlags (Transform parentTransfotm)
    {
        GameObject open1RoomButton = Instantiate(flag1Prefab, parentTransfotm);
        GameObject open2RoomButton = Instantiate(flag2Prefab, parentTransfotm);
    }
    #endregion
}

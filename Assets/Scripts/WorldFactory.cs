using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

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
    }
    public void Start ()
    {
        AssembleShop();
    }

    public async void AssembleShop ()
    {

        await InitializeArrays();       // ���������������� ��� �������

        await CollectProducts();        // ������� ������� � ����������

        //await CollectManagers();        // ��������� ����������

        //await CollectImprovement();     // ������� ������� � �����������

        await CollectMenu();            // ������� ����

        _ShopManager.StartShop();
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
                            break;
                        case 1:
                            GameObject managerPanel = Instantiate(managersPanelPrefab, roomTransform);
                            ManagerPanelManager manager = managerPanel.GetComponent<ManagerPanelManager>();
                            manager.shopManager = _ShopManager;
                            _ShopManager.managerPanelsArray[i][j] = manager;
                            manager.managerSO = roomSetSOArray[i].managers[j];
                            manager.backgroundImage.sprite = manager.managerSO.managersBackIcon;
                            break;
                        case 2:
                            GameObject improvementPanel = Instantiate(improvementsPanelPrefab, roomTransform);
                            ImprovementPanelManager improvement = improvementPanel.GetComponent<ImprovementPanelManager>();
                            improvement.shopManager = _ShopManager;
                            _ShopManager.improvementPanelArray[i][j] = improvement;
                            improvement.improvementSO = roomSetSOArray[i].improvements[j];
                            improvement.improvementTypeImage.sprite = improvementTypeImagesArray[improvement.improvementSO.improvementsType];
                            improvement.improvementBackgroundImage.sprite = improvement.improvementSO.cardBackImage;
                            break;
                        case 3:

                            break;
                    }
                }
            }
            _roomsPanelManager[s].OpenFirstPnel();
        }
    }



    //public async Task CollectManagers ()
    //{
    //    for(int i = 0; i < managersSOArray.Length; i++)
    //    {
    //        GameObject panel = Instantiate(managersPanelPrefab, _ShopManager.managersPanel.transform);
    //        ManagerPanelManager manager = panel.GetComponent<ManagerPanelManager>();
    //        manager.shopManager = _ShopManager;
    //        _ShopManager.managerPanelsArray[i] = manager;

    //        manager.managerSO = managersSOArray[i];
    //    }
    //}

    //public async Task CollectImprovement ()
    //{
    //    for(int i = 0; i < improvementSOArray.Length; i++)
    //    {
    //        GameObject panel = Instantiate(improvementsPanelPrefab, _ShopManager.improvementsPanel.transform);
    //        panel.GetComponent<ImprovementPanelManager>().improvementSO = improvementSOArray[i];
    //        ImprovementPanelManager improvement = panel.GetComponent<ImprovementPanelManager>();
    //        improvement.shopManager = _ShopManager;
    //        _ShopManager.improvementPanelArray[i] = improvement;
    //        improvement.improvementBackgroundImage.sprite = improvementBackgroundImages[improvementSOArray[i].improvementsType];
    //        improvement.improvementSO = improvementSOArray[i];
    //    }
    //}

    public void CreateRoomButton (GameObject room, RoomSetSO set, Transform parentTransfotm, RoomsPanelManager roomsPM)
    {
        GameObject openRoomButton = Instantiate(openRoomButtonPrefab, parentTransfotm);
        room.GetComponent<AniManager>().openRoomButton = openRoomButton.GetComponent<Button>();

        Button button =  openRoomButton.GetComponent<Button>();
        button.onClick.AddListener(() => roomsPM.SwitchPanel(room));
        button.image.sprite = set.closedButtonSprite;


        // ��������� ������� ��� ������� ��������� ������
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
}

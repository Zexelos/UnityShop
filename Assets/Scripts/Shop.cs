using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Button itemButtonPrefab = default;
    [SerializeField] GameObject shopScrollView = default;
    [SerializeField] GameObject shopContent = default;
    [SerializeField] GameObject playerScrollView = default;
    [SerializeField] GameObject playerContent = default;

    Inventory shopInventory;
    Inventory playerInventory;

    void Start()
    {
        shopInventory = GetComponent<Inventory>();
        shopInventory.GetNewRandomItemList();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<Inventory>();
            ShowAllShopWindows();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
            playerScrollView.SetActive(false);
            shopScrollView.SetActive(false);
            ClearAllShopWindows();
        }
    }

    void ShowPlayerInventory()
    {
        playerScrollView.SetActive(true);

        foreach (var item in playerInventory.ItemList)
        {
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[0].text = item.Name;
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[1].text = (item.Price * playerInventory.PriceMultiplier).ToString("c");
            var tempButton = Instantiate(itemButtonPrefab, playerContent.transform);
            tempButton.onClick.RemoveAllListeners();
            tempButton.onClick.AddListener(() => MakeTransaction(item, playerInventory, shopInventory));
            tempButton.onClick.AddListener(ClearAllShopWindows);
            tempButton.onClick.AddListener(ShowShopInventory);
            tempButton.onClick.AddListener(ShowPlayerInventory);
        }
    }

    void ShowShopInventory()
    {
        shopScrollView.SetActive(true);

        foreach (var item in shopInventory.ItemList)
        {
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[0].text = item.Name;
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[1].text = (item.Price * shopInventory.PriceMultiplier).ToString("c");
            var tempButton = Instantiate(itemButtonPrefab, shopContent.transform);
            tempButton.onClick.RemoveAllListeners();
            tempButton.onClick.AddListener(() => MakeTransaction(item, shopInventory, playerInventory));
            tempButton.onClick.AddListener(ClearAllShopWindows);
            tempButton.onClick.AddListener(ShowAllShopWindows);
        }
    }

    public void MakeTransaction(Item item, Inventory seller, Inventory buyer)
    {
        if (buyer.MaxWeight - buyer.CurrentWeight < item.Weight)
        {
            Debug.Log($"{buyer.gameObject.name} does not have enough weight to lift {item.Name}.");
            return;
        }
        else if (buyer.Money < item.Price * seller.PriceMultiplier)
        {
            Debug.Log($"{buyer.gameObject.name} does not have enough money to buy {item.Name}.");
            return;
        }

        buyer.AddItem(item, seller.PriceMultiplier);
        seller.RemoveItem(item, seller.PriceMultiplier);
    }

    void ClearShopWindow()
    {
        foreach (var item in shopContent.GetComponentsInChildren<Button>())
            Destroy(item.gameObject);
    }

    void ClearPlayerWindow()
    {
        foreach (var item in playerContent.GetComponentsInChildren<Button>())
            Destroy(item.gameObject);
    }

    void ShowAllShopWindows()
    {
        ShowShopInventory();

        ShowPlayerInventory();
    }

    void ClearAllShopWindows()
    {
        ClearShopWindow();

        ClearPlayerWindow();
    }
}

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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<Inventory>();
            ShowShopInventory();
            ShowPlayerInventory();
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
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[1].text = item.Price.ToString("c");
            var tempButton = Instantiate(itemButtonPrefab, playerContent.transform);
            tempButton.onClick.RemoveAllListeners();
            tempButton.onClick.AddListener(() => shopInventory.AddItem(item));
            tempButton.onClick.AddListener(ClearShopWindow);
            tempButton.onClick.AddListener(ShowShopInventory);
            tempButton.onClick.AddListener(() => playerInventory.RemoveItem(item));
        }
    }

    void ShowShopInventory()
    {
        shopScrollView.SetActive(true);

        foreach (var item in shopInventory.ItemList)
        {
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[0].text = item.Name;
            itemButtonPrefab.GetComponentsInChildren<TMP_Text>()[1].text = item.Price.ToString("c");
            var tempButton = Instantiate(itemButtonPrefab, shopContent.transform);
            tempButton.onClick.RemoveAllListeners();
            tempButton.onClick.AddListener(() => playerInventory.AddItem(item));
            tempButton.onClick.AddListener(ClearPlayerWindow);
            tempButton.onClick.AddListener(ShowPlayerInventory);
        }
    }

    void ClearAllShopWindows()
    {
        ClearShopWindow();

        ClearPlayerWindow();
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
}

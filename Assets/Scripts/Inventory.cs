using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float money = default;
    [SerializeField] float priceMultiplier = 1f;
    [SerializeField] float maxWeight = default;
    [SerializeField] List<Item> potentialItemList = default;

    public float Money => money;
    public float PriceMultiplier => priceMultiplier;
    public float MaxWeight => maxWeight;
    public float CurrentWeight => currentWeight;
    public List<Item> ItemList => itemList;

    List<Item> itemList;
    float currentWeight;

    void Start()
    {
        GetOriginalItemList();
    }

    void CalculateInventoryWeight()
    {
        float inventoryWeight = 0;

        foreach (var item in itemList)
            inventoryWeight += item.Weight;

        currentWeight = inventoryWeight;
    }

    public void GetOriginalItemList()
    {
        itemList = new List<Item>(potentialItemList);

        CalculateInventoryWeight();
    }

    public void GetNewRandomItemList()
    {
        itemList = new List<Item>(potentialItemList);

        for (int i = itemList.Count - 1; i >= 0; i--)
            if (Random.Range(0f, 1f) <= 0.5f)
                itemList.Remove(itemList[i]);

        CalculateInventoryWeight();
    }

    public void AddItem(Item item, float sellerPriceMultiplier)
    {
        itemList.Add(item);
        money -= item.Price * sellerPriceMultiplier;
        currentWeight += item.Weight;
        Debug.Log($"Added {item.Name} to {this}.");
    }

    public void RemoveItem(Item item, float sellerPriceMultiplier)
    {
        itemList.Remove(item);
        money += item.Price * sellerPriceMultiplier;
        currentWeight -= item.Weight;
        Debug.Log($"Removed {item.Name} from {this}.");
    }
}

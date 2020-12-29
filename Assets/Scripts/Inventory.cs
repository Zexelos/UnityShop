using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float money = default;
    [SerializeField] float priceMultiplier = 1f;
    [SerializeField] float maxWeight = default;
    [SerializeField] List<Item> itemList = default;
    public float Money => money;
    public float PriceMultiplier => priceMultiplier;
    public float MaxWeight => maxWeight;
    public float CurrentWeight => currentWeight;
    public List<Item> ItemList => itemList;

    float currentWeight;

    void Start()
    {
        float inventoryWeight = 0;

        foreach (var item in itemList)
        {
            inventoryWeight += item.Weight;
        }

        currentWeight = inventoryWeight;
    }

    public void AddItem(Item item, float sellerPriceMultiplier)
    {
        itemList.Add(item);
        money -= item.Price * sellerPriceMultiplier;
        currentWeight += item.Weight;
        Debug.Log($"Added {item.Name} to {gameObject.name}s {name}.");
    }

    public void RemoveItem(Item item, float sellerPriceMultiplier)
    {
        itemList.Remove(item);
        money += item.Price * sellerPriceMultiplier;
        currentWeight -= item.Weight;
        Debug.Log($"Removed {item.Name} from {gameObject.name}s {name}.");
    }
}

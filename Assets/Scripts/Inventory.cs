using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float money = default;
    [SerializeField] float maxWeight = default;
    [SerializeField] List<Item> itemList = default;
    public List<Item> ItemList => itemList;
    public float Money => money;

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

    public void AddItem(Item item)
    {
        if (maxWeight - currentWeight < item.Weight || money < item.Price)
        {
            Debug.Log($"Could not add {item.Name} to {gameObject.name}s inventory.");
            return;
        }

        itemList.Add(item);
        money -= item.Price;
        currentWeight += item.Weight;
        Debug.Log($"Added {item.Name} to {gameObject.name}s inventory.");
    }

    public void RemoveItem(Item item)
    {
        //Nie wiem jak sprawdzic czy transakcja przebiegla pomyslnie aby odpalic ta medote and inventory
    }
}

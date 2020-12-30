using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] string iName = default;
    [SerializeField] string description = default;
    [SerializeField] float weight = default;
    [SerializeField] float price = default;
    [Header("Choose float between 0 and 1. 0 = 0%, 1 = 100%")]
    [SerializeField] float rollChance = 1f;

    public string Name => iName;
    public string Description => description;
    public float Weight => weight;
    public float Price => price;
    public float RollChance => rollChance;
}

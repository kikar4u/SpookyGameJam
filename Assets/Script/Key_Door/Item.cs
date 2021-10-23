using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    // correspondance entre clé et porte
    public string name;
    public string description;
    public Sprite image;
}

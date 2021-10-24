using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    // correspondance entre cl� et porte
    public string name;
    public string description;
    public Sprite image;
    public Color color;
}

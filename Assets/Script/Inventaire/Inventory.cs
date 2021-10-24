using UnityEngine;
using System.Collections.Generic;
using System.Data;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private KeyUnitUI keyUnitUIBase;
    private List<KeyUnitUI> content = new List<KeyUnitUI>();
    [SerializeField] private Transform listItems;

    private static Inventory cela;
    public static Inventory instance
    {
        get
        {
            if (!cela) cela = FindObjectOfType<Inventory>();
            return cela;
        }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    public void AddItem(Item item)
    {
        if (Instantiate(keyUnitUIBase.gameObject, listItems.transform).TryGetComponent(out KeyUnitUI unit))
        {
            unit.Init(item);
            content.Add(unit);
        }
    }

    public bool Contain(int idItem, out Item item)
    {
        foreach (KeyUnitUI unit in content)
        {
            if (unit.Item.id == idItem)
            {
                item = unit.Item;
                return true;
            }
        }
        item = null;
        return false;
    }
    
    public bool Contain(int idItem)
    {
        foreach (KeyUnitUI unit in content)
        {
            if (unit.Item.id == idItem) return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        List<KeyUnitUI> newContent = new List<KeyUnitUI>(content);
        foreach (KeyUnitUI unit in content)
        {
            if (unit.Item == item)
            {
                newContent.Remove(unit);
                Destroy(unit.gameObject);
            }
        }
        content = newContent;
    }
}

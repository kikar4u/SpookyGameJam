using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    // normalement ca doit pouvoir ramasser d'autres objets que les cles

    //private Text interactUI;
    public Item item;
    
    [Header("Interface")]
    [SerializeField] private Canvas interactUI;
    [SerializeField] private TextMeshProUGUI textInfo;

 

    protected virtual void Awake()
    {
        interactUI.gameObject.SetActive(false);
        interactUI.worldCamera = Camera.main;
    }

    void Update()
    {
    }

    public void Pick()
    {
        if (!Inventory.instance) throw new NullReferenceException("Y manque un Inventaire dans cette scene");
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            interactUI.gameObject.SetActive(true);
            player.currentItem = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            interactUI.gameObject.SetActive(false);
            player.currentItem = null;
        }
    }
}

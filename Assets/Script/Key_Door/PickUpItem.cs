using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    // normalement ca doit pouvoir ramasser d'autres objets que les cles

    private Text interactUI;
    private bool isInRange;

    public Item item;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            Inventory.instance.content.Add(item);
            Inventory.instance.UpdateInventoryUI();
            interactUI.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}

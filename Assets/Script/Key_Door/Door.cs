using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int doorId;

    public GameObject collision;
    // un gameObject enfant de Door qui contient un box collider (isTrigger=false) pour bloquer le personnage, celui de Door est déjà utilisé pour détecter le joueur

    private Text interactUI;
    private bool isInRange;

    public Animator animator;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && isInRange && Inventory.instance.content[Inventory.instance.contentCurrentIndex].id == doorId)
        {
            animator.SetTrigger("OpenDoor");
            collision.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            interactUI.enabled = false;
            Inventory.instance.content.Remove(Inventory.instance.content[Inventory.instance.contentCurrentIndex]);
            Inventory.instance.UpdateInventoryUI();
        }
    }

    // Entree dans le collider de la porte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    // Sortie du collider de la porte
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}

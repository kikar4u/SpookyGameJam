using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int doorId;

    //public GameObject collision;
    // un gameObject enfant de Door qui contient un box collider (isTrigger=false) pour bloquer le personnage, celui de Door est déjà utilisé pour détecter le joueur
    [Header("Cette valeur override l'axe z de la rotation dans l'éditeur")]
    [SerializeField] private float rotationPorte;
    
    [Header("Interface")]
    [SerializeField] private Canvas interactUI;
    [SerializeField] private TextMeshProUGUI textInfo;
    private bool isInRange;

    [Header("Visual")]
    [SerializeField] private Animator animator;

    private bool isOpen;

    private void OnValidate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotationPorte);
        CounterRotationForUI();
    }

    void Awake()
    {
        interactUI.gameObject.SetActive(false);
    }

    void Update()
    {
        CounterRotationForUI();
    }

    public void Open()
    {
        if(!isOpen && Inventory.instance.content[Inventory.instance.contentCurrentIndex].id == doorId)
        {
            animator.SetTrigger("OpenDoor");
            // collision.GetComponent<BoxCollider2D>().enabled = false;
            // GetComponent<BoxCollider2D>().enabled = false;
            interactUI.gameObject.SetActive(false);
            Inventory.instance.content.Remove(Inventory.instance.content[Inventory.instance.contentCurrentIndex]);
            Inventory.instance.UpdateInventoryUI();
            isOpen = true;
        }
        else
        {
            textInfo.text = "Wrong Key";
        }
    }

    private void CounterRotationForUI()
    {
        if(interactUI) interactUI.transform.localRotation = Quaternion.Euler(0,0,-transform.eulerAngles.z);
    }

    // Entree dans le collider de la porte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isOpen && collision.TryGetComponent(out PlayerController player))
        {
            interactUI.gameObject.SetActive(true);
            textInfo.text = "Press (Space) to open";
            player.currentDoor = this;
        }
    }

    // Sortie du collider de la porte
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isOpen && collision.TryGetComponent(out PlayerController player))
        {
            interactUI.gameObject.SetActive(false);
            player.currentDoor = null;
        }
    }
}

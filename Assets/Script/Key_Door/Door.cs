using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public int doorId;

    //public GameObject collision;
    // un gameObject enfant de Door qui contient un box collider (isTrigger=false) pour bloquer le personnage, celui de Door est déjà utilisé pour détecter le joueur
    [Header("Cette valeur override l'axe z de la rotation dans l'éditeur")]
    [SerializeField] private float rotationDoor;
    [SerializeField] private Transform rotationRoot;
    private List<GameObject> content = new List<GameObject>();
    [Header("Interface")]
    [SerializeField] private Canvas interactUI;
    [SerializeField] private TextMeshProUGUI textInfo;

    [Header("Visual")]
    [SerializeField] private Animator animator;

    private bool isOpen;
    private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");

    private void OnValidate()
    {
        if(rotationRoot)rotationRoot.rotation = Quaternion.Euler(0, 0, rotationDoor);
        CounterRotationForUI();
    }

    void Awake()
    {
        interactUI.gameObject.SetActive(false);
        interactUI.worldCamera = Camera.main;
    }

    void Update()
    {
        CounterRotationForUI();
    }

    public void Open()
    {
        if (!Inventory.instance) throw new NullReferenceException("Y a pas d'inventaire dans cette scene");
        if(!isOpen)
        {
            if (Inventory.instance.Contain(doorId, out Item key))
            {
                animator.SetTrigger(OpenDoor);
                interactUI.gameObject.SetActive(false);
                
                Inventory.instance.RemoveItem(key);
                isOpen = true;
            }
            else
            {
                textInfo.text = "Don't have right key";
            }
        }
    }

    private void CounterRotationForUI()
    {
        if(interactUI && rotationRoot) interactUI.transform.localRotation = Quaternion.Euler(0,0,-rotationRoot.eulerAngles.z);
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
    public void speedMonster(float speed)
    {
        
        Array table = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject item in table)
        {
            item.GetComponent<MonsterBehaviour>().changeSpeed(speed);
        }
    }
    public void backtonormal(float speed)
    {
        Array table = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject item in table)
        {
            item.GetComponent<MonsterBehaviour>().backToNormalSpeed(speed);
        }
    }
}

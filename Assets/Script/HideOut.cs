using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HideOut : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private CircleCollider2D trigger;
    [Header("UI")]
    [SerializeField] private Canvas signInput;
    [SerializeField] private TextMeshProUGUI messageInput;
    private bool isUsed;

    private void OnValidate()
    {
        if (!trigger) TryGetComponent(out trigger);
        if(signInput) signInput.worldCamera = Camera.main;
    }


    // Start is called before the first frame update
    private void Start()
    {
        if (!signInput) throw new NullReferenceException("Y manque une ref à Sign Input ! C pas bien !");
        signInput.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.currentHideout = this;
            messageInput.text = "Press (E) to hide";
            SetSignVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.currentHideout = null;
            SetSignVisible(false);
        }
    }

    private void SetSignVisible(bool visible)
    {
        signInput.gameObject.SetActive(visible);
    }

    public void Use(bool use)
    {
        isUsed = use;
        messageInput.text = use ? "Press (E) to exit" : "Press (E) to hide";
    }
}

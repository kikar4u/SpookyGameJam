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

    [Header("ExitPoints")] 
    [SerializeField] private Transform exitPointUp;
    [SerializeField] private Transform exitPointDown;
    [SerializeField] private Transform exitPointRight;
    [SerializeField] private Transform exitPointLeft;
    public Transform ExitPointUp
    {
        get
        {
            LayerMask obstacleMask = LayerMask.GetMask("Obstacle");
            return Physics2D.OverlapCircle(exitPointUp.position, 0.1f, obstacleMask) ? null : exitPointUp;
        }
    }
    public Transform ExitPointDown
    {
        get
        {
            LayerMask obstacleMask = LayerMask.GetMask("Obstacle");
            return Physics2D.OverlapCircle(exitPointDown.position, 0.1f, obstacleMask) ? null : exitPointDown;
        }
    }
    public Transform ExitPointRight
    {
        get
        {
            LayerMask obstacleMask = LayerMask.GetMask("Obstacle");
            return Physics2D.OverlapCircle(exitPointRight.position, 0.1f, obstacleMask) ? null : exitPointRight;
        }
    }
    public Transform ExitPointLeft
    {
        get
        {
            LayerMask obstacleMask = LayerMask.GetMask("Obstacle");
            return Physics2D.OverlapCircle(exitPointLeft.position, 0.1f, obstacleMask) ? null : exitPointLeft;
        }
    }
    

    private void OnDrawGizmos()
    {
        if(exitPointDown && exitPointUp && exitPointLeft && exitPointRight)
        {
            Gizmos.color = ExitPointUp ? Color.green : Color.red;
            Gizmos.DrawSphere(exitPointUp.position, 0.2f);
            Gizmos.color = ExitPointDown ? Color.green : Color.red;
            Gizmos.DrawSphere(exitPointDown.position, 0.2f);
            Gizmos.color = ExitPointRight ? Color.green : Color.red;
            Gizmos.DrawSphere(exitPointRight.position, 0.2f);
            Gizmos.color = ExitPointLeft ? Color.green : Color.red;
            Gizmos.DrawSphere(exitPointLeft.position, 0.2f);
        }
    }

    private void OnValidate()
    {
        if (!trigger) TryGetComponent(out trigger);
        if(signInput) signInput.worldCamera = Camera.main;
        
    }


    // Start is called before the first frame update
    private void Start()
    {
        if (!signInput) throw new NullReferenceException("Y manque une ref à Sign Input ! C pas bien !");
        if (!exitPointDown || !exitPointUp || !exitPointLeft || !exitPointRight)
            throw new NullReferenceException("Y manque les Exit Points !");
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

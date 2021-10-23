using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(SoundGen))]
public class PlayerController : MonoBehaviour
{

    [Header("movements")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float multRun;
    private bool isHiding;
    public bool IsHiding => isHiding;
    
    [SerializeField] private Rigidbody2D body;
    [Header("Visual")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprRend;
    
    [HideInInspector] public HideOut currentHideout;
    private Vector3 bufferPosition;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    // Start is called before the first frame update
    private void Start()
    {

    }
    private void Update()
    {
        ControleHiding();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!isHiding) ControleMoving();
    }

    private void ControleHiding()
    {
        if (Input.GetButtonDown("Fire1") && currentHideout)
        {
            GetHidden();
        }
    }

    private void ControleMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float _multRun = Input.GetButton("Fire2") ? multRun : 1;
        
        Vector2 newVelocity = new Vector2(horizontal * speed * _multRun, vertical * speed * _multRun);

        animator.SetBool(IsMoving, newVelocity.magnitude > 0);
        animator.SetBool(IsRunning, _multRun > 1);

        body.velocity = newVelocity;
        
    }
    
    private void GetHidden()
    {
        animator.SetBool(IsMoving, false);
        animator.SetBool(IsRunning, false);
        if(!isHiding)
        {
            currentHideout.Use(true);
            
            body.velocity = Vector2.zero;
            bufferPosition = transform.position;
            transform.position = currentHideout.transform.position;
            
            isHiding = true;
        }
        else
        {
            currentHideout.Use(false);
            
            Vector3 exitPoint = new Vector3();
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                if (horizontal > 0 && currentHideout.ExitPointRight)
                {
                    exitPoint = currentHideout.ExitPointRight.position;
                }
                else if(currentHideout.ExitPointLeft)
                {
                    exitPoint = currentHideout.ExitPointLeft.position;
                }

            }
            else
            {
                if (vertical > 0 && currentHideout.ExitPointUp)
                {
                    exitPoint = currentHideout.ExitPointUp.position;
                }
                else if (currentHideout.ExitPointDown)
                {
                    exitPoint = currentHideout.ExitPointDown.position;
                }
            }

            if (exitPoint.magnitude == 0) exitPoint = bufferPosition;
            
            exitPoint.z = transform.position.z;

            transform.position = exitPoint;
            
            isHiding = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;
    private float speedBuffer;
    private float vertical;
    private float horizontal;
    private bool isHiding;
    private bool hidden; //Jamais utilisé pour le moment, peut être supprimé dans l'état
    private int lastPressed;
    [SerializeField] private Rigidbody2D body;
    private GameObject currentHideout;
    private Vector3 bufferPosition;
    
    // Start is called before the first frame update
    private void Start()
    {
        speedBuffer = speed;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isHiding && currentHideout != null)
        {
            Debug.Log("premier if");
            //Debug.Log("veut rentrer");
            GetHidden(true);
            lastPressed++;

        }
        if (Input.GetButtonDown("Fire1") && !isHiding && lastPressed == 1)
        {
            Debug.Log("deuxi�me if");
        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
        if (Input.GetButton("Fire2"))
        { 
            speed = 10.0f;
            //Debug.Log("i am speed" + speed);
        }
        else if(!isHiding)
        {
            speed = speedBuffer;
            //Debug.Log("speed goes back");
        }
    }
    private void GetHidden(bool isNotHidden)
    {

        if(isNotHidden)
        {
            bufferPosition = transform.position;
            speed = 0;
            currentHideout.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log(bufferPosition);
            transform.position = currentHideout.gameObject.transform.position;
            hidden = true;
            isHiding = false;
        }
        else
        {
            Debug.Log(currentHideout);
            speed = speedBuffer;
            transform.position = bufferPosition;
            currentHideout.GetComponent<BoxCollider2D>().isTrigger = false;

        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("hideout"))
        {
            //Debug.Log("HIDE" + col.gameObject.GetComponent<Transform>().position);
            isHiding = true;
            currentHideout = col.gameObject;
        }


    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("hideout"))
        {
            isHiding = false;
        }
    }
}

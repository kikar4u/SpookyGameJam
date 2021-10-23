using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;
    float speedBuffer;
    float vertical;
    float horizontal;
    bool hiding = false;
    bool hidden = false;
    int lastPressed = 0;
    Rigidbody2D body;
    GameObject currentHideout;
    Vector3 bufferPosition;
    // Start is called before the first frame update
    void Start()
    {
        speedBuffer = speed;
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && hiding && currentHideout != null)
        {
            Debug.Log("premier if");
            //Debug.Log("veut rentrer");
            getHidden(true);
            lastPressed++;

        }
        if (Input.GetButtonDown("Fire1") && !hiding && lastPressed == 1)
        {
            Debug.Log("deuxième if");
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
        if (Input.GetButton("Fire2"))
        { 
            speed = 10.0f;
            //Debug.Log("i am speed" + speed);
        }
        else if(!hiding)
        {
            speed = speedBuffer;
            //Debug.Log("speed goes back");
        }







    }
    void getHidden(bool isNotHidden)
    {

        if(isNotHidden){
            bufferPosition = gameObject.GetComponent<Transform>().position;
            speed = 0;
            currentHideout.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log(bufferPosition);
            gameObject.GetComponent<Transform>().position = currentHideout.gameObject.GetComponent<Transform>().position;
            hidden = true;
            hiding = false;
        }
        else
        {
            Debug.Log(currentHideout);
            speed = speedBuffer;
            gameObject.GetComponent<Transform>().position = bufferPosition;
            currentHideout.GetComponent<BoxCollider2D>().isTrigger = false;

        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "hideout")
        {
            //Debug.Log("HIDE" + col.gameObject.GetComponent<Transform>().position);
            hiding = true;
            currentHideout = col.gameObject;
        }


    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "hideout")
        {
            hiding = false;

        }
    }
}

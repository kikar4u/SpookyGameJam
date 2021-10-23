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
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        speedBuffer = speed;
        body = GetComponent<Rigidbody2D>();
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
        else
        {
            speed = speedBuffer;
            //Debug.Log("speed goes back");
        }
    }
    void getHidden()
    {
        hiding = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "hideout")
        {
            Debug.Log("OnCollisionEnter2D");
        }


    }
}

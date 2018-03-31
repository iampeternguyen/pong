using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    // Use this for initialization
    private Rigidbody2D rigidBody;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 10f;
    public float boundary = 4.0f;

    public float response_line = 0f;
    public bool human_player = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DoNoTGoPastBoundary();
        if (GameManager.instance.NumberPlayers == 2)
        {
            PlayerMove();
        }
        else if (GameManager.instance.NumberPlayers == 1 && human_player)
        {
            PlayerMove();
        }
        else
        {
            AIMove();
        }




    }

    public void PlayerMove()
    {
        var velocity = rigidBody.velocity;


        if (Input.GetKey(moveUp))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            velocity.y = -speed;
        }
        else if (!Input.anyKey)
        {
            velocity.y = 0;
        }




        rigidBody.velocity = velocity;


    }

    private void DoNoTGoPastBoundary()
    {

        var position = transform.position;

        if (position.y > boundary)
        {
            position.y = boundary;
        }
        else if (position.y < -boundary)
        {
            position.y = -boundary;
        }
        transform.position = position;
    }

    public void AIMove()
    {
        if (Ball.instance.transform.position.x > response_line)
        {
            var velocity = rigidBody.velocity;

            if (Ball.instance.transform.position.y > transform.position.y && Ball.instance.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                velocity.y = speed;
            }
            else if (Ball.instance.transform.position.y < transform.position.y && Ball.instance.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                velocity.y = -speed;
            }

            rigidBody.velocity = velocity;

        }
    }
}

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

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
}

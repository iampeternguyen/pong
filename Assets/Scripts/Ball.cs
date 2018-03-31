using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance = null;
    public KeyCode start = KeyCode.Return;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;

    public float boundary = 4.3f;

    public float x_speed = 100f;
    public float y_speed = 400f;

    public float maximum_top = 5f;
    public float maximum_bottom = -4.4f;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        DoNoTGoPastBoundary();
        if (Input.GetKey(start) && GameManager.instance.GameStatus == "serve")
        {
            if (GameManager.instance.LastScored == 1)
            {
                BallStartingMovement(1);
            }
            else if (GameManager.instance.LastScored == 2)
            {
                BallStartingMovement(0);
            }
            else
            {
                float left_or_right = Random.Range(0, 2);
                BallStartingMovement(left_or_right);
            }

            GameManager.instance.SetGameStatusToActive();
        }
    }

    void BallStartingMovement(float left_or_right)
    {
        float x_speed = Random.Range(250, 300);
        float y_speed = Random.Range(-100, 100);


        // float y_force = Random.Range(10, 20);

        if (left_or_right < 1)
        {

            rigidBody.AddForce(new Vector2(-x_speed, y_speed));
        }
        else
        {
            rigidBody.AddForce(new Vector2(x_speed, y_speed));
        }
    }

    void OnCollisionEnter2D(Collision2D ball_collision)
    {
        if (ball_collision.collider.CompareTag("Player"))
        {
            velocity.x = ball_collision.relativeVelocity.x * 1.2f;
            velocity.y = ball_collision.relativeVelocity.y * 1.2f;
        }

        if (ball_collision.collider.CompareTag("top_wall"))
        {
            velocity.x = -ball_collision.relativeVelocity.x;
            velocity.y = ball_collision.relativeVelocity.y;
            var position = transform.position;
            position.y = maximum_top;
        }

        if (ball_collision.collider.CompareTag("bottom_wall"))
        {
            velocity.x = -ball_collision.relativeVelocity.x;
            velocity.y = ball_collision.relativeVelocity.y;
            var position = transform.position;
            position.y = maximum_bottom;
        }

        if (ball_collision.collider.CompareTag("left_wall"))
        {
            GameManager.instance.Score(2);
        }

        if (ball_collision.collider.CompareTag("right_wall"))
        {
            GameManager.instance.Score(1);
        }


        rigidBody.velocity = velocity;

    }

    public void ResetBall()
    {
        velocity = new Vector2(0, 0);

        rigidBody.velocity = velocity;
        var position = transform.position;
        position.x = 0;
        position.y = 0;
        transform.position = position;


    }

    private void DoNoTGoPastBoundary()
    {

        var position = transform.position;
        if (position.y > boundary)
        {
            position.y = boundary;
            velocity.y = -rigidBody.velocity.y;
            rigidBody.velocity = velocity;

        }
        else if (position.y < -boundary)
        {
            position.y = -boundary;
            velocity.y = -rigidBody.velocity.y;
            rigidBody.velocity = velocity;

        }
        transform.position = position;
    }
}

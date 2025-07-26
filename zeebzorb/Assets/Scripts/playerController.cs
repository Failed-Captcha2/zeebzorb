using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public bool player1;
    public float speed;
    private Rigidbody2D rb2D;
    private KeyCode Up;
    private KeyCode Down;
    private KeyCode Left;
    private KeyCode Right;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (player1)
        {
            Up = KeyCode.W;
            Down = KeyCode.S;
            Left = KeyCode.A;
            Right = KeyCode.D;
        }
        else
        {
            Up = KeyCode.UpArrow;
            Down = KeyCode.DownArrow;
            Left = KeyCode.LeftArrow;
            Right = KeyCode.RightArrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = Vector3.zero;
        if (Input.GetKey(Up))
        {
            rb2D.velocity = new Vector3(rb2D.velocity.x, speed, 0);
        }
        if (Input.GetKey(Down))
        {
            rb2D.velocity = new Vector3(rb2D.velocity.x, -1*speed, 0);
        }
        if (Input.GetKey(Left))
        {
            rb2D.velocity = new Vector3(-1*speed,rb2D.velocity.y, 0);
        }
        if (Input.GetKey(Right))
        {
            rb2D.velocity = new Vector3(speed,rb2D.velocity.y, 0);
        }

    }

}

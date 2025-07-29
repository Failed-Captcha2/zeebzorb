using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    public bool player1;
    public float speed;
    public GameObject pen;
    public Color penColor;
    public GameObject lineObj;
    private Rigidbody2D penRB;
    private Transform penTransform;
    private SpriteRenderer penSprite;
    Line lineScript;
    private Rigidbody2D playerRB;
    private Vector3 move;
    private KeyCode Up;
    private KeyCode Down;
    private KeyCode Left;
    private KeyCode Right;
    private KeyCode MovePen;
    private KeyCode Draw;
    private bool controlPen;
    private bool drawing;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        if (player1)
        {
            Up = KeyCode.W;
            Down = KeyCode.S;
            Left = KeyCode.A;
            Right = KeyCode.D;
            Draw = KeyCode.E;
            MovePen = KeyCode.LeftShift;
        }
        else
        {
            Up = KeyCode.UpArrow;
            Down = KeyCode.DownArrow;
            Left = KeyCode.LeftArrow;
            Right = KeyCode.RightArrow;
            Draw = KeyCode.Backslash;
            MovePen = KeyCode.RightShift;
        }

        penRB = pen.GetComponent<Rigidbody2D>();
        penTransform = pen.GetComponent<Transform>();
        penSprite = pen.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        playerRB.velocity = Vector3.zero;
        penRB.velocity = Vector3.zero;
        move = Vector3.zero;

        if (Input.GetKey(Up))
        {
            move = new Vector3(move.x, speed, 0);
        }
        if (Input.GetKey(Down))
        {
            move = new Vector3(move.x, -1 * speed, 0);
        }
        if (Input.GetKey(Left))
        {
            move = new Vector3(-1 * speed, move.y, 0);
        }
        if (Input.GetKey(Right))
        {
            move = new Vector3(speed, move.y, 0);
        }

        if (Input.GetKeyDown(Draw) && controlPen && !drawing)
        {
            drawing = true;
            GameObject newLine = Instantiate(lineObj);
            lineScript = newLine.GetComponent<Line>();
            newLine.GetComponent<LineRenderer>().startColor = penSprite.color;
            newLine.GetComponent<LineRenderer>().endColor = penSprite.color;
            
        }
        else if (drawing && (Input.GetKeyDown(Draw)|| !controlPen))
        {
            drawing = false;
            lineScript = null;
        }

        if (Input.GetKeyUp(MovePen))
        {
            if (controlPen)
            {
                controlPen = false;
            }
            else
            {
                controlPen = true;
            }
        }

        if (controlPen)
        {
            penRB.velocity = move;
        }
        else
        {
            playerRB.velocity = move;
        }

        if (lineScript != null)
        {
            lineScript.UpdateLine(penTransform.position);
        }



    }

}

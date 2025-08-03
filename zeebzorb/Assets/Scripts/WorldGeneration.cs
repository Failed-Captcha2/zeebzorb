using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    public int worldSize;
    public float tileSize;

    private int zeebRight;
    private int zeebTop;
    private int zorbRight;
    private int zorbTop;
    private Vector2 zeebPos;
    public Transform zeeb;
    public Transform zeebCamera;
    private Vector2 zorbPos;
    public Transform zorb;
    public Transform zorbCamera;

    private PolygonCollider2D worldCollider;
    // Start is called before the first frame update
    void Start()
    {

        //create world border colliders
        float max = tileSize * (worldSize - 1) + tileSize / 2;
        Vector2[] wallsIn = { new Vector2(-tileSize / 2, -tileSize / 2), new Vector2(-tileSize / 2, max), new Vector2(max, max), new Vector2(max, -tileSize / 2) };
        Vector2[] wallsOut = { new Vector2(-tileSize / 2 - 1, -tileSize / 2 - 1), new Vector2(-tileSize / 2 - 1, max + 1), new Vector2(max + 1, max + 1), new Vector2(max + 1, -tileSize / 2 - 1) };
        worldCollider = GetComponent<PolygonCollider2D>();
        worldCollider.SetPath(0, wallsIn);
        worldCollider.SetPath(1, wallsOut);

        //randomly select player spawn quadrants
        zeebRight = Random.Range(0, 2);
        zorbRight = Random.Range(0, 2);
        //ensures players spawn in different world quadrants
        zeebTop = Random.Range(0, 2);
        if (zeebRight == zorbRight)
        {
            if (zeebTop == 1) { zorbTop = 0; }
            else { zorbTop = 1; }
        }
        else
        {
            zorbTop = Random.Range(0, 2);
        }

        //spawn players in random world position within selected quadrants
        int num = (worldSize - 1) / 2;
        zeebPos = new Vector3(Random.Range(0,num) + zeebRight*num, Random.Range(0, num) + zeebTop * num);
        zorbPos = new Vector3(Random.Range(0, num) + zorbRight * num, Random.Range(0, num) + zorbTop * num);
        zeebPos *= tileSize;
        zorbPos *= tileSize;
        zeeb.position = new Vector3(zeebPos.x, zeebPos.y, -5);
        zorb.position = new Vector3(zorbPos.x, zorbPos.y, -5);
        zeebCamera.position = new Vector3(zeebPos.x, zeebPos.y, -10);
        zorbCamera.position = new Vector3(zorbPos.x, zorbPos.y, -10);



    }

}

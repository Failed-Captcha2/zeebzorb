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
    private Vector2 tempzeeb;
    private Vector2 tempzorb;
    private bool inpath;
    private Vector2 zeebPos;
    public Transform zeeb;
    public Transform zeebCamera;
    private Vector2 zorbPos;
    public Transform zorb;
    public Transform zorbCamera;
    public GameObject[] rooms;
    private GameObject[] tempRoomInstances;
    public GameObject[] roomInstances;
    private int currentRoom;
    public Vector2[] path;
    public Vector2[] temppath;

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
        int num = (worldSize - 1) / 3;
        zeebPos = new Vector3(Random.Range(0, num) + zeebRight * 2 * num, Random.Range(0, num) + zeebTop * 2 * num);
        zorbPos = new Vector3(Random.Range(0, num) + zorbRight * 2 * num, Random.Range(0, num) + zorbTop * 2 * num);
        tempzeeb = zeebPos;
        tempzorb = zorbPos;
        zeebPos *= tileSize;
        zorbPos *= tileSize;
        zeeb.position = new Vector3(zeebPos.x, zeebPos.y, -5);
        zorb.position = new Vector3(zorbPos.x, zorbPos.y, -5);
        zeebCamera.position = new Vector3(zeebPos.x, zeebPos.y, -10);
        zorbCamera.position = new Vector3(zorbPos.x, zorbPos.y, -10);

        //plot path to ensure game is winnable
        AddToPath(tempzeeb);
        while (tempzeeb != tempzorb)
        {
            int tempcoord = Random.Range(0, 2); //choose between x and y direction (0=x, 1=y)
            int tempdirection = Random.Range(0, 2); //choose dirction (0=-1, 1=+1)

            if (tempcoord == 0)
            {
                if (tempdirection == 0 && tempzeeb.x < worldSize - 1) { tempzeeb.x += 1; }
                else if (tempdirection == 1 && tempzeeb.x > 0) { tempzeeb.x -= 1; }
            }

            else
            {
                if (tempdirection == 0 && tempzeeb.y < worldSize - 1) { tempzeeb.y += 1; }
                else if (tempdirection == 1 && tempzeeb.y > 0) { tempzeeb.y -= 1; }
            }

            //remove wall in room in direction exited
            if (tempcoord == 0 && tempdirection == 0) { roomInstances[currentRoom].transform.Find("east wall").gameObject.SetActive(false); }//remove east wall
            else if (tempcoord == 0 && tempdirection == 1) { roomInstances[currentRoom].transform.Find("west wall").gameObject.SetActive(false); } //remove west wall
            else if (tempcoord == 1 && tempdirection == 0) { roomInstances[currentRoom].transform.Find("north wall").gameObject.SetActive(false); }//remove north wall
            else { roomInstances[currentRoom].transform.Find("south wall").gameObject.SetActive(false); }//remove south wall

            //check if new position is already in path array
            inpath = false;
            for (int i = 0; i < path.Length; i++)
            {
                if (tempzeeb == path[i])
                {
                    inpath = true;
                    currentRoom = i;
                }
            }

            //add new position to path array
            if (!inpath)
            {
                AddToPath(tempzeeb);
                currentRoom = path.Length - 1;
            }

            //remove wall of room in direction entered
            if (tempcoord == 0 && tempdirection == 0) { roomInstances[currentRoom].transform.Find("west wall").gameObject.SetActive(false); }//remove west wall
            else if (tempcoord == 0 && tempdirection == 1) { roomInstances[currentRoom].transform.Find("east wall").gameObject.SetActive(false); } //remove east wall
            else if (tempcoord == 1 && tempdirection == 0) { roomInstances[currentRoom].transform.Find("south wall").gameObject.SetActive(false); }//remove south wall
            else { roomInstances[currentRoom].transform.Find("north wall").gameObject.SetActive(false); }//remove north wall

        }




        // //spawn random rooms 
        // for (int i = 0; i < worldSize; i++)
        // {
        //     for (int j = 0; j < worldSize; j++)
        //     {
        //         Vector2 tilePos = new Vector2(i, j);
        //         inpath = false;
        //         for (int k = 0; k < path.Length; k++)
        //         {
        //             if (tilePos == path[k])
        //             {
        //                 inpath = true;
        //             }
        //         }
        //         if (inpath)
        //         {
        //             GameObject newRoom = Instantiate(rooms[0], new Vector3(i * tileSize, j * tileSize, 0), Quaternion.identity);
        //         }
        //         else
        //         {
        //             GameObject newRoom = Instantiate(rooms[Random.Range(1, rooms.Length)], new Vector3(i * tileSize, j * tileSize, 0), Quaternion.identity);
        //         }

        //     }
        //}



    }

    
    //function to add new position to path array
    private void AddToPath(Vector2 position)
    {
        //make temporary copy of path array
        temppath = new Vector2[path.Length];
        tempRoomInstances = new GameObject[roomInstances.Length];
        for (int i = 0; i < path.Length; i++)
        {
            temppath[i] = path[i];
            tempRoomInstances[i] = roomInstances[i];
        }

        //copy temporary array back to larger array
        path = new Vector2[path.Length + 1];
        roomInstances = new GameObject[roomInstances.Length + 1];
        for (int i = 0; i < path.Length - 1; i++)
        {
            path[i] = temppath[i];
            roomInstances[i] = tempRoomInstances[i];
        }

        //add new position to larger array
        path[path.Length - 1] = position;

        GameObject newRoom = Instantiate(rooms[0], new Vector3(position.x * tileSize, position.y * tileSize, 0), Quaternion.identity);
        roomInstances[roomInstances.Length - 1] = newRoom;


        //update currentRoom variable to newest object in array
        currentRoom = path.Length - 1;

    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public float roomSize;
    public int roomAmount;
    public GameObject[] roomPrefabs;
    private GameObject[,] rooms;
    private int roomNum;

    private PolygonCollider2D worldCollider;
    // Start is called before the first frame update
    void Start()
    {
        float max = roomSize * (roomAmount-1) + roomSize / 2;
        Vector2[] wallsIn = { new Vector2(-roomSize/2, -roomSize/2), new Vector2(-roomSize/2, max), new Vector2(max, max), new Vector2(max, -roomSize/2) };
        Vector2[] wallsOut = { new Vector2(-roomSize / 2-1, -roomSize / 2-1), new Vector2(-roomSize / 2-1, max+1), new Vector2(max+1, max+1), new Vector2(max+1, -roomSize / 2-1) };
        worldCollider = GetComponent<PolygonCollider2D>();
        worldCollider.SetPath(0,wallsIn);
        worldCollider.SetPath(1, wallsOut);

        GameObject[,] rooms = new GameObject[roomAmount,roomAmount];
        for (int i = 0; i < roomAmount; i++)
        {
            for (int j = 0; j < roomAmount; j++)
            {
                roomNum = Random.Range(0, roomPrefabs.Length);
                rooms[i,j] = Instantiate(roomPrefabs[roomNum]);
                rooms[i,j].GetComponent<Transform>().position = new Vector3(roomSize * i, roomSize * j, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private GameObject[] mapLayouts;
    private int[,] levelMap;
    private Transform[] mapPieces;
    public Sprite emptySpace;
    public Sprite outsideCorner;
    public Sprite outsideWall;
    public Sprite insideCorner;
    public Sprite insideWall;
    public Sprite pellet;
    public GameObject powerPellet;
    public Sprite tJunction;
    // Start is called before the first frame update
    void Start()
    {
        mapLayouts = GameObject.FindGameObjectsWithTag("MapLayout");
        foreach (GameObject layout in mapLayouts)
        {
            Destroy(layout);
        }
        levelMap = new int[,]   
        { 
            { 1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            { 2,5,5,5,5,5,5,5,5,5,5,5,5,4},   
            { 2,5,3,4,4,3,5,3,4,4,4,3,5,4},   
            { 2,6,4,0,0,4,5,4,0,0,0,4,5,4},     
            { 2,5,3,4,4,3,5,3,4,4,4,3,5,3},        
            { 2,5,5,5,5,5,5,5,5,5,5,5,5,5},     
            { 2,5,3,4,4,3,5,3,3,5,3,4,4,4},     
            { 2,5,3,4,4,3,5,4,4,5,3,4,4,3},     
            { 2,5,5,5,5,5,5,4,4,5,5,5,5,4},     
            { 1,2,2,2,2,1,5,4,3,4,4,3,0,4},       
            { 0,0,0,0,0,2,5,4,3,4,4,3,0,3},       
            { 0,0,0,0,0,2,5,4,4,0,0,0,0,0},       
            { 0,0,0,0,0,2,5,4,4,0,3,4,4,0},      
            { 2,2,2,2,2,1,5,3,3,0,4,0,0,0},      
            { 0,0,0,0,0,0,5,0,0,0,4,0,0,0},    
        };
        //to the right 1.276
        //down 3.72
        mapPieces = GetComponentsInChildren<Transform>();
        for (int i = 0; i< levelMap.GetLength(0); i++)
        {

            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                int mapPiece = levelMap[i,j];
                Transform currentPiece = Instantiate(mapPieces[mapPiece+1], new Vector3(j*1.276f, i*-1.276f, 0f), Quaternion.identity);
                switch (mapPiece+1)
                {
                    case 2:
                        Corner(North(i,j),East(i,j),South(i,j),West(i,j),true);
                    case 3:
                        Wall(North(i, j), East(i, j), South(i, j), West(i, j),true);
                    case 4:
                        Corner(North(i, j), East(i, j), South(i, j), West(i, j),false);
                    case 5:
                        Wall(North(i, j), East(i, j), South(i, j), West(i, j),false);
                }
            }
        }
        //Instantiate(mapPieces[1], new Vector3(0f, 0f, 0f), Quaternion.identity);
        //Instantiate(mapPieces[2], new Vector3(1.276f, 0f, 0f), Quaternion.identity);
    }
    public int North(int i, int j)
    {
        if (i == 0)
        {
            return 10;
        }
        else
        {
            return levelMap[i - 1, j];
        }
    }
    public int East(int i, int j)
    {
        if (j == 13)
        {
            return 10;
        }
        else
        {
            return levelMap[i, j+1];
        }
    }
    public int South(int i, int j)
    {
        if (i == 14)
        {
            return 10;
        }
        else
        {
            return levelMap[i + 1, j];
        }
    }
    public int West(int i, int j)
    {
        if (j == 0)
        {
            return 10;
        }
        else
        {
            return levelMap[i, j-1];
        }
    }
    public void Corner(int north, int east, int south, int west, bool outside)
    {

    }
    public void Wall(int north, int east, int south, int west, bool outside)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

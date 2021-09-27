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
                float rotate = 0;
                switch (mapPiece+1)
                {
                    case 2:
                        rotate = Corner(North(i,j),East(i,j),South(i,j),West(i,j));
                        currentPiece.Rotate(new Vector3(0f,0f,rotate));
                        break;
                    case 3:
                        rotate = Wall(North(i, j), East(i, j), South(i, j), West(i, j));
                        currentPiece.Rotate(new Vector3(0f, 0f, rotate));
                        break;
                    case 4:
                        rotate = Corner(North(i, j), East(i, j), South(i, j), West(i, j));
                        currentPiece.Rotate(new Vector3(0f, 0f, rotate));
                        break;
                    case 5:
                        rotate = Wall(North(i, j), East(i, j), South(i, j), West(i, j));
                        currentPiece.Rotate(new Vector3(0f, 0f, rotate));
                        break;
                    case 8:
                        currentPiece.Rotate(new Vector3(0f, 0f, -90f));
                        break;
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
    private bool[] CompassLocation(bool[] directions, int north, int east, int south, int west)
    {
        if (north == 2 || north == 3 || north == 4 || north == 5)
        {
            directions[0] = true;
        }
        if (east == 2 || east == 3 || east == 4 || east == 5)
        {
            directions[1] = true;
        }
        if (south == 2 || south == 3 || south == 4 || south == 5)
        {
            directions[2] = true;
        }
        if (west == 2 || west == 3 || west == 4 || west == 5)
        {
            directions[3] = true;
        }
        return directions;
    }

    //CORNERS NEED PRESEDENCE
    public float Corner(int north, int east, int south, int west)
    {
        bool[] directions = { false, false, false, false };
        

        directions = CompassLocation(directions, north, east, south, west);

        float rotate = 0f;
        if (directions[0] && directions[1])
        {
            rotate = 90f;
        }else if (directions[0] && directions[3])
        {
            rotate = 180f;
        }        
        else if (directions[2] && directions[3])
        {
            rotate = -90f;
        }
        else if (directions[2] && directions[1])
        {
            rotate = 0f;
        }
        return rotate;
    }
    public float Wall(int north, int east, int south, int west)
    {

        bool[] directions = { false, false, false, false };


        directions = CompassLocation(directions, north, east, south, west);

        float rotate = 0f;
        if (directions[0] && directions[2])
        {
            rotate = 0f;
        }
        else if (directions[1] && directions[3])
        {
            rotate = 90f;
        }
        return rotate;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

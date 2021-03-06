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
        //array of map sprites where mapPieces[0] is the parent empty object
        mapPieces = GetComponentsInChildren<Transform>();

        //loop through every row
        for (int i = 0; i< levelMap.GetLength(0); i++)
        {
            //loop through every column
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                //numerical represenation of map sprite
                int mapPiece = levelMap[i,j];

                //create current map piece in loop where the selected sprite is "mapPiece+1" and location is row*1.276f and column*1.276f
                Transform currentPiece = Instantiate(mapPieces[mapPiece+1], new Vector3(j*1.276f, i*-1.276f, 0f), Quaternion.identity);
                float rotate = 0;

                //determine rotation of map piece, only pieces that need rotating are 2,3,4,5 & 8
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
    }

    //return piece north of current piece
    //if there is no piece north of current piece return 10 (unused number)
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
    //return piece east of current piece
    //if there is no piece east of current piece return 10 (unused number)
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
    //return piece south of current piece
    //if there is no piece south of current piece return 10 (unused number)
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
    //return piece west of current piece
    //if there is no piece west of current piece return 10 (unused number)
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

    //determine rotation of current corner piece based on the pieces sorounding it
    public float Corner(int north, int east, int south, int west)
    {
        float rotate = 0f;

        if (north == 1 || north == 3 || north == 7 || north == 2 || north == 4)
        {
            rotate = 90f;
            if (east == 2 || east == 4 || east == 1 || east == 3 || east == 7)
            {
                rotate = 90f;
                if (west == 4 && south == 4)
                {
                    rotate = 0f;
                }
            }else if (west == 1 || west == 3 || west == 7 || west == 2 || west == 4)
            {
                rotate = 180f;
                if (south == 4){
                    rotate = -90f;
                }
            }
        }else if (south == 2 || south == 4 || south == 1 || south == 3 || south == 7)
        {
            if (east == 2 || east == 4 || east == 1 || east == 3 || east == 7)
            {
                rotate = 0f;
            }
            else if (west == 1 || west == 3 || west == 7 || west == 2 || west == 4)
            {
                rotate = -90f;
            }
        }

        return rotate;
    }

    //determine rotation of current wall piece based on the pieces sorounding it
    public float Wall(int north, int east, int south, int west)
    {


        float rotate = 0f;

        
        if (east == 1 || east == 3 || east == 7 || west == 1 || west == 3 || west == 7)
        {
            rotate = 90f;
            if (north == 4 && south == 4)
            {
                rotate = 0f;
            }
        }else if (north == 1 || north == 3 || north == 7 || south == 1 || south == 3 || south == 7)
        {
            rotate = 0f;
            if (west == 4 && north == 5)
            {
                rotate = 90f;
            }
        }
        else if (north == 2 || north == 4 || south == 2 || south == 4)
        {
            rotate = 0f;
            if (east == 4 && west == 4)
            {
                rotate = 90f;
            }
        }
        else if (east == 2 || east == 4 || west == 2 || west == 4)
        {
            rotate = 90f;
        }
        
        return rotate;
    }
}

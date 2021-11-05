using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CherryController : MonoBehaviour
{

    public GameObject cherry;
    GameObject currentCherry;
    List<GameObject> cherries = new List<GameObject>();
    private Tweener tweener;
    private Vector3 centre = new Vector3(521.5f,374.1f,0f);
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        StartCoroutine(CallCherry());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentCherry != null)
        {
            for (int i = cherries.Count-1; i>-1; i--)
            {
                //if tween of cherry no longer exists, delete cherry
                if (!tweener.TweenExists(cherries[i].transform))
                {
                    DestroyCherry(cherries[i]);
                }
            }
        }
    }

    public void DestroyCherry(GameObject cher)
    {
        Destroy(cherries[cherries.IndexOf(cher)]);
        cherries.Remove(cherries[cherries.IndexOf(cher)]);
    }

    IEnumerator CallCherry()
    {
        //infinitely spawn a cherry every 10 seconds
        while (true)
        {
            yield return new WaitForSeconds(10);
            StartCoroutine(SpawnCherry());
        }
    }


    IEnumerator SpawnCherry()
    {
        //randomly choose 1 side of game for cherry to come from
        rand = UnityEngine.Random.Range(0, 4);

        float y = 0;
        float x = 0;
        //randomly choose where on the y and x axis cherry may spawn
        float randy = UnityEngine.Random.Range(-15.0f, 800.0f);
        float randx = UnityEngine.Random.Range(-12.0f, 1059.0f);

        //instantiate cherry with correct initial x and y values
        //and end x or y values based on random number generation
        switch (rand){
            case 0:
                currentCherry = Instantiate(cherry, new Vector3(-12f, randy, 0f),Quaternion.identity);
                x = 1059f;
                break;
            case 1:
                currentCherry = Instantiate(cherry, new Vector3(1059f, randy, 0f), Quaternion.identity);
                x = -12f;
                break;
            case 2:
                currentCherry = Instantiate(cherry, new Vector3(randx, -15f, 0f), Quaternion.identity);
                y = 800f;
                break;
            case 3:
                currentCherry = Instantiate(cherry, new Vector3(randx, 800f, 0f), Quaternion.identity);
                y = -15f;
                break;
        }

        cherries.Add(currentCherry);
        float m = CalculateSlope();
        
        //calculate final x or y position
        if (rand == 0 || rand == 1)
        {
            y = m * x - m * centre.x + centre.y;
        }else if (rand == 2 || rand == 3)
        {
            x = (y - centre.y + m * centre.x) / m;
        }

        //set end postion and create new Tween
        Vector3 targetPosittion = new Vector3(x, y, 0f);
        AddItem(targetPosittion, GetTime(targetPosittion));
        yield return null;
    }

    //calculate line slope of cherry trajectory from spawnpoint through the centre of the map
    float CalculateSlope()
    {
        return (currentCherry.transform.position.y - centre.y) / (currentCherry.transform.position.x - centre.x);
    }

    //calcualte time for cherry to travel from start pos to end pos so that all cherries move at the same speed
    float GetTime(Vector3 position)
    {
        float speed = 100f;

        float a = position.x - currentCherry.transform.position.x;
        float b = position.y - currentCherry.transform.position.y;
        float ab = a * a + b * b;
        double rawDistance = Math.Sqrt((double)ab);
        float distance =(float)rawDistance;
        return distance / speed;
    }

    private bool AddItem(Vector3 position, float time)
    {
        if (tweener.AddTween(currentCherry.transform, currentCherry.transform.position, position, time))
        {
            return true;
        }
        return false;
    }
}

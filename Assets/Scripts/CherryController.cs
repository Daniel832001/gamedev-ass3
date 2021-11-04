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
                if (!tweener.TweenExists(cherries[i].transform))
                {
                    Destroy(cherries[i]);
                    cherries.Remove(cherries[i]);
                }
            }
        }
    }

    IEnumerator CallCherry()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            StartCoroutine(SpawnCherry());
        }
    }


    IEnumerator SpawnCherry()
    {
        rand = UnityEngine.Random.Range(0, 4);
        float y = 0;
        float x = 0;
        float randy = UnityEngine.Random.Range(-15.0f, 800.0f);
        float randx = UnityEngine.Random.Range(-12.0f, 1059.0f);
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
        
        if (rand == 0 || rand == 1)
        {
            y = m * x - m * centre.x + centre.y;
        }else if (rand == 2 || rand == 3)
        {
            x = (y - centre.y + m * centre.x) / m;
        }
        Vector3 targetPosittion = new Vector3(x, y, 0f);
        AddItem(targetPosittion, GetTime(targetPosittion));
        yield return null;
    }

    float CalculateSlope()
    {
        return (currentCherry.transform.position.y - centre.y) / (currentCherry.transform.position.x - centre.x);
    }

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

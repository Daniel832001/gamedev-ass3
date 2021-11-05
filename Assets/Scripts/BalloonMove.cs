using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BalloonMove : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    private Tweener tweener;
    public Animator pacmanController;
    Boolean first;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        first = true;
        MoveInCircle(new Vector3(65.1f, 38f, 0f));
        //item.transform.position = new Vector3(65.1f, 38f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(item.name +": " +  item.transform.position);
        StartCoroutine(PacmanLoop());
    }

    private IEnumerator PacmanLoop()
    {
        //if (item.transform.localPosition == new Vector3(-489.4f, -310f, 0f))
        //if (item.transform.position == new Vector3(65.1f, 38f, 0f))
        if (item.transform.position == new Vector3(65.1f, 38f, 0f))
        {
            pacmanController.ResetTrigger("Left");
            pacmanController.SetTrigger("Up");
            MoveInCircle(new Vector3(65.1f, 586.1f, 0f));
        }
        //else if(item.transform.localPosition == new Vector3(-489.4f, 238.06f, 0f))
        else if (item.transform.position == new Vector3(65.1f, 586.1f, 0f))
        {
            pacmanController.ResetTrigger("Up");
            pacmanController.SetTrigger("Right");
            MoveInCircle(new Vector3(1044.7f, 586.1f, 0f));
        }
        //else if (item.transform.localPosition == new Vector3(490.2f, 238.06f, 0f))
        else if (item.transform.position == new Vector3(1044.7f, 586.1f, 0f))
        {
            pacmanController.ResetTrigger("Right");
            pacmanController.SetTrigger("Down");
            MoveInCircle(new Vector3(1044.7f, 38f, 0f));
        }
        //else if (item.transform.localPosition == new Vector3(490.2f, 238.06f, 0f))
        else if (item.transform.position == new Vector3(1044.7f, 38f, 0f))
        {
            pacmanController.ResetTrigger("Down");
            pacmanController.SetTrigger("Left");
            MoveInCircle(new Vector3(65.1f, 38f, 0f));
        }
        yield return null;
    }
    private bool MoveInCircle(Vector3 endPos)
    {
        float distance = 0.1f;
        if (endPos.x == item.transform.position.x)
        {
            distance = endPos.y - item.transform.position.y;
        }
        else if (endPos.y == item.transform.position.y)
        {
            distance = endPos.x - item.transform.position.x;
        }
        if (first)
        {
            distance = endPos.x - item.transform.position.x;
            first = false;
        }
        //return AddItem(endPos, 1.0f*Math.Abs(distance)/100);
        return AddItem(endPos, 1.0f);
    }

    private bool AddItem(Vector3 position, float time)
    { 
        if (tweener.AddTween(item.transform, item.transform.position, position, time))
        {
            return true;
        }
        return false;
    }


}

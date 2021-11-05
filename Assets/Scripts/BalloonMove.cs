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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(item.name +": " +  item.transform.position);
        StartCoroutine(PacmanLoop());
    }

    private IEnumerator PacmanLoop()
    {
        //if in bottom left corner move up
        if (item.transform.position == new Vector3(65.1f, 38f, 0f))
        {
            pacmanController.ResetTrigger("Left");
            pacmanController.SetTrigger("Up");
            MoveInCircle(new Vector3(65.1f, 586.1f, 0f));
        }
        //if in top left corner move right
        else if (item.transform.position == new Vector3(65.1f, 586.1f, 0f))
        {
            pacmanController.ResetTrigger("Up");
            pacmanController.SetTrigger("Right");
            MoveInCircle(new Vector3(1044.7f, 586.1f, 0f));
        }
        //if in top right corner move down
        else if (item.transform.position == new Vector3(1044.7f, 586.1f, 0f))
        {
            pacmanController.ResetTrigger("Right");
            pacmanController.SetTrigger("Down");
            MoveInCircle(new Vector3(1044.7f, 38f, 0f));
        }
        //if in bottom right corner move left
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
        //if on same x axis, move y
        if (endPos.x == item.transform.position.x)
        {
            distance = endPos.y - item.transform.position.y;
        }
        //if on same y axis, move x
        else if (endPos.y == item.transform.position.y)
        {
            distance = endPos.x - item.transform.position.x;
        }
        //if its the first time it moves, move up on x axis
        if (first)
        {
            distance = endPos.x - item.transform.position.x;
            first = false;
        }
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

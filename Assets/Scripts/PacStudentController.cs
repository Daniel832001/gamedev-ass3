using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    private string lastInput;
    private bool lerping;
    private string currentInput;
    private Tweener tweener;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = "W";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = "A";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = "S";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = "D";
        }

        lerping = Lerping();

        if (!lerping)
        {
            Move();
        }
        Debug.Log(lastInput);
    }

    bool Lerping()
    {

        return false;
    }

    void Move()
    {
        string attemptedDirection = Direction();
        if (!attemptedDirection.Equals(""))
        {
            currentInput = attemptedDirection;

            Vector3 targetPosittion = GetNewPosition();
            AddItem(targetPosittion, GetTime(targetPosittion));
        }

    }

    string Direction()
    {
        //if (lastInput)
        //{
        //    return lastInput;
        //}else if (currentInput)
        //{
        //    return currentInput;
        //}

        return "";
    }

    Vector3 GetNewPosition()
    {
        //currentInput;
        return new Vector3(0, 0, 0);
    }
    float GetTime(Vector3 distance)
    {
        return 0f;
    }

    private bool AddItem(Vector3 position, float time)
    {
        //if (tweener.AddTween(item.transform, item.transform.position, position, time))
        //{
        //    return true;
        //}
        return false;
    }
}

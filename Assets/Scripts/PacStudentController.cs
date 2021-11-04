using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    private string lastInput;
    private bool lerping;
    private string currentInput;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if (!lerping)
        {
            Move();
        }
    }

    void Move()
    {
        if (Walkable())
        {
            currentInput = lastInput;
        }
    }

    bool Walkable()
    {
        return true;
    }
}

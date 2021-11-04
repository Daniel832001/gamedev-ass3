using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PacStudentController : MonoBehaviour
{

    private string lastInput;
    private bool lerping;
    private string currentInput;
    private Tweener tweener;
    private GameObject pacStudent;
    private int currentRow = 1;
    //currentCol 0 is the parent object
    private int currentCol = 2;
    private List<Row> rows = new List<Row>();
    public AudioSource pacmanMovementSound;

    // Start is called before the first frame update
    void Start()
    {
        //pacmanMovementSound.Play();
        tweener = GetComponent<Tweener>();
        pacStudent = GameObject.FindWithTag("PacStudent");

        for (int i = 0; i<29; i++)
        {
            GameObject currentRow = GameObject.FindWithTag("Row " + i.ToString());
            Row row = new Row(currentRow.transform.GetComponentsInChildren<Transform>());
            rows.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tweener.TweenExists(pacStudent.transform) && !pacmanMovementSound.isPlaying)
        {
            pacmanMovementSound.Play();
        }
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
        //if not lerping start new lerp
        if (!tweener.TweenExists(pacStudent.transform))
        {
            StartCoroutine(Move());
        }
        if (!tweener.TweenExists(pacStudent.transform))
        {
            pacmanMovementSound.Stop();
        }
    }

    private IEnumerator Move()
    {
        string attemptedDirection = Direction();
        //if its possible to move in current direction or last input create new lerp
        if (!attemptedDirection.Equals(""))
        {
            currentInput = attemptedDirection;
            UpdateLocation();

            Vector3 targetPosittion = rows[currentRow].cols[currentCol].position;
            AddItem(targetPosittion, GetTime(targetPosittion));
        }
        yield return null;
    }

    string Direction()
    {
        if (CanMove(lastInput))
        {
            return lastInput;
        }else if (CanMove(currentInput))
        {
            return currentInput;
        }

        return "";
    }

    bool CanMove(string direction)
    {
        switch (direction)
        {
            case "W":
                if (currentRow > 0)
                {
                    if (rows[currentRow - 1].cols[currentCol].name.Contains("layout_5")
                    || rows[currentRow - 1].cols[currentCol].name.Contains("layout_0")
                    || rows[currentRow - 1].cols[currentCol].name.Contains("power pellet"))
                    {
                        return true;
                    }
                }                
                break;
            case "S":
                
                if (currentRow < 28)
                {
                    if (rows[currentRow + 1].cols[currentCol].name.Contains("layout_5")
                    || rows[currentRow + 1].cols[currentCol].name.Contains("layout_0")
                    || rows[currentRow + 1].cols[currentCol].name.Contains("power pellet"))
                    {
                        
                        return true;
                    }
                }
                break;
            case "D":
                if (currentCol < 28)
                {
                    if (rows[currentRow].cols[currentCol + 1].name.Contains("layout_5")
                    || rows[currentRow].cols[currentCol + 1].name.Contains("layout_0")
                    || rows[currentRow].cols[currentCol + 1].name.Contains("power pellet"))
                    {
                        return true;
                    }
                }
                break;
            case "A":
                if (currentCol > 0)
                {
                    if (rows[currentRow].cols[currentCol - 1].name.Contains("layout_5")
                    || rows[currentRow].cols[currentCol - 1].name.Contains("layout_0")
                    || rows[currentRow].cols[currentCol - 1].name.Contains("power pellet"))
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }
    void UpdateLocation()
    {
        switch (currentInput)
        {
            case "W":
                currentRow -= 1;
                break;
            case "S":
                currentRow += 1;
                break;
            case "D":
                currentCol += 1;
                break;
            case "A":
                currentCol -= 1;
                break;
        }
    }


    float GetTime(Vector3 position)
    {
        float speed = 100f;
        float distance = 0f;
        if (currentInput.Equals("W") || currentInput.Equals("S"))
        {
            distance = Math.Abs(position.y - pacStudent.transform.position.y);
        }else if (currentInput.Equals("A") || currentInput.Equals("D"))
        {
            distance = Math.Abs(position.x - pacStudent.transform.position.x);
        }
        return distance / speed;
    }

    private bool AddItem(Vector3 position, float time)
    {
        if (tweener.AddTween(pacStudent.transform, pacStudent.transform.position, position, time))
        {
            return true;
        }
        return false;
    }
}

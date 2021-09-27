using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    private Tweener tweener;
    public Animator pacmanController;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(PacmanLoop());
    }

    private IEnumerator PacmanLoop()
    {
        if (item.transform.position == new Vector3(1.28f, -6.45f, -1.0f))
        {
            
            MoveInCircle(new Vector3(1.28f, -1.32f, -1.0f));
            pacmanController.SetTrigger("Up");
        }
        else if(item.transform.position == new Vector3(1.28f, -1.32f, -1.0f))
        {
            
            MoveInCircle(new Vector3(7.65f, -1.32f, -1.0f));
            pacmanController.SetTrigger("Right");
        }
        else if (item.transform.position == new Vector3(7.65f, -1.32f, -1.0f))
        {
            
            MoveInCircle(new Vector3(7.65f, -6.45f, -1.0f));
            pacmanController.SetTrigger("Down");
        }
        else if (item.transform.position == new Vector3(7.65f, -6.45f, -1.0f))
        {
            
            MoveInCircle(new Vector3(1.28f, -6.45f, -1.0f));
            pacmanController.SetTrigger("Left");
        }
        yield return null;
    }
    private bool MoveInCircle(Vector3 endPos)
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider entity)
    {
        //Debug.Log("Collision Enter: " + entity.gameObject.name + " : " + entity.contacts[0]);
        if (entity.gameObject.name.Contains("layout_1") || entity.gameObject.name.Contains("layout_2") || entity.gameObject.name.Contains("layout_7")|| entity.gameObject.name.Contains("layout_3") || entity.gameObject.name.Contains("layout_4"))
        {
            Wall(entity);
        }
        else if (entity.gameObject.name.Equals("map layout_0 (34)"))
        {
            Portal(entity);
        }
        else if (entity.gameObject.name.Contains("layout_5"))
        {
            Pellet(entity);
        }
        else if (entity.gameObject.name.Contains("cherry"))
        {
            Cherry(entity);
        }
        else if (entity.gameObject.name.Contains("power pellet"))
        {
            PowerPellet(entity);
        }
        else if (entity.gameObject.name.Contains("ghost"))
        {
            Ghost(entity);
        }

    }

    void Wall(Collider wall)
    {
        Debug.Log("you hit: " + wall.gameObject.name);
    }

    void Pellet(Collider pellet)
    {
        Debug.Log("you hit: " + pellet.gameObject.name);
    }

    void Cherry(Collider cherry)
    {
        Debug.Log("you hit: " + cherry.gameObject.name);
    }

    void PowerPellet(Collider powerPellet)
    {
        Debug.Log("you hit: " + powerPellet.gameObject.name);
    }

    void Ghost(Collider ghost)
    {
        Debug.Log("you hit: " + ghost.gameObject.name);
    }

    void Portal(Collider portal)
    {
        Debug.Log("you hit: " + portal.gameObject.name);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacStudentCollision : MonoBehaviour
{
    public ParticleSystem collisionParticle;
    private ParticleSystem currentParticle;
    List<ParticleSystem> collisions = new List<ParticleSystem>();
    public AudioSource collisionSound;
    public GameObject pacStu;
    public Tweener tweener;
    public Transform leftPortal;
    public Transform rightPortal;
    public GameObject emptyTile;
    public Text points;
    private int score;
    public CherryController cherryController;

    // Start is called before the first frame update
    void Start()
    {
        score = int.Parse(points.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentParticle != null)
        {
            for (int i = collisions.Count - 1; i > -1; i--)
            {
                if (collisions[i].isStopped)
                {
                    Destroy(collisions[i]);
                    collisions.Remove(collisions[i]);
                }
            }
        }
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
        //Debug.Log("you hit: " + wall.gameObject.name);
        currentParticle = Instantiate(collisionParticle, wall.gameObject.transform.position, Quaternion.identity);
        collisions.Add(currentParticle);
        currentParticle.Play();
        collisionSound.Play();
        StartCoroutine(StopParticle(collisions.IndexOf(currentParticle)));
    }

    IEnumerator StopParticle(int index)
    {
        yield return new WaitForSeconds(1);
        collisions[index].Stop();
        Destroy(collisions[index]);
    }

    void Portal(Collider portal)
    {
        Debug.Log("you hit: " + portal.gameObject.name);
        if (portal.gameObject.transform.position == leftPortal.position)
        {
            Debug.Log("left portal activated: " + pacStu.transform.position);
            //pacStu.transform.position = rightPortal.position;
            tweener.AddTween(pacStu.transform, pacStu.transform.position, rightPortal.position, 0f);
            Debug.Log("teleportation complete: " + pacStu.transform.position);
        }
        else if (portal.gameObject.transform.position == rightPortal.position)
        {
            //pacStu.transform.position = leftPortal.position;
            tweener.AddTween(pacStu.transform, pacStu.transform.position, leftPortal.position, 0f);
        }
    }

    void Pellet(Collider pellet)
    {
        //Debug.Log("you hit: " + pellet.gameObject.name);
        StartCoroutine(DestroyPellet(pellet));
    }

    IEnumerator DestroyPellet(Collider pellet)
    {
        yield return new WaitForSeconds(0.1f);
        pellet.gameObject.name = "map layout_0";
        pellet.gameObject.GetComponent<SpriteRenderer>().sprite = emptyTile.GetComponent<SpriteRenderer>().sprite;
        score += 10;
        points.text = score.ToString();
    }

    void Cherry(Collider cherry)
    {
        Debug.Log("you hit: " + cherry.gameObject.name);
        cherryController.DestroyCherry(cherry.gameObject);
        score += 100;
        points.text = score.ToString();
    }

    void PowerPellet(Collider powerPellet)
    {
        Debug.Log("you hit: " + powerPellet.gameObject.name);
    }

    void Ghost(Collider ghost)
    {
        Debug.Log("you hit: " + ghost.gameObject.name);
    }

}

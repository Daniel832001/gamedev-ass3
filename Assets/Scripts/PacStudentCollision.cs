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
    public AudioSource scaredSound;
    public GameObject pacStu;
    public Tweener tweener;
    public Transform leftPortal;
    public Transform rightPortal;
    public GameObject emptyTile;
    public Text points;
    private int score;
    public CherryController cherryController;
    public bool startTimer = false;
    public introMusic music;
    public Text timer;
    private float time;
    public Text ghostTimer;
    public Animator greenGhost;
    public Animator yellowGhost;
    public Animator orangeGhost;
    public Animator pinkGhost;
    private bool startGame = true;
    private float gameTime;
    public Text clock;
    public AudioSource deathSound;
    public Text life;
    private Image[] lives = new Image[3];
    public PacStudentController pacStuController;

    // Start is called before the first frame update
    void Start()
    {
        score = int.Parse(points.text);
        lives = life.GetComponentsInChildren<Image>();
        ghostTimer.enabled = false;
        timer.enabled = false;
        greenGhost.SetTrigger("Alive");
        yellowGhost.SetTrigger("Alive");
        orangeGhost.SetTrigger("Alive");
        pinkGhost.SetTrigger("Alive");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentParticle != null)
        {
            for (int i = collisions.Count - 1; i > 0; i--)
            {
                if (collisions[i] != null)
                {
                    if (collisions[i].isStopped)
                    {
                        Destroy(collisions[i]);
                        collisions.Remove(collisions[i]);
                    }
                }
                
            }
        }

        if (startGame)
        {   
            gameTime += Time.deltaTime;

            ChangeTimeMinute(gameTime);
            
        }

        if (startTimer)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
            }

            ChangeTime(time);

        }


    }
    void ChangeTimeMinute(float currentTime)
    {
        float minute = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float miliseconds = currentTime % 1 * 1000;
        clock.text = string.Format("{0:00}:{1:00}:{2:00}", minute,seconds, miliseconds);
    }
    void ChangeTime(float currentTime)
    {
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float miliseconds = currentTime % 1 * 1000;
        timer.text = string.Format("{0:00}:{1:000}", seconds, miliseconds);
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
        StartCoroutine(StopParticle(currentParticle));
    }

    IEnumerator StopParticle(ParticleSystem index)
    {
        yield return new WaitForSeconds(1);
        index.Stop();
        Destroy(index);
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
        //Debug.Log("you hit: " + cherry.gameObject.name);
        cherryController.DestroyCherry(cherry.gameObject);
        score += 100;
        points.text = score.ToString();
    }

    void PowerPellet(Collider powerPellet)
    {
        //Debug.Log("you hit: " + powerPellet.gameObject.name);
        powerPellet.gameObject.name = "map layout_0";
        powerPellet.gameObject.GetComponent<Animator>().enabled = false;
        powerPellet.gameObject.GetComponent<SpriteRenderer>().sprite = emptyTile.GetComponent<SpriteRenderer>().sprite;
        scaredSound.Play();
        music.PowerPelletStart();
        startTimer = true;
        time = 10;
        ghostTimer.enabled = true;
        timer.enabled = true;
        Scared(greenGhost);
        Scared(yellowGhost);
        Scared(orangeGhost);
        Scared(pinkGhost);
        StartCoroutine(CountDown());
    }

    void Scared(Animator ghost)
    {
        ghost.SetTrigger("Scared");
        ghost.ResetTrigger("Alive");
        ghost.ResetTrigger("Recover");
    }

    void Recover(Animator ghost)
    {
        ghost.SetTrigger("Recover");
        ghost.ResetTrigger("Alive");
        ghost.ResetTrigger("Scared");
    }
    void Alive(Animator ghost)
    {
        if (!ghost.GetBool("Dead"))
        {
            ghost.SetTrigger("Alive");
            ghost.ResetTrigger("Recover");
            ghost.ResetTrigger("Scared");
        }
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(7);
        Recover(greenGhost);
        Recover(yellowGhost);
        Recover(orangeGhost);
        Recover(pinkGhost);
        yield return new WaitForSeconds(3);
        Alive(greenGhost);
        Alive(yellowGhost);
        Alive(orangeGhost);
        Alive(pinkGhost);
        scaredSound.Stop();
        if (!greenGhost.GetBool("Dead") && !yellowGhost.GetBool("Dead") && !orangeGhost.GetBool("Dead") && !pinkGhost.GetBool("Dead"))
        {
            music.PowerPelletFinish();
        }            
        ghostTimer.enabled = false;
        timer.enabled = false;

    }

    void Ghost(Collider ghost)
    {
        //Debug.Log("you hit: " + ghost.gameObject.name);

        if (ghost.gameObject.GetComponent<Animator>().GetBool("Scared") || ghost.gameObject.GetComponent<Animator>().GetBool("Recover"))
        {
            scaredSound.Pause();
            if (!deathSound.isPlaying)
            {
                deathSound.Play();
            }
            score += 300;
            points.text = score.ToString();
            ghost.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            ghost.gameObject.GetComponent<Animator>().ResetTrigger("Recover");
            ghost.gameObject.GetComponent<Animator>().ResetTrigger("Scared");
            ghost.gameObject.GetComponent<Animator>().ResetTrigger("Alive");
            StartCoroutine(Resurection(ghost.gameObject.GetComponent<Animator>()));
        }
        else if (ghost.gameObject.GetComponent<Animator>().GetBool("Alive")){
            for (int i = lives.Length-1; i>-1; i--)
            {
                if (lives[i] != null)
                {
                    Destroy(lives[i]);
                    break;
                }
            }

            tweener.AddTween(pacStu.transform, pacStu.transform.position, pacStuController.GetTilePos(1, 1), 0f);
        }


    }

    IEnumerator Resurection(Animator ghost)
    {
        yield return new WaitForSeconds(5);
        ghost.SetTrigger("Alive");
        ghost.ResetTrigger("Recover");
        ghost.ResetTrigger("Scared");
        ghost.ResetTrigger("Dead");
        CheckAlive();
    }
    void CheckAlive()
    {
        if (!greenGhost.GetBool("Dead") && !yellowGhost.GetBool("Dead") && !orangeGhost.GetBool("Dead") && !pinkGhost.GetBool("Dead"))
        {
            deathSound.Stop();
            if (greenGhost.GetBool("Scared") || yellowGhost.GetBool("Scared") || orangeGhost.GetBool("Scared") || pinkGhost.GetBool("Scared"))
            {
                scaredSound.Play();
            }
            else
            {
                music.PowerPelletFinish();
            }
            
            
        }
    }
}

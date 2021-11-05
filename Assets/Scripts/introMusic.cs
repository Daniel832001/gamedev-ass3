using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introMusic : MonoBehaviour   
{

    public AudioSource gameMusic;
    AudioSource introSound;
    private bool game = false;
    // Start is called before the first frame update
    void Start()
    {
        introSound = GetComponent<AudioSource>();
        introSound.Play();
        StartCoroutine(PlayMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayMusic()
    {
        //yield return new WaitForSecondsRealtime(4.5f);        
        //gameMusic.Play();

        yield return new WaitUntil(() => introSound.isPlaying == false);
        gameMusic.Play();
        game = true;
    }
    public void PowerPelletStart()
    {
        introSound.Pause();
        gameMusic.Pause();
    }
    public void PowerPelletFinish()
    {
        if (game)
        {
            gameMusic.Play();
        }
        else
        {
            introSound.Play();
        }
    }
}

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

    IEnumerator PlayMusic()
    {

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

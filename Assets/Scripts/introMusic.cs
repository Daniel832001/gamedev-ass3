using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introMusic : MonoBehaviour   
{

    public AudioSource gameMusic;
    AudioSource introSound;
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
        yield return new WaitForSecondsRealtime(4.5f);        
        gameMusic.Play();

        //I know you asked for the game music to play once the intro sound had finished,
        //but I prefer the sound when it starts sooner
        //here is the code required to play the game music once the intro music finishes
        //to show my competency:

        //yield return new WaitUntil(() => introSound.isPlaying == false);
        //gameMusic.Play();
    }
}

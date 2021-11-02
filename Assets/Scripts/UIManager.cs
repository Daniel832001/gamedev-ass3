using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    
    private RectTransform[] rectTransforms = new RectTransform[3];
    private Tweener tweener;

    // Start is called before the first frame update
    void Start()
    {
        //rectTransforms = gameObject.GetComponentsInChildren<RectTransform>();
        //rectTransforms[1].sizeDelta = new Vector2(1500f,1500f);
        //StartCoroutine(HideLoadingScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowLoadingScreen()
    {
        yield return Tween(rectTransforms[1], new Vector2(0f, -1000f), new Vector2(0.0f, 0.0f), 1.0f);
    }

    IEnumerator HideLoadingScreen()
    {
        yield return new WaitForSeconds(1);
        yield return Tween(rectTransforms[1], new Vector2(0f, 0f), new Vector2(0f, -1000f), 1.0f);
    }

    IEnumerator Tween(RectTransform target, Vector2 startPos, Vector2 endPos, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            target.anchoredPosition = Vector2.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        target.anchoredPosition = endPos;
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadSceneAsync(1);

    }
    public void LoadFirstLevel1()
    {
        DontDestroyOnLoad(this);
        //SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(loadLevel());

    }
    IEnumerator loadLevel()
    {

        yield return ShowLoadingScreen();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    //public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.buildIndex == 1)
    //    {
    //        Button quitButton = GameObject.FindWithTag("QuitButton").GetComponent<Button>();
    //        healthImage = GameObject.FindWithTag("PlayerHealthBar").GetComponent<Image>();
    //        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    //        parentImage = healthImage.GetComponentsInParent<Image>();
    //        quitButton.onClick.AddListener(QuitGame);

    //        StartCoroutine(HideLoadingScreen());
    //    }
    //}
}

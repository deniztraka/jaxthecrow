using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject DeadPanel;
    public GameObject MenuPanel;
    public Crow Crow;
    // Start is called before the first frame update

    void Awake(){
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        Crow.OnDead.AddListener(OnDead);
    }

    void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    private void OnDead()
    {
        DeadPanel.SetActive(true);
    }

    public void Restart()
    {
        StartCoroutine(LoadAsyncScene("GameScene"));
    }

    public void Pause()
    {
        MenuPanel.SetActive(true);
        isPaused = true;
    }

    public void Resume(){
        isPaused = false;
        MenuPanel.SetActive(false);
    }

    public void GoToMainMenu(){
         StartCoroutine(LoadAsyncScene("MenuScene"));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

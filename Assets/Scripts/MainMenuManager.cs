using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame(){
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        //Debug.Log("hede");
    }

    public void Quit(){
        Application.Quit();
    }
}
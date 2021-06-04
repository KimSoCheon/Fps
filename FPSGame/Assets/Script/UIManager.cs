using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameExit()
    {
        Application.Quit();
        Debug.Log("게임나감:::");
    }
}

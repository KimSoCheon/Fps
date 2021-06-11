using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerBird : MonoBehaviour
{
    public Toggle pauseBtn;
    public GameObject popUpPanel;
    void Start()
    {
        pauseBtn.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUpPanelOnOff()
    {
        if (pauseBtn.isOn == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        popUpPanel.SetActive(pauseBtn.isOn);
    }
}

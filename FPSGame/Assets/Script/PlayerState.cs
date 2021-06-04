using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public int hp = 5;
    public Text hpText;
    public bool isDead = false;
    public GameObject gameOverText;
    public Slider hpBar;
    public CameraShake cameraShake;
    public AudioClip bitSound;
    public Text scoreText;

    public Slider bgmSlider;
    public AudioSource bgm;

    public GameObject popUpPanel;
    public Toggle pauseBtn;

    public GameObject gameOverPanel;
    void Start()
    {
        pauseBtn.isOn = false;
        cameraShake = GetComponentInChildren<CameraShake>();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isDead == false)
        {
            hpText.text = "HP :" +hp;
            hpBar.value = (float)hp / 5;
        }
        else
        {
            hpText.text = "YOU DIED";
            hpBar.value = 0;
            gameOverText.SetActive(true);
        }
        int myScore = ScoreManager.InsTance().myScore;
        int bestScore = ScoreManager.InsTance().bestScore;

        scoreText.text = "MY SCORE :" + myScore + "\n" + "BEST SOCRE :" + bestScore;

        bgm.volume = bgmSlider.value;
        if(hp <= 0)
        {
            gameOverPanel.SetActive(true);
            if(gameOverPanel == true)
            {
                
            }
        }
    }

    public void DamageByEnemy()
    {
        if (isDead)
            return;
        
        --hp;
        AudioManager.Instance().PlaySfx(bitSound);

        cameraShake.PlayCameraShake();
        if (hp <= 0)
        {
            isDead = true;
        }
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

    public void GameRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameLobby()
    {
        SceneManager.LoadScene(0);
    }
}

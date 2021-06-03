using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {
        cameraShake = GetComponentInChildren<CameraShake>();
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
    }

    public void DamageByEnemy()
    {
        if (isDead)
            return;
        
        --hp;
        //AudioManager.Instance().PlaySfx(bitSound);

        cameraShake.PlayCameraShake();
        if (hp <= 0)
        {
            isDead = true;
        }
    }
}

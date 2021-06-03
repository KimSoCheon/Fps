
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance = null;
    public static AudioManager Instance()
    {
        return _instance;
    }
    public AudioClip bgm;
    void Start()
    {
        if (_instance == null)
            _instance = this;

        GetComponent<AudioSource>().clip = bgm;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }


    public void PlaySfx(AudioClip sfx)
    {
        GetComponent<AudioSource>().PlayOneShot(sfx);
    }
    
    void Update()
    {
        
    }
}

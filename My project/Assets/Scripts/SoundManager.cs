using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public AudioSource aud;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlayClip(AudioClip cl)
    {
        aud.PlayOneShot(cl);
    }
    public void PlayBGM(AudioClip cl)
    {
        BGM.clip = cl;
        BGM.Play();
    }
}

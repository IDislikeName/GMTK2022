using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SoundManager");
                go.AddComponent<SoundManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
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

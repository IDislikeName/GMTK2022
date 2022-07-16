using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region SingletonDeclaration 
    public static SoundManager instance;
    public static SoundManager FindInstance()
    {
        return instance; //that's just a singletone as the region says
    }

    void Awake() //this happens before the game even starts and it's a part of the singletone
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            //DontDestroyOnLoad(this);
            instance = this;
        }
    }
    #endregion
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

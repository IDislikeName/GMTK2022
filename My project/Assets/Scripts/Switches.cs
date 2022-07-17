using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour
{
    public GameObject door;
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activated = CheckActivation();
        if (door.activeInHierarchy)
        {
            if (activated)
            {
                door.SetActive(false);
                SoundManager.instance.PlayClip(SoundManager.instance.togglespikes);
            }
                
        }
        else
        {
            if (!activated)
            {
                door.SetActive(true);
                SoundManager.instance.PlayClip(SoundManager.instance.togglespikes);
            }
                
        }
    }
    public bool CheckActivation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Switch>().on)
            {
                return false;
            }
        }
        return true;
    }
}

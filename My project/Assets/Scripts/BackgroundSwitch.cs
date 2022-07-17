using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    public GameObject Background1;
    public GameObject Background2;
    public GameObject Background3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackgroundSwitchTo(int Target)
    {
        switch (Target)
        {
            case 1:
                Background1.SetActive(true);
                Background2.SetActive(false);
                Background3.SetActive(false);
                break;
            case 2:
                Background1.SetActive(false);
                Background2.SetActive(true);
                Background3.SetActive(false);
                break;
            case 3:
                Background1.SetActive(false);
                Background2.SetActive(false);
                Background3.SetActive(true);
                break;
        }
    }
}

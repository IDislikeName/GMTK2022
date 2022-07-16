using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch_Visual : MonoBehaviour
{
    public GameObject cover1;
    public GameObject cover2;
    public GameObject cover3;

    int playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerStatus = GameManager.instance.inWorldNumber;
        if (playerStatus == 1)
        {
            cover1.SetActive(false);
            cover2.SetActive(true);
            cover3.SetActive(true);
        }
        else if (playerStatus == 2)
        {
            cover1.SetActive(true);
            cover2.SetActive(false);
            cover3.SetActive(true);
        }
        else
        {
            cover1.SetActive(true);
            cover2.SetActive(true);
            cover3.SetActive(false);
        }
    }
}

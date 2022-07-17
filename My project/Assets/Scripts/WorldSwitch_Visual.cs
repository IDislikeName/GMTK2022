using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch_Visual : MonoBehaviour
{
    public GameObject cover1;
    public GameObject cover2;
    public GameObject cover3;

    public GameObject deathUI;
    public GameObject Buttons;

    int playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        deathUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.instance.inWorldNumber)
        {
            case 1:
                cover1.SetActive(true);
                cover2.SetActive(false);
                cover3.SetActive(false);
                break;
            case 2:
                cover1.SetActive(false);
                cover2.SetActive(true);
                cover3.SetActive(false);
                break;
            case 3:
                cover1.SetActive(false);
                cover2.SetActive(false);
                cover3.SetActive(true);
                break;
        }

        if (GameManager.instance.playerDead)
        {
            deathUI.SetActive(true);
            Buttons.SetActive(false);
        }
        else
        {
            deathUI.SetActive(false);
            Buttons.SetActive(true);
        }
    }
}

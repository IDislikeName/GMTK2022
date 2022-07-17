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

    public GameObject collectablesImage1;
    public GameObject collectablesImage2;
    public GameObject collectablesImage3;
    Dictionary<int, GameObject> collection;

    public GameObject successUI;

    // Start is called before the first frame update
    void Start()
    {
        deathUI.SetActive(false);
        collection = new() {
            {1, collectablesImage1},
            {2, collectablesImage2},
            {3, collectablesImage3}
        };
    }

    // Update is called once per frame
    void Update()
    {
        // �л�������ʾ
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
        
        // �����������棬���ڵ�����������ʱ�Ƴ����Ͻǵİ�ť
        if (GameManager.instance.playerDead && GameManager.instance.playerDeadLegacy)
        {
            deathUI.SetActive(true);
            Buttons.SetActive(false);
            freez(true);
        }
        else
        {
            deathUI.SetActive(false);
            Buttons.SetActive(true);
            freez(false);
        }

        // ��ʾ��ǰ�ռ������ռ���
        for (int i = 1; i < 4; i++)
        {
                collection[i].SetActive(GameManager.instance.collectablesGot[i]);
        }

        successUI.SetActive(GameManager.instance.playerCompleted);
    }

    void freez(bool wantFreeze)
    {
        // �������Ӧ�����и����Ե�ui������ʱ����
        // ���Զ�����Ϸ����
        GameManager.instance.isFreez = wantFreeze;
    }
}

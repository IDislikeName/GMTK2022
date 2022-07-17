using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    List<GameObject> InteractiveObjects = new();
    Stack<List<GameObject>> ObjectsStacks = new();
    Stack<int> WorldNumberStacks = new();
    Stack<int> RecallRecords = new();  //���������¼ÿһ�λ���ʱӦ�û��ݶ������ǻ�������

    public int inWorldNumber = 2;

    public string currentLevelName;

    public bool playerDead = false;

    public GameObject BackgoundUI;

    public Dictionary<int, bool> collectablesGot;

    #region SingletonDeclaration 
    public static GameManager instance;
    public static GameManager FindInstance()
    {
        return instance; //that's just a singletone as the region says
    }

    private void Awake() //this happens before the game even starts and it's a part of the singletone
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

        //init game
        WorldSwitch(2);
        // ���ռ����ռ�������ռ���֮���Ӧ��ű�Ϊtrue��
        collectablesGot = new() {
            {1, false},
            {2, false},
            {3, false}
        };
    }

    private void Update()
    {

    }
    #endregion

    // ʵ��Undo
    public void Regist(GameObject gameObject)  // ����������������ɱ��ı����Ʒ����list��
    {
        InteractiveObjects.Add(gameObject);
    }

    public void Obliterate(GameObject gameObject)
    {
        InteractiveObjects.Remove(gameObject);
    }

    public void SaveElse()  // ��������������浱ǰ���ϵ�״̬
    {
        // ��֪����ôд��ռ�ڴ棬�������ף����Ǿ��������ջ��ðɣ�������զ��
        List<GameObject> tempList = new List<GameObject>();
        foreach (var item in InteractiveObjects)
        {
            GameObject tempObject = Instantiate(item.gameObject, item.transform.position, Quaternion.identity);
            tempObject.SetActive(false);  //������������inactive��������Ӧ�û��ã����ǵ��Ե�ʱ��object��ʾ��Ƚ���
            tempList.Add(tempObject);
        }
        ObjectsStacks.Push(tempList);
        RecallRecords.Push(1);
    }

    public void SaveWorldNumber()
    {
        WorldNumberStacks.Push(inWorldNumber);
        RecallRecords.Push(2);
    }

    public void UnDo()
    {
        if (RecallRecords.Count == 0) return;
        switch (RecallRecords.Pop())
        {
            case 1:
                foreach (var item in InteractiveObjects)
                {
                    Destroy(item);
                }
                List<GameObject> tempList = ObjectsStacks.Pop();
                foreach (var item in tempList)
                {
                    item.SetActive(true);
                }
                break;
            case 2:
                inWorldNumber = WorldNumberStacks.Pop();
                break;
        }
    }

    //����ʵ����Undo��

    public void Restart()
    {
        SceneManager.LoadScene(currentLevelName);
    }
    
    public void WorldSwitch(int Target)
    {
        SaveWorldNumber();
        //�������ֻӦ�ñ���ť����
        inWorldNumber = Target;
        BackgoundUI.GetComponent<BackgroundSwitch>().BackgroundSwitchTo(Target);
    }
}
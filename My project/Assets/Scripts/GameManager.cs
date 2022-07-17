using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    List<GameObject> InteractiveObjects = new();
    Stack<List<GameObject>> ObjectsStacks = new();
    Stack<int> WorldNumberStacks = new();
    Stack<int> RecallRecords = new();  //这个用来记录每一次回溯时应该回溯动作还是回溯世界

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
        // 存收集物收集情况，收集到之后对应序号变为true。
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

    // 实现Undo
    public void Regist(GameObject gameObject)  // 调用这个方法，将可被改变的物品传到list里
    {
        InteractiveObjects.Add(gameObject);
    }

    public void Obliterate(GameObject gameObject)
    {
        InteractiveObjects.Remove(gameObject);
    }

    public void SaveElse()  // 调用这个方法保存当前场上的状态
    {
        // 我知道这么写很占内存，蠢的离谱，但是就先这样凑活用吧，还能离咋滴
        List<GameObject> tempList = new List<GameObject>();
        foreach (var item in InteractiveObjects)
        {
            GameObject tempObject = Instantiate(item.gameObject, item.transform.position, Quaternion.identity);
            tempObject.SetActive(false);  //不过这里做了inactive，玩起来应该还好，就是调试的时候object显示会比较乱
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

    //以上实现了Undo。

    public void Restart()
    {
        SceneManager.LoadScene(currentLevelName);
    }
    
    public void WorldSwitch(int Target)
    {
        SaveWorldNumber();
        //这个方法只应该被按钮调用
        inWorldNumber = Target;
        BackgoundUI.GetComponent<BackgroundSwitch>().BackgroundSwitchTo(Target);
    }
}
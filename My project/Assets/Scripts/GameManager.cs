using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    List<GameObject> InteractiveObjects = new List<GameObject>();
    Stack<List<GameObject>> ObjectsStacks = new Stack<List<GameObject>>();

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnDo();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
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

    public void Save()  // 调用这个方法保存当前场上的状态
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
    }

    public void UnDo()
    {
        if (ObjectsStacks.Count == 0) return;
        foreach (var item in InteractiveObjects)
        {
            Destroy(item);
        }
        List<GameObject> tempList = ObjectsStacks.Pop();
        foreach (var item in tempList)
        {
            item.SetActive(true);
        }
    }

    //以上实现了Undo，碎碎念：我在做测试的时候碰到了一个奇怪的bug，应该是我undo代码里面出了一些问题，导致最开始的那个角色被移除了。

    void Restart()
    {
        SceneManager.LoadScene("lv1");
    }
}
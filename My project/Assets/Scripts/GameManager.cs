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

    // ʵ��Undo
    public void Regist(GameObject gameObject)  // ����������������ɱ��ı����Ʒ����list��
    {
        InteractiveObjects.Add(gameObject);
    }

    public void Obliterate(GameObject gameObject)
    {
        InteractiveObjects.Remove(gameObject);
    }

    public void Save()  // ��������������浱ǰ���ϵ�״̬
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

    //����ʵ����Undo����������������Ե�ʱ��������һ����ֵ�bug��Ӧ������undo�����������һЩ���⣬�����ʼ���Ǹ���ɫ���Ƴ��ˡ�

    void Restart()
    {
        SceneManager.LoadScene("lv1");
    }
}
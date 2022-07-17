using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    LayerMask lm;

    public int CollectablesType;
    
    // Start is called before the first frame update
    void Start()
    {
        lm = LayerMask.GetMask("Players");
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D temp = GetColliderAt(this.transform.position);
        if(temp != null)
        {
            switch (temp.gameObject.tag)
            {
                case "PlayerMiddle":
                    if(GameManager.instance.inWorldNumber == 2)
                    {
                        GameManager.instance.UpdateCollectableStatus(CollectablesType, true);
                        Destroy(this.gameObject);
                    }
                    break;
                case "PlayerLeft":
                    if (GameManager.instance.inWorldNumber == 1)
                    {
                        GameManager.instance.UpdateCollectableStatus(CollectablesType, true);
                        Destroy(this.gameObject);
                    }
                    break;
                case "PlayerRight":
                    if (GameManager.instance.inWorldNumber == 3)
                    {
                        GameManager.instance.UpdateCollectableStatus(CollectablesType, true);
                        Destroy(this.gameObject);
                    }
                    break;
            }
        }
    }

    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector3.zero, .3f, lm);
        return hit.collider;
    }

    private void OnEnable()
    {
        try
        {
            GameManager.instance.UpdateCollectableStatus(CollectablesType, false);
        }
        catch
        {
            // 这里初始化的时候因为onEable好像在GameManager变Instance之前就会调用了，所以用个try把这个排掉，catch不做操作。
        }
    }

}

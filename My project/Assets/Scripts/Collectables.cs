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
                        GameManager.instance.collectablesGot[CollectablesType] = true;
                        Destroy(this.gameObject);
                    }
                    break;
                case "PlayerLeft":
                    if (GameManager.instance.inWorldNumber == 1)
                    {
                        GameManager.instance.collectablesGot[CollectablesType] = true;
                        Destroy(this.gameObject);
                    }
                    break;
                case "PlayerRight":
                    if (GameManager.instance.inWorldNumber == 3)
                    {
                        GameManager.instance.collectablesGot[CollectablesType] = true;
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
        GameManager.instance.collectablesGot[CollectablesType] = false;
    }

}

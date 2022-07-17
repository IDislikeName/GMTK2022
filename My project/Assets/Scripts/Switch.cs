using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    LayerMask lm;
    public bool on = false;
    public Sprite pressed;
    public Sprite notpressed;
    public SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lm = LayerMask.GetMask("Pushables", "Players");
    }
    private void Update()
    {
        Collider2D col = GetColliderAt(transform.position);
        if (col!=null)
        {
            if (col.CompareTag("Box"))
            {
                on = true;
                sr.sprite = pressed;
            }
            else
            {
                // 这里是为了实现让人物踩上去也能把门打开的效果。
                if(col.gameObject.layer == 11)
                {
                on = true;
                sr.sprite = pressed;
                }
            }
        }
        else
        {
            on = false;
            sr.sprite = notpressed;
        }
    }
    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector3.zero,.3f,lm);
        return hit.collider;
    }
}

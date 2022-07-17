using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public LayerMask lm;
    public bool on = false;
    private void Update()
    {
        Collider2D col = GetColliderAt(transform.position);
        if (col!=null)
        {
            if (col.tag=="Box")
            {
                on = true;
            }
            else
            {
                on = false;
            }
        }
    }
    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector3.zero,.3f,lm);
        return hit.collider;
    }
}

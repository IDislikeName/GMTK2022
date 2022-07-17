using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform boxToMove;

    GameObject LeftPlayer;
    GameObject RightPlayer;

    public LayerMask Filter;
    void Awake()
    {
        LeftPlayer = findChildWithTag(this.gameObject, "PlayerLeft");
        RightPlayer = findChildWithTag(this.gameObject, "PlayerRight");
    }

    void Update()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection = GetMovementInput();
        boxToMove = null;
        if (movementDirection != Vector3.zero && CanMove(movementDirection))
        {
            GameManager.instance.SaveElse();
            MoveTo(movementDirection);

        }
        DeathCheck();
        UpdateCollision();
    }

    void MoveTo(Vector3 movementDirection)
    {
        transform.position += movementDirection;
        if (boxToMove != null)
        {
            boxToMove.transform.position += movementDirection;
        }
    }

    Vector3 GetMovementInput()
    {
        Vector3 inputValue = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputValue = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputValue = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputValue = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputValue = Vector3.down;
        }
        return inputValue;
    }


    bool CanMove(Vector3 direction)
    {
        Vector3 CurrentPosition = Vector3.zero;
        if (GameManager.instance.inWorldNumber == 2)
        {
            CurrentPosition = this.transform.position;
        }
        else if (GameManager.instance.inWorldNumber == 1)
        {
            CurrentPosition = LeftPlayer.transform.position;
        }
        else
        {
            CurrentPosition = RightPlayer.transform.position;
        }


        Vector3 positionToCheckFirst = CurrentPosition + direction;
        Collider2D colliderFirst = GetColliderAt(positionToCheckFirst);
        Vector3 positionToCheckSecond = positionToCheckFirst + direction;
        Collider2D colliderSecond = GetColliderAt(positionToCheckSecond);
        if (colliderFirst == null)
        {
            return true;
        }
        else if (colliderFirst.tag == "Box")
        {
            if (colliderSecond == null|| colliderSecond.tag =="Switch")
            {
                boxToMove = colliderFirst.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }

    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector3.zero);
        return hit.collider;
    }

    void DeathCheck()
    {
        // 基本思路是从左到右依次check
        // 而且主世界的check如果不通过直接报死亡

        Collider2D GetColliderAtSpecifiedLayer(Vector3 position)
        {
            RaycastHit2D hit;
            hit = Physics2D.CircleCast(position, .3f, Vector3.zero, .3f, Filter);
            return hit.collider;
        }

        bool DeathCheckBasic(Vector3 position)
        {
            if (GetColliderAtSpecifiedLayer(position) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        bool DeathCheckLeft()
        {
            if (DeathCheckBasic(LeftPlayer.transform.position))
            {
                LeftPlayer.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); //这里就姑且用换颜色代替一下，等立绘出来了之后直接换立绘就好
                return true;
            }
            else
            {
                LeftPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1); //这里就姑且用换颜色代替一下，等立绘出来了之后直接换立绘就好
            }
            return false;
        }
        
        bool DeathCheckMiddle()
        {
            if (DeathCheckBasic(this.transform.position))
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); //这里就姑且用换颜色代替一下，等立绘出来了之后直接换立绘就好
                return true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1); //这里就姑且用换颜色代替一下，等立绘出来了之后直接换立绘就好
            }
            return false;
        }
        
        bool DeathCheckRight()
        {
            if (DeathCheckBasic(RightPlayer.transform.position))
            {
                RightPlayer.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); //这里就姑且用换颜色代替一下，等立绘出来了之后直接换立绘就好
                return true;
            }
            else
            {
                RightPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1); //这里就姑且用换颜色代替一下，等立绘出来了之后直接换立绘就好
            }
            return false;
        }
        
        switch (GameManager.instance.inWorldNumber)
        {
            case 1:
                DeathCheckMiddle();
                DeathCheckRight();
                if (DeathCheckLeft())
                {
                    print("Dead1");  //这里后面要换成切UI
                }
                break;
            case 2:
                DeathCheckLeft();
                DeathCheckRight();
                if (DeathCheckMiddle())
                {
                    print("Dead2");  //这里后面要换成切UI
                }
                break;
            case 3:
                DeathCheckLeft();
                DeathCheckMiddle();
                if (DeathCheckRight())
                {
                    print("Dead3");  //这里后面要换成切UI
                }
                break;
        }
    }

    GameObject findChildWithTag(GameObject parent, string tagName)
    {
        // 这个方法仅能找到父物体下的第一个有着给定tag的子物体
        for(int i = 0;i<parent.transform.childCount;i++)
        {
            GameObject temp = parent.transform.GetChild(i).gameObject;
            if (temp.tag == tagName)
            {
                return temp;
            }
        }
        return null;
    }
    public void UpdateCollision()
    {
        if (GameManager.instance.inWorldNumber == 1)
        {
            
        }
    }
}

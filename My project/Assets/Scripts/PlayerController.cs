using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform boxToMove;
    Transform boxToMove2;

    GameObject LeftPlayer;
    GameObject RightPlayer;

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
        boxToMove2 = null;
        if (movementDirection != Vector3.zero && CanMove(movementDirection))
        {
            GameManager.instance.SaveElse();
            MoveTo(movementDirection);

        }
        DeathCheck();

    }

    void MoveTo(Vector3 movementDirection)
    {
        transform.position += movementDirection;
        if (boxToMove != null)
        {
            boxToMove.transform.position += movementDirection;
            SoundManager.instance.PlayClip(SoundManager.instance.boxpush);
        }
        if (boxToMove2 != null)
        {
            boxToMove2.transform.position += movementDirection;
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
        if (GameManager.instance.isFreez)
        {
            return false;
        }
        Vector3 CurrentPosition = Vector3.zero;

        switch (GameManager.instance.inWorldNumber)  // 用这个判断来实现切换世界时使用不同的碰撞箱
        {
            case 1:
                CurrentPosition = LeftPlayer.transform.position;
                break;
            case 2:
                CurrentPosition = this.transform.position;
                break;
            case 3:
                CurrentPosition = RightPlayer.transform.position;
                break;
        }


        Vector3 positionToCheckFirst = CurrentPosition + direction;
        Collider2D colliderFirst = GetColliderAt(positionToCheckFirst);
        Vector3 positionToCheckSecond = positionToCheckFirst + direction;
        Collider2D colliderSecond = GetColliderAt(positionToCheckSecond);
        Vector3 positionToCheckThird = positionToCheckSecond + direction;
        Collider2D colliderThird = GetColliderAt(positionToCheckThird);
        if (colliderFirst == null || colliderFirst.CompareTag("Collectables"))
        {
            return true;
        }
        else if (colliderFirst.CompareTag("Box"))
        {
            if (colliderSecond == null || colliderSecond.CompareTag("Switch"))
            {
                boxToMove = colliderFirst.transform;
                return true;
            }
            else
            {
                if (colliderSecond.CompareTag("Box"))
                {
                    if (colliderThird == null || colliderThird.CompareTag("Switch"))
                    {
                        boxToMove = colliderFirst.transform;
                        boxToMove2 = colliderSecond.transform;
                        return true;
                    }

                }
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
            LayerMask Filter = LayerMask.GetMask("HardBlock", "Pushables");
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
                LeftPlayer.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0f, 0f, 1); 
                return true;
            }
            else
            {
                LeftPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1); 
            }
            return false;
        }
        
        bool DeathCheckMiddle()
        {
            if (DeathCheckBasic(this.transform.position))
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0f, 0f, 1); 
                return true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
            }
            return false;
        }
        
        bool DeathCheckRight()
        {
            if (DeathCheckBasic(RightPlayer.transform.position))
            {
                RightPlayer.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0f, 0f, 1);
                return true;
            }
            else
            {
                RightPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
            }
            return false;
        }

        GameManager.instance.playerDead = false;

        switch (GameManager.instance.inWorldNumber)
        {
            case 1:
                DeathCheckMiddle();
                DeathCheckRight();
                if (DeathCheckLeft())
                {
                    GameManager.instance.playerDead = true;
                }
                break;
            case 2:
                DeathCheckLeft();
                DeathCheckRight();
                if (DeathCheckMiddle())
                {
                    GameManager.instance.playerDead = true;
                }
                break;
            case 3:
                DeathCheckLeft();
                DeathCheckMiddle();
                if (DeathCheckRight())
                {
                    GameManager.instance.playerDead = true;
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
}

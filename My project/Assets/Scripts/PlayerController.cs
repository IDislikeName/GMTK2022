using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int inWorldNumber = 2;
    Transform boxToMove;

    GameObject LeftPlayer;
    GameObject RightPlayer;
    void Awake()
    {
        LeftPlayer = GameObject.FindGameObjectWithTag("PlayerLeft");
        RightPlayer = GameObject.FindGameObjectWithTag("PlayerRight");
    }

    void Update()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection = GetMovementInput();
        boxToMove = null;
        if (movementDirection != Vector3.zero && CanMove(movementDirection))
        {
            GameManager.instance.Save();
            MoveTo(movementDirection);
            DeathCheck();
        }

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
        if (inWorldNumber == 2)
        {
            CurrentPosition = this.transform.position;
        }
        else if (inWorldNumber == 1)
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
            if (colliderSecond == null)
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

    public void WorldSwitch(int Target)
    {
        //�������ֻӦ�ñ���ť����
        inWorldNumber = Target;

        DeathCheck();
    }

    void DeathCheck()
    {
        // ����˼·�Ǵ���������check
        // �����������check�����ͨ��ֱ�ӱ�����
        bool DeathCheckBasic(Vector3 position)
        {
            if (GetColliderAt(position) != null)
            {
                string temp = GetColliderAt(position).tag;
                if (temp != "PlayerLeft" && temp != "PlayerMiddle" && temp != "PlayerRight")
                {
                    return true;
                }
                return false;
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
                LeftPlayer.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); //����͹����û���ɫ����һ�£������������֮��ֱ�ӻ�����ͺ�
                return true;
            }
            else
            {
                LeftPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1); //����͹����û���ɫ����һ�£������������֮��ֱ�ӻ�����ͺ�
            }
            return false;
        }
        
        bool DeathCheckMiddle()
        {
            if (DeathCheckBasic(this.transform.position))
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); //����͹����û���ɫ����һ�£������������֮��ֱ�ӻ�����ͺ�
                return true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1); //����͹����û���ɫ����һ�£������������֮��ֱ�ӻ�����ͺ�
            }
            return false;
        }
        
        bool DeathCheckRight()
        {
            if (DeathCheckBasic(RightPlayer.transform.position))
            {
                RightPlayer.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1); //����͹����û���ɫ����һ�£������������֮��ֱ�ӻ�����ͺ�
                return true;
            }
            else
            {
                RightPlayer.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1); //����͹����û���ɫ����һ�£������������֮��ֱ�ӻ�����ͺ�
            }
            return false;
        }
        
        switch (inWorldNumber)
        {
            case 1:
                DeathCheckMiddle();
                DeathCheckRight();
                if (DeathCheckLeft())
                {
                    print("Dead1");  //�������Ҫ������UI
                }
                break;
            case 2:
                DeathCheckLeft();
                DeathCheckRight();
                if (DeathCheckMiddle())
                {
                    print("Dead2");  //�������Ҫ������UI
                }
                break;
            case 3:
                DeathCheckLeft();
                DeathCheckMiddle();
                if (DeathCheckRight())
                {
                    print("Dead3");  //�������Ҫ������UI
                }
                break;
        }
    }

}

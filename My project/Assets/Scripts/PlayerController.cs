using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int inWorldNumber = 2;
    Transform boxToMove;
    void Awake()
    {

    }

    void Update()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection = GetMovementInput();
        boxToMove = null;
        if (movementDirection != Vector3.zero && CanMove(movementDirection))
        {
            MoveTo(movementDirection);
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
            GameManager.instance.Save();
            inputValue = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.instance.Save();
            inputValue = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.instance.Save();
            inputValue = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.instance.Save();
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
            CurrentPosition = GameObject.FindGameObjectWithTag("PlayerLeft").transform.position;
        }
        else
        {
            CurrentPosition = GameObject.FindGameObjectWithTag("PlayerRight").transform.position;
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
        if (hit)
        {
            Debug.Log(hit.collider);
        }
        return hit.collider;
    }

    public void WorldSwitch(int Target)
    {
        //这个方法只应该被按钮调用
        inWorldNumber = Target;
    }

}

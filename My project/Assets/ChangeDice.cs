using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDice : MonoBehaviour
{
    public int face;
    public GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeFace();
        }
    }
    public void ChangeFace()
    {
        GameObject temp = players[face];
        Vector3 ptemp = players[face].transform.position;
        int ftem = face;
        face++;
        if (face > 2)
        {
            face = 0;
        }
        players[ftem] = players[face];
        players[face] = temp;
        players[ftem].transform.position = players[face].transform.position;
        players[face].transform.position = temp.transform.position;
    }
}

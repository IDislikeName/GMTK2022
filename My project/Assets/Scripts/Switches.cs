using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour
{
    public GameObject door;
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activated = CheckActivation();
        door.SetActive(!activated);
    }
    public bool CheckActivation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Switch>().on)
            {
                return false;
            }
        }
        return true;
    }
}

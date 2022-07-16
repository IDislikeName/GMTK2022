using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoMaterials : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Regist(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        GameManager.instance.Obliterate(gameObject);
    }
}
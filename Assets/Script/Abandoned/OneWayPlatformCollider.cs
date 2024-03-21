using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformCollider : MonoBehaviour
{
    private Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //判断是否在平台上，玩家在平台上时变成平台的子类
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.parent = gameObject.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //离开平台
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.parent = playerTrans;
        }
    }
}

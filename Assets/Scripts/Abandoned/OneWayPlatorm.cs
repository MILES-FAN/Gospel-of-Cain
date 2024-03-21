using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatorm : MonoBehaviour
{
    public float speed;
    public Transform movePos;
    public GameObject trigger;

    private bool activated;

   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            moving();
        }
    }

    void moving()
    {
        //平台移动
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //判断接触，这里实现方法有点怪，我把这个物体的碰撞箱放在按钮的位置上然后使用这个代码
        //因为我不知道怎么在这个文件里检测按钮那个图像的碰撞情况，所以我就干脆直接把平台的碰撞箱放在按钮的图像上然后做了个trigger
        //平台本身踩住的那个碰撞箱我弄了个子物体，在这个物体上做非触发器的碰撞箱充当地板
        if (other.gameObject.CompareTag("Player"))
        {
            activated = true;
        }
    }


}

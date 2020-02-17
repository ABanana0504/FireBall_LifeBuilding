using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
//using UnityEngine.SceneManagement;
public class Head : MonoBehaviour
{
    private static Head _head;
    public static Head GetHead
    {
        get
        {
            return _head;
        }
    }
    public GameObject head;                //获取预设    
    public GameObject worker;              //获取工人预设
    public GameObject lineHolder;          //存放工人
    public bool isGot = false;              //是否获取建筑材料
    public bool isOver;             //是否游戏结束
    public bool isDeleted;                  //是否删除十名工人
    public bool isSlow;                     //是否速度减半
    public bool isSlowRepeat;

    private Vector3 headPos;        //头位置
    private int step = 24;          //每次移动间隔
    private int x;              //移动方向
    private int y;
    private float slowTime = 20;   
 
    public List<GameObject> workerList = new List<GameObject>();       //队伍列表
    private Transform preTramHead;
    void Awake()
    {
        _head = this;
    }

    void Start()
    {
        InvokeRepeating("Move", 0, 0.13f);         //循环移动语句
        x = step;
        y = 0;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");       //水平
        float v = Input.GetAxisRaw("Vertical");         //垂直
        if ((h > 0 || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && x != -step)                                     //右
        {
            x = step;
            y = 0;
        }
        else if ((h < 0 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && x != step)                                  //左
        {
            x = -step;
            y = 0;
        }
        else if ((v < 0 || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && y != step)                                  //下
        {
            x = 0;
            y = -step;
        }
        else if ((v > 0 || Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.UpArrow)) && y != -step)                                  //上
        {
            x = 0;
            y = step;
        }
        //Debug.Log(workerList.ToArray().Length);
        if (isDeleted)
        {
            DeleteWorker();
        }
        if (isSlow)
        {
            if (isSlowRepeat)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, 0.2f);
                isSlowRepeat = false;
            }            
            slowTime -= Time.deltaTime;
            if (slowTime < 0)
            {
                slowTime = 20;
                CancelInvoke();
                InvokeRepeating("Move", 0, 0.1f);
                isSlow = false;
            }
            
        }
    }

    void Move()
    {   //头移动      
        //Debug.Log(head.transform.position);     
        if (!isOver)
        {
            headPos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);                  //位移
            if (workerList.ToArray().Length > 1)
            {
                for (int i = workerList.ToArray().Length - 2; i >= 0; i--)
                {   //身体位置                
                    workerList[i + 1].transform.localPosition = workerList[i].transform.localPosition;
                }
            }
            if (workerList.ToArray().Length != 0)
            {
                workerList[0].transform.localPosition = headPos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)                     //头碰撞判断
    {
        switch (collision.tag)
        {
            case "Material":
                //Debug.Log("M");
                isGot = true;
                Grow();
                Destroy(collision.gameObject);               
                break;
            case "Wall":
            case "Worker":
                isOver = true;
                break;
        }
    }
    void Grow()
    {   //增加一名工人        
        GameObject workerBody = Instantiate(worker, new Vector3(2000, 2000, 0), transform.localRotation);
        workerBody.transform.SetParent(lineHolder.transform, false);
        workerList.Add(workerBody);
    }

    void DeleteWorker()
    {
        //Debug.Log("Delete");
        int workerIndex = workerList.ToArray().Length - 1;
        for (int i = workerIndex; i > workerIndex - 10; i--)
        {
            Destroy(workerList[i].gameObject);
            workerList.RemoveAt(i);

        }
        isDeleted = false;
        //Debug.Log(workerList.ToArray().Length);
    }


}

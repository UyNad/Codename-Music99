using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager NMinstance;
    
    // 初始化队列，先进先出，为了保证每次只按第一个
    public Queue<GameObject> key1 = new Queue<GameObject>();
    public Queue<GameObject> key2 = new Queue<GameObject>();
    public Queue<GameObject> key3 = new Queue<GameObject>();
    public Queue<GameObject> key4 = new Queue<GameObject>();

    // 四条轨道的x位置
    public float key1_x;
    public float key2_x;
    public float key3_x;
    public float key4_x;

    // 按一次只会判定一个，布尔值（相当傻逼，但能满足需求，所以就这么写了）
    public bool key1_HasPressed = false;
    public bool key2_HasPressed = false;
    public bool key3_HasPressed = false;
    public bool key4_HasPressed = false;

    //Manager生成note，以及加入队列；note自己判断自己是不是第一个，不是的话就不会触发hit事件
    public GameObject gameNote_1;
    public GameObject gameNote_2;
    public GameObject gameNote_3;
    public GameObject gameNote_4;
    public GameObject gameNoteParent;

    // 在这里用数组配置Note，浮点数对应初始位置，非常傻逼但目前先这样，策划对不起
    public float[] gameNoteKey1Group;
    public float[] gameNoteKey2Group;
    public float[] gameNoteKey3Group;
    public float[] gameNoteKey4Group;

    public BeatScroller BS;

    // 生成GameNote1的方法
    public void InsGameNote_1(float yPosition)
    {
        key1.Enqueue(Instantiate (gameNote_1, new Vector3(key1_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }

    // 生成GameNote2的方法
    public void InsGameNote_2(float yPosition)
    {
        key2.Enqueue(Instantiate(gameNote_2, new Vector3(key2_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }

    // 生成GameNote3的方法
    public void InsGameNote_3(float yPosition)
    {
        key3.Enqueue(Instantiate(gameNote_3, new Vector3(key3_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }

    // 生成GameNote4的方法
    public void InsGameNote_4(float yPosition)
    {
        key4.Enqueue(Instantiate(gameNote_4, new Vector3(key4_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }
    // Start is called before the first frame update
    private void Awake()
    {
        NMinstance = this;
        // 初始化GameNote1
        foreach (float i in gameNoteKey1Group)
        {
            
            InsGameNote_1(i * BS.beatTempo);
        }
        // 初始化GameNote2
        foreach (float i in gameNoteKey2Group)
        {
            InsGameNote_2(i * BS.beatTempo);
        }
        // 初始化GameNote3
        foreach (float i in gameNoteKey3Group)
        {
            InsGameNote_3(i * BS.beatTempo);
        }
        // 初始化GameNote4
        foreach (float i in gameNoteKey4Group)
        {
            InsGameNote_4(i * BS.beatTempo);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

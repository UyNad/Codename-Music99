using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager NMinstance;
    
    // ��ʼ�����У��Ƚ��ȳ���Ϊ�˱�֤ÿ��ֻ����һ��
    public Queue<GameObject> key1 = new Queue<GameObject>();
    public Queue<GameObject> key2 = new Queue<GameObject>();
    public Queue<GameObject> key3 = new Queue<GameObject>();
    public Queue<GameObject> key4 = new Queue<GameObject>();

    // ���������xλ��
    public float key1_x;
    public float key2_x;
    public float key3_x;
    public float key4_x;

    // ��һ��ֻ���ж�һ��������ֵ���൱ɵ�ƣ����������������Ծ���ôд�ˣ�
    public bool key1_HasPressed = false;
    public bool key2_HasPressed = false;
    public bool key3_HasPressed = false;
    public bool key4_HasPressed = false;

    //Manager����note���Լ�������У�note�Լ��ж��Լ��ǲ��ǵ�һ�������ǵĻ��Ͳ��ᴥ��hit�¼�
    public GameObject gameNote_1;
    public GameObject gameNote_2;
    public GameObject gameNote_3;
    public GameObject gameNote_4;
    public GameObject gameNoteParent;

    // ����������������Note����������Ӧ��ʼλ�ã��ǳ�ɵ�Ƶ�Ŀǰ���������߻��Բ���
    public float[] gameNoteKey1Group;
    public float[] gameNoteKey2Group;
    public float[] gameNoteKey3Group;
    public float[] gameNoteKey4Group;

    public BeatScroller BS;

    // ����GameNote1�ķ���
    public void InsGameNote_1(float yPosition)
    {
        key1.Enqueue(Instantiate (gameNote_1, new Vector3(key1_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }

    // ����GameNote2�ķ���
    public void InsGameNote_2(float yPosition)
    {
        key2.Enqueue(Instantiate(gameNote_2, new Vector3(key2_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }

    // ����GameNote3�ķ���
    public void InsGameNote_3(float yPosition)
    {
        key3.Enqueue(Instantiate(gameNote_3, new Vector3(key3_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }

    // ����GameNote4�ķ���
    public void InsGameNote_4(float yPosition)
    {
        key4.Enqueue(Instantiate(gameNote_4, new Vector3(key4_x, yPosition, 0), Quaternion.identity, gameNoteParent.transform));
    }
    // Start is called before the first frame update
    private void Awake()
    {
        NMinstance = this;
        // ��ʼ��GameNote1
        foreach (float i in gameNoteKey1Group)
        {
            
            InsGameNote_1(i * BS.beatTempo);
        }
        // ��ʼ��GameNote2
        foreach (float i in gameNoteKey2Group)
        {
            InsGameNote_2(i * BS.beatTempo);
        }
        // ��ʼ��GameNote3
        foreach (float i in gameNoteKey3Group)
        {
            InsGameNote_3(i * BS.beatTempo);
        }
        // ��ʼ��GameNote4
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

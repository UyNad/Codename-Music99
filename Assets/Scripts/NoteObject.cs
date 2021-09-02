using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool CanBePressed;

    public KeyCode KeyToPress;
    private bool hasScored;

    private float noteDistance;
    private float goodPercent;

    [Tooltip("判定线，用来取Y轴位置")]
    public GameObject JudgementLines;

    public 

    // Start is called before the first frame update
    void Start()
    {
        hasScored = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (NoteManager.NMinstance.key1.Count != 0)
        {
            if(gameObject == NoteManager.NMinstance.key1.Peek())
            {
                print("1st");
                if (Input.GetKeyDown(KeyToPress))
                {
                    if(CanBePressed)
                    {
                        CalculateScore();
                        NoteManager.NMinstance.key1.Dequeue();
                    }    
                }
            }
        }
        if (NoteManager.NMinstance.key2.Count != 0)
        {
            if (gameObject == NoteManager.NMinstance.key2.Peek())
            {
                print("2nd");
                if (Input.GetKeyDown(KeyToPress))
                {
                    if (CanBePressed)
                    {
                        CalculateScore();
                        NoteManager.NMinstance.key2.Dequeue();
                    }
                }
            }
        }
        if (NoteManager.NMinstance.key3.Count != 0)
        {
            if (gameObject == NoteManager.NMinstance.key3.Peek())
            {
                print("3rd");
                if (Input.GetKeyDown(KeyToPress))
                {
                    if (CanBePressed)
                    {
                        CalculateScore();
                        NoteManager.NMinstance.key3.Dequeue();
                    }
                }
            }
        }
        if (NoteManager.NMinstance.key4.Count != 0)
        {
            if (gameObject == NoteManager.NMinstance.key4.Peek())
            {
                print("4st");
                if (Input.GetKeyDown(KeyToPress))
                {
                    if (CanBePressed)
                    {
                        CalculateScore();
                        NoteManager.NMinstance.key4.Dequeue();
                    }
                }
            }
        }
        //if (this.gameObject == NoteManager.NMinstance.key1.Peek() | this.gameObject == NoteManager.NMinstance.key2.Peek() | this.gameObject == NoteManager.NMinstance.key3.Peek() | this == NoteManager.NMinstance.key4.Peek())
        //{
        //    print("1st");
        //    // 如果触发了对应按键
        //    if (Input.GetKeyDown(KeyToPress))
        //    {
        //        // 如果此时可以判定
        //        if (CanBePressed)
        //        {

        //            CalculateScore();

        //            IfPeekLeave();
        //        }
        //    }
        //}
    }

    private void CalculateScore()
    {
        // 按照y轴位置计算精准度
        noteDistance = Mathf.Abs(JudgementLines.transform.position.y - transform.position.y);
        // Note小于100%距离时
        if (noteDistance <= GameManager.GMinstance.perfectInterval)
        {
            GameManager.GMinstance.PerfectScored();
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
        }
        // Note大于100%距离但小于10%距离时
        else if (noteDistance > GameManager.GMinstance.perfectInterval && noteDistance < GameManager.GMinstance.missInterval)
        {
            goodPercent = Mathf.Ceil(Mathf.Abs(noteDistance - GameManager.GMinstance.goodInterval)
                / ((GameManager.GMinstance.goodInterval - GameManager.GMinstance.perfectInterval) / 9)) / 10;

            // debug打印
            print(goodPercent);

            GameManager.GMinstance.GoodScored(goodPercent);
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
        }
        // Note大于MISS距离时
        else if (noteDistance >= GameManager.GMinstance.missInterval)
        {
            GameManager.GMinstance.NoteMissed();
        }



        hasScored = true;
        gameObject.SetActive(false);
    }

    private void IfPeekLeave()
    {
        // 对应的队列退出一个，傻逼代码但暂时这么写
        if (this.gameObject == NoteManager.NMinstance.key1.Peek())
        {
            NoteManager.NMinstance.key1.Dequeue();
        }
        if (this.gameObject == NoteManager.NMinstance.key2.Peek())
        {
            NoteManager.NMinstance.key2.Dequeue();
        }
        if (this.gameObject == NoteManager.NMinstance.key3.Peek())
        {
            NoteManager.NMinstance.key3.Dequeue();
        }
        if (this.gameObject == NoteManager.NMinstance.key4.Peek())
        {
            NoteManager.NMinstance.key4.Dequeue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // 进入了判定范围，可以按了
        if(other.tag == "Activator")
        {
            CanBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        // 出了判定范围还没得分，所以MISS了
        if (other.tag == "Activator" && hasScored == false)
        {
            CanBePressed = false;
            GameManager.GMinstance.NoteMissed();
            IfPeekLeave();
        }
    }
}

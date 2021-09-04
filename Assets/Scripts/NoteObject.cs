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

    public GameObject perfectEffect, goodEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        hasScored = false;

    }

    // Update is called once per frame
    void Update()
    {
        //如果松开的是A：key1状态未按下
        if(KeyToPress == KeyCode.A & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key1_HasPressed = false;
        }

        //如果松开的是S：key2状态未按下
        if (KeyToPress == KeyCode.S & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key2_HasPressed = false;
        }

        //如果松开的是D：key3状态未按下
        if (KeyToPress == KeyCode.D & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key3_HasPressed = false;
        }

        //如果松开的是F：key4状态未按下
        if (KeyToPress == KeyCode.F & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key4_HasPressed = false;
        }

        //如果key1还有得按：不断检测是否按到key1，并触发得分事件
        if (NoteManager.NMinstance.key1.Count != 0)
        {

            if(gameObject == NoteManager.NMinstance.key1.Peek())
            {
                if (NoteManager.NMinstance.key1_HasPressed == false)
                {
                    if (Input.GetKeyDown(KeyToPress))
                    {

                        if (CanBePressed)
                        {
                            NoteManager.NMinstance.key1_HasPressed = true;
                            CalculateScore();
                            NoteManager.NMinstance.key1.Dequeue();
                        }
                    }
                }

            }
        }

        //如果key2还有得按：不断检测是否按到key2，并触发得分事件
        if (NoteManager.NMinstance.key2.Count != 0)
        {
            if (gameObject == NoteManager.NMinstance.key2.Peek())
            {
                if (NoteManager.NMinstance.key2_HasPressed == false)
                {
                    if (Input.GetKeyDown(KeyToPress))
                    {
                        if (CanBePressed)
                        {
                            NoteManager.NMinstance.key2_HasPressed = true;
                            CalculateScore();
                            NoteManager.NMinstance.key2.Dequeue();
                        }
                    }
                }

            }
        }

        //如果key3还有得按：不断检测是否按到key3，并触发得分事件
        if (NoteManager.NMinstance.key3.Count != 0)
        {
            if (gameObject == NoteManager.NMinstance.key3.Peek())
            {
                if (NoteManager.NMinstance.key3_HasPressed == false)
                {
                    if (Input.GetKeyDown(KeyToPress))
                    {
                        if (CanBePressed)
                        {
                            NoteManager.NMinstance.key3_HasPressed = true;
                            CalculateScore();
                            NoteManager.NMinstance.key3.Dequeue();
                        }
                    }
                }

            }
        }

        //如果key4还有得按：不断检测是否按到key4，并触发得分事件
        if (NoteManager.NMinstance.key4.Count != 0)
        {
            if (gameObject == NoteManager.NMinstance.key4.Peek())
            {
                if (NoteManager.NMinstance.key4_HasPressed == false)
                {
                    if (Input.GetKeyDown(KeyToPress))
                    {
                        if (CanBePressed)
                        {
                            NoteManager.NMinstance.key4_HasPressed = true;
                            CalculateScore();
                            NoteManager.NMinstance.key4.Dequeue();
                        }
                    }
                }

            }
        }

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
            Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
        }
        // Note大于100%距离但小于10%距离时
        else if (noteDistance > GameManager.GMinstance.perfectInterval && noteDistance < GameManager.GMinstance.missInterval)
        {
            goodPercent = Mathf.Ceil(Mathf.Abs(noteDistance - GameManager.GMinstance.goodInterval)
                / ((GameManager.GMinstance.goodInterval - GameManager.GMinstance.perfectInterval) / 9)) / 10;


            GameManager.GMinstance.GoodScored(goodPercent);
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
            Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
        }
        // Note大于MISS距离时
        else if (noteDistance >= GameManager.GMinstance.missInterval)
        {
            GameManager.GMinstance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
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
            gameObject.SetActive(false);
            IfPeekLeave();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}

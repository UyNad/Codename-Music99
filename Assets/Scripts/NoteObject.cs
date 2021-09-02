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

    [Tooltip("�ж��ߣ�����ȡY��λ��")]
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
        //    // ��������˶�Ӧ����
        //    if (Input.GetKeyDown(KeyToPress))
        //    {
        //        // �����ʱ�����ж�
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
        // ����y��λ�ü��㾫׼��
        noteDistance = Mathf.Abs(JudgementLines.transform.position.y - transform.position.y);
        // NoteС��100%����ʱ
        if (noteDistance <= GameManager.GMinstance.perfectInterval)
        {
            GameManager.GMinstance.PerfectScored();
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
        }
        // Note����100%���뵫С��10%����ʱ
        else if (noteDistance > GameManager.GMinstance.perfectInterval && noteDistance < GameManager.GMinstance.missInterval)
        {
            goodPercent = Mathf.Ceil(Mathf.Abs(noteDistance - GameManager.GMinstance.goodInterval)
                / ((GameManager.GMinstance.goodInterval - GameManager.GMinstance.perfectInterval) / 9)) / 10;

            // debug��ӡ
            print(goodPercent);

            GameManager.GMinstance.GoodScored(goodPercent);
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
        }
        // Note����MISS����ʱ
        else if (noteDistance >= GameManager.GMinstance.missInterval)
        {
            GameManager.GMinstance.NoteMissed();
        }



        hasScored = true;
        gameObject.SetActive(false);
    }

    private void IfPeekLeave()
    {
        // ��Ӧ�Ķ����˳�һ����ɵ�ƴ��뵫��ʱ��ôд
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

        // �������ж���Χ�����԰���
        if(other.tag == "Activator")
        {
            CanBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        // �����ж���Χ��û�÷֣�����MISS��
        if (other.tag == "Activator" && hasScored == false)
        {
            CanBePressed = false;
            GameManager.GMinstance.NoteMissed();
            IfPeekLeave();
        }
    }
}

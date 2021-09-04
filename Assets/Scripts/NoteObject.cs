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

    public GameObject perfectEffect, goodEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        hasScored = false;

    }

    // Update is called once per frame
    void Update()
    {
        //����ɿ�����A��key1״̬δ����
        if(KeyToPress == KeyCode.A & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key1_HasPressed = false;
        }

        //����ɿ�����S��key2״̬δ����
        if (KeyToPress == KeyCode.S & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key2_HasPressed = false;
        }

        //����ɿ�����D��key3״̬δ����
        if (KeyToPress == KeyCode.D & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key3_HasPressed = false;
        }

        //����ɿ�����F��key4״̬δ����
        if (KeyToPress == KeyCode.F & Input.GetKeyUp(KeyToPress))
        {
            NoteManager.NMinstance.key4_HasPressed = false;
        }

        //���key1���еð������ϼ���Ƿ񰴵�key1���������÷��¼�
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

        //���key2���еð������ϼ���Ƿ񰴵�key2���������÷��¼�
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

        //���key3���еð������ϼ���Ƿ񰴵�key3���������÷��¼�
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

        //���key4���еð������ϼ���Ƿ񰴵�key4���������÷��¼�
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
        // ����y��λ�ü��㾫׼��
        noteDistance = Mathf.Abs(JudgementLines.transform.position.y - transform.position.y);
        // NoteС��100%����ʱ
        if (noteDistance <= GameManager.GMinstance.perfectInterval)
        {
            GameManager.GMinstance.PerfectScored();
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
            Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
        }
        // Note����100%���뵫С��10%����ʱ
        else if (noteDistance > GameManager.GMinstance.perfectInterval && noteDistance < GameManager.GMinstance.missInterval)
        {
            goodPercent = Mathf.Ceil(Mathf.Abs(noteDistance - GameManager.GMinstance.goodInterval)
                / ((GameManager.GMinstance.goodInterval - GameManager.GMinstance.perfectInterval) / 9)) / 10;


            GameManager.GMinstance.GoodScored(goodPercent);
            GameManager.GMinstance.FeverUp();
            GameManager.GMinstance.ShieldUp();
            Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
        }
        // Note����MISS����ʱ
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
            gameObject.SetActive(false);
            IfPeekLeave();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}

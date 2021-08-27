using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool CanBePressed;

    public KeyCode KeyToPress;
    private bool hasScored;

    // Start is called before the first frame update
    void Start()
    {
        hasScored = false;
    }

    // Update is called once per frame
    void Update()
    {
        //��������˶�Ӧ����
        if(Input.GetKeyDown(KeyToPress))
        {
            //�����ʱ���԰�����trigger�
            if(CanBePressed)
            {
                //�ù��˷֣������÷��¼����ĳ�δ����
                hasScored = true;
                GameManager.GMinstance.NoteHit();
                gameObject.SetActive(false);
               
                //Ϊɶ��ֱ�������أ�
                //Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //���ˣ����������ж��ߣ����Կ��԰���
        if(other.tag == "Activator")
        {
            CanBePressed = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        //���ˣ����Ѿ������ж��߻�û���÷֣�����MISS��
        if (other.tag == "Activator" && hasScored == false)
        {
            CanBePressed = false;
            GameManager.GMinstance.NoteMissed();
        }
    }
}

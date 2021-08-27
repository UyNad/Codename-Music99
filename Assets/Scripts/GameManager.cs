using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //BGM����
    public AudioSource BGM;
    
    //�о���boolûɶƨ�ã���ɵ�
    public bool startPlaying;

    // �޸��˼ҵ�bool��֪ͨ��ʼ��
    public BeatScroller theBS;

    // ���ܡ����ܲۺ�Ѫ���ĳ�ʼ��
    public int shieldcount;
    public int shieldThresholds;
    public int healthcount;
    
    //����һ���UItext
    public Text scoreText;
    public Text feverText;
    public Text shieldText;
    public Text healthText;
    public GameObject failedText;

    //��̬��������function������
    public static GameManager GMinstance;

    //���������ĳ�ʼ��
    public int playerScore;

    public int perfectScore;

    //fever�����ĳ�ʼ��
    public int playerFever;
    public int feverGauge;
    public int perfectFever;
    public int[] feverThresholds;

    // Start is called before the first frame update
    void Start()
    {
        //ָ����̬����
        GMinstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //�����Լ��ǲ�������
        if (healthcount <= 0)
        {
            GameOver();
        }

        //���������ʼ���о��������е���࣬������ɵ�һ��
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                BGM.Play();
            }
        }
    }

    //������
    public void NoteHit()
    {
        //����һϵ�е÷��¼����պ�Ҫ��չ�ɶ���ٷֱ�
        Debug.Log("Perfect");
        playerScore += perfectScore;
        scoreText.text = "Score: " + playerScore;

        feverGauge += perfectFever;
        //�ȿ���fever�Ƿ�������
        if (playerFever <= feverThresholds.Length)
        {
            //�ٿ���fever���Ƿ�����
            if (feverGauge >= feverThresholds[playerFever - 1])
            {
                feverGauge = 0;
                playerFever++;
                feverText.text = "Fever: " + playerFever;
            }

        }
    }

    //��miss��
    public void NoteMissed()
    {
        Debug.Log("Miss");


        //�ۻ��ף�û���׾Ϳ�Ѫ
        if (shieldcount > 0)
        {
            shieldcount--;
            shieldText.text = "shield: " + shieldcount;
        }
        else
        {
            healthcount--;
            healthText.text = "health: " + healthcount;
        }
    }


    //�����ˣ������߼���ûд��
    void GameOver()
    {
        Time.timeScale = 0;
        BGM.Stop();
        failedText.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ������Ҫ�Ż������У��Ӷ�����remix
    [Tooltip("ʹ��BGM")]
    public AudioSource BGM;
    
    // �о���bool����û���ˣ���ɵ�
    public bool startPlaying;

    [Tooltip("���õ���������������")]
    public BeatScroller theBS;

    // ���ܡ����ܲۺ�Ѫ���ĳ�ʼ��
    [Tooltip("��ʼ��������")]
    public int shieldcount;
    [Tooltip("��󻤶�����")]
    public int shieldcountNax;
    [Tooltip("��ҵĻ��ܲۻ���ֵ")]
    public int shieldGauge;
    [Tooltip("���ܲ۵���ֵ")]
    public int shieldThresholds;
    [Tooltip("100%ʱ�����Ļ���ֵ")]
    public int perfectShield;
    [Tooltip("��100%ʱ�����Ļ���ֵ")]
    public int goodShield;
    [Tooltip("��ʼѪ��")]
    public int healthcount;

    // ����һ���UI
    [Tooltip("���õĵ÷�UI")]
    public Text scoreText;
    [Tooltip("���õ�Fever UI")]
    public Text feverText;
    [Tooltip("���õĻ���UI")]
    public Text shieldText;
    [Tooltip("���õ�Ѫ��UI")]
    public Text healthText;
    [Tooltip("���õ�ʧ��UI")]
    public GameObject failedText;

    // ��̬��������function������
    public static GameManager GMinstance;

    // ���������ĳ�ʼ��
    [Tooltip("��ҳ�ʼ����")]
    public int playerScore;
    [Tooltip("100%�ĵ÷ַ���")]
    public int perfectScore;
    [Tooltip("��100%�ĵ÷ַ���")]
    public int fineScore;

    // fever�����ĳ�ʼ��
    [Tooltip("��ҵĳ�ʼFever�ȼ�")]
    public int playerFever;
    [Tooltip("��ҵ�Fever�ۻ���ֵ")]
    public int feverGauge;
    [Tooltip("100%��Fever����ֵ")]
    public int perfectFever;
    [Tooltip("��100%��Fever����ֵ")]
    public int fineFever;
    [Tooltip("Fever�۵���ֵ�����飬��Ӧ����ȼ�")]
    public int[] feverThresholds;

    //�ж�����
    [Tooltip("100%�ж��ķ�Χ")]
    public float perfectInterval;
    [Tooltip("10%~90%�ж��ķ�Χ")]
    public float goodInterval;
    [Tooltip("MISS�ж��ķ�Χ")]
    public float missInterval;

    // Start is called before the first frame update
    void Awake()
    {
        //ָ����̬����
        GMinstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // �����Լ��ǲ�������
        if (healthcount <= 0)
        {
            GameOver();
        }

        // ���������ʼ���о��������е���࣬������ɵ�һ��
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

    // �ж��Ƿ�����fever
    public void FeverUp()
    {
        //�Ƿ񵽴����fever����
        if (playerFever <= feverThresholds.Length)
        {
            //fever���Ƿ񵽴���ֵ
            if (feverGauge >= feverThresholds[playerFever - 1])
            {
                feverGauge = 0;
                playerFever++;
                feverText.text = "Fever: " + playerFever;
            }
        }
    }

    // �ж��Ƿ���������
    public void ShieldUp()
    {
        //�Ƿ񵽴���󻤶���
        if (shieldcount <= shieldcountNax)
        {
            //���ܲ��Ƿ񵽴���ֵ
            if (shieldGauge >= shieldThresholds)
            {
                shieldGauge = 0;
                shieldcount++;
                shieldText.text = "Shield: " + shieldcount;
            }
        }
    }

    // �����ж���100%���ĵ÷��¼�
    public void PerfectScored()
    {
        // �ӷ֣�Ȼ�����UI
        Debug.Log("Perfect");
        playerScore += perfectScore;
        scoreText.text = "Score: " + playerScore;

        // ��feverֵ
        feverGauge += perfectFever;

        // �ӻ���ֵ
        shieldGauge += perfectShield;

    }

    // �������ж���10%~90%���ĵ÷��¼�
    // ���ν�ȥ��goodpercent����㼸����NoteObject����
    public void GoodScored(float goodPercent)
    {
        // �ӷ֣�Ȼ�����UI
        Debug.Log("Fine");
        playerScore += (int)(fineScore * goodPercent);
        scoreText.text = "Score: " + playerScore;

        // ��feverֵ
        feverGauge += (int)(fineFever * goodPercent);

        // �ӻ���ֵ
        shieldGauge += (int)(goodShield * goodPercent);
    }

    // ��miss��
    // �����ж�������Ӧ�Ŀ۷��¼�
    // Ŀǰ�ǽ��۷��¼�ֱ��д������
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


    // �����߼�
    void GameOver()
    {
        Time.timeScale = 0;
        BGM.Stop();
        failedText.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 后续需要优化成数列，从而进行remix
    [Tooltip("使用BGM")]
    public AudioSource BGM;
    
    // 感觉此bool现在没用了，想干掉
    public bool startPlaying;

    [Tooltip("引用的音符滚动控制器")]
    public BeatScroller theBS;

    // 护盾、护盾槽和血量的初始化
    [Tooltip("初始护盾数量")]
    public int shieldcount;
    [Tooltip("最大护盾数量")]
    public int shieldcountNax;
    [Tooltip("玩家的护盾槽积累值")]
    public int shieldGauge;
    [Tooltip("护盾槽的阈值")]
    public int shieldThresholds;
    [Tooltip("100%时增长的护盾值")]
    public int perfectShield;
    [Tooltip("非100%时增长的护盾值")]
    public int goodShield;
    [Tooltip("初始血量")]
    public int healthcount;

    // 引用一万个UI
    [Tooltip("引用的得分UI")]
    public Text scoreText;
    [Tooltip("引用的Fever UI")]
    public Text feverText;
    [Tooltip("引用的护盾UI")]
    public Text shieldText;
    [Tooltip("引用的血条UI")]
    public Text healthText;
    [Tooltip("引用的失败UI")]
    public GameObject failedText;

    // 静态化，方便function被调用
    public static GameManager GMinstance;

    // 分数变量的初始化
    [Tooltip("玩家初始分数")]
    public int playerScore;
    [Tooltip("100%的得分分数")]
    public int perfectScore;
    [Tooltip("非100%的得分分数")]
    public int fineScore;

    // fever变量的初始化
    [Tooltip("玩家的初始Fever等级")]
    public int playerFever;
    [Tooltip("玩家的Fever槽积累值")]
    public int feverGauge;
    [Tooltip("100%的Fever积累值")]
    public int perfectFever;
    [Tooltip("非100%的Fever积累值")]
    public int fineFever;
    [Tooltip("Fever槽的阈值，数组，对应多个等级")]
    public int[] feverThresholds;

    //判定区间
    [Tooltip("100%判定的范围")]
    public float perfectInterval;
    [Tooltip("10%~90%判定的范围")]
    public float goodInterval;
    [Tooltip("MISS判定的范围")]
    public float missInterval;

    // Start is called before the first frame update
    void Awake()
    {
        //指定静态变量
        GMinstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // 看看自己是不是死了
        if (healthcount <= 0)
        {
            GameOver();
        }

        // 按任意键后开始，感觉俩变量有点多余，所以想干掉一个
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

    // 判断是否升级fever
    public void FeverUp()
    {
        //是否到达最大fever级别
        if (playerFever <= feverThresholds.Length)
        {
            //fever槽是否到达阈值
            if (feverGauge >= feverThresholds[playerFever - 1])
            {
                feverGauge = 0;
                playerFever++;
                feverText.text = "Fever: " + playerFever;
            }
        }
    }

    // 判断是否升级护盾
    public void ShieldUp()
    {
        //是否到达最大护盾数
        if (shieldcount <= shieldcountNax)
        {
            //护盾槽是否到达阈值
            if (shieldGauge >= shieldThresholds)
            {
                shieldGauge = 0;
                shieldcount++;
                shieldText.text = "Shield: " + shieldcount;
            }
        }
    }

    // 完美判定（100%）的得分事件
    public void PerfectScored()
    {
        // 加分，然后更新UI
        Debug.Log("Perfect");
        playerScore += perfectScore;
        scoreText.text = "Score: " + playerScore;

        // 加fever值
        feverGauge += perfectFever;

        // 加护盾值
        shieldGauge += perfectShield;

    }

    // 非完美判定（10%~90%）的得分事件
    // 传参进去，goodpercent（零点几）在NoteObject里算
    public void GoodScored(float goodPercent)
    {
        // 加分，然后更新UI
        Debug.Log("Fine");
        playerScore += (int)(fineScore * goodPercent);
        scoreText.text = "Score: " + playerScore;

        // 加fever值
        feverGauge += (int)(fineFever * goodPercent);

        // 加护盾值
        shieldGauge += (int)(goodShield * goodPercent);
    }

    // 您miss了
    // 根据判定触发对应的扣分事件
    // 目前是将扣分事件直接写在里面
    public void NoteMissed()
    {
        Debug.Log("Miss");


        //扣护甲，没护甲就扣血
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


    // 死亡逻辑
    void GameOver()
    {
        Time.timeScale = 0;
        BGM.Stop();
        failedText.SetActive(true);
    }

}

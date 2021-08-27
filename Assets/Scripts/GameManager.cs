using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //BGM引用
    public AudioSource BGM;
    
    //感觉此bool没啥屁用，想干掉
    public bool startPlaying;

    // 修改人家的bool，通知开始了
    public BeatScroller theBS;

    // 护盾、护盾槽和血量的初始化
    public int shieldcount;
    public int shieldThresholds;
    public int healthcount;
    
    //引用一万个UItext
    public Text scoreText;
    public Text feverText;
    public Text shieldText;
    public Text healthText;
    public GameObject failedText;

    //静态化，方便function被调用
    public static GameManager GMinstance;

    //分数变量的初始化
    public int playerScore;

    public int perfectScore;

    //fever变量的初始化
    public int playerFever;
    public int feverGauge;
    public int perfectFever;
    public int[] feverThresholds;

    // Start is called before the first frame update
    void Start()
    {
        //指定静态变量
        GMinstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //看看自己是不是死了
        if (healthcount <= 0)
        {
            GameOver();
        }

        //按任意键后开始，感觉俩变量有点多余，所以想干掉一个
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

    //您中了
    public void NoteHit()
    {
        //触发一系列得分事件，日后要拓展成多个百分比
        Debug.Log("Perfect");
        playerScore += perfectScore;
        scoreText.text = "Score: " + playerScore;

        feverGauge += perfectFever;
        //先看看fever是否还能升级
        if (playerFever <= feverThresholds.Length)
        {
            //再看看fever槽是否满了
            if (feverGauge >= feverThresholds[playerFever - 1])
            {
                feverGauge = 0;
                playerFever++;
                feverText.text = "Fever: " + playerFever;
            }

        }
    }

    //您miss了
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


    //您死了（死亡逻辑还没写）
    void GameOver()
    {
        Time.timeScale = 0;
        BGM.Stop();
        failedText.SetActive(true);
    }

}

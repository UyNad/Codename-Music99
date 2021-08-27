using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        //bpm是一分钟多少beat，除以60就是一分钟几拍；需要和曲子的bpm保持一致
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            /*if(Input.anyKeyDown)
            {
                hasStarted = true;
            } */
        }

        else
        {
            //这个说实话没大看懂，一秒移动beat的y轴位移，为啥就是bpm了
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}

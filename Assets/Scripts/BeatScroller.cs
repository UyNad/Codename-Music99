using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Tooltip("BPM除以60，一分钟几个拍，决定滚动速度")]
    public float beatTempo;

    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        // bpm是一分钟多少beat，除以60就是一分钟几拍；需要和曲子的bpm保持一致
        // beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStarted)
        {

            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}

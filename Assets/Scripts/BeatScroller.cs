using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Tooltip("BPM����60��һ���Ӽ����ģ����������ٶ�")]
    public float beatTempo;

    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        // bpm��һ���Ӷ���beat������60����һ���Ӽ��ģ���Ҫ�����ӵ�bpm����һ��
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

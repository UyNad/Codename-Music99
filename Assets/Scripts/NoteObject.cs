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
        //如果触发了对应按键
        if(Input.GetKeyDown(KeyToPress))
        {
            //如果此时可以按（在trigger里）
            if(CanBePressed)
            {
                //得过了分，触发得分事件，改成未激活
                hasScored = true;
                GameManager.GMinstance.NoteHit();
                gameObject.SetActive(false);
               
                //为啥不直接销毁呢？
                //Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //碰了，且碰到了判定线，所以可以按了
        if(other.tag == "Activator")
        {
            CanBePressed = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        //碰了，且已经除了判定线还没被得分，所以MISS了
        if (other.tag == "Activator" && hasScored == false)
        {
            CanBePressed = false;
            GameManager.GMinstance.NoteMissed();
        }
    }
}

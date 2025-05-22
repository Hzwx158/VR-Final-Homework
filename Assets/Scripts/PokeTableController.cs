using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokeTableController : MonoBehaviour 
{
    public Text IdText;
    private string defaultString;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * 所有继承MonoBehaviour的都可以挂在物品上
         * 首先先创这个脚本
         * 然后把这个脚本拖到Table上（挂在Table上）
         * 然后创建这个Text控件，去Editor左侧找到要的Text控件，拖到右侧Table脚本显示的成员处，这就算挂上了。
         * 在Editor里给圆环加个tag
         * 然后写一下响应事件就可以了
        */
        defaultString = IdText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("torus"))
        {
            IdText.text = "Student ID: 522030910171";
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("torus")){
            IdText.text = defaultString;
        }
    }
}

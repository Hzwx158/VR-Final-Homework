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
         * ���м̳�MonoBehaviour�Ķ����Թ�����Ʒ��
         * �����ȴ�����ű�
         * Ȼ�������ű��ϵ�Table�ϣ�����Table�ϣ�
         * Ȼ�󴴽����Text�ؼ���ȥEditor����ҵ�Ҫ��Text�ؼ����ϵ��Ҳ�Table�ű���ʾ�ĳ�Ա�������������ˡ�
         * ��Editor���Բ���Ӹ�tag
         * Ȼ��дһ����Ӧ�¼��Ϳ�����
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

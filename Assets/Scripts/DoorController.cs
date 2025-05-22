using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float Speed;
    public float HoldTime;
    public float MaxHeight;
    public float MinHeight;
    
    private float holdTimer;
    private bool isOpening;


    // Start is called before the first frame update
    void Start()
    {
        isOpening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            if (transform.position.y < MaxHeight)
            {
                Vector3 pos = transform.position;
                pos.y += Speed;
                transform.position = pos;
            }
            else
            {
                isOpening = false;
                holdTimer = HoldTime;
            }
        }
        else if (holdTimer <= 0 && transform.position.y > MinHeight)
        {
            Vector3 pos = transform.position;
            pos.y -= Speed;
            transform.position = pos;
        }

        if (holdTimer > 0)
        {
            holdTimer -= Time.deltaTime;
        }
    }
    public void Open()
    {
        isOpening = true;
        holdTimer = 0;
    }

}

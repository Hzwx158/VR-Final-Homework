using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyoController : MonoBehaviour
{
    private Animator animator;
    public float Theta;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(animator.transform.position);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.ToLower().Contains("wall"))
        {
            MoveStop();
        }
        Debug.Log("stop!!!");
    }
    
    public void MoveStraight()
    {
        animator.SetBool("ClickToMove", true);
    }

    public void MoveLeft()
    {
        animator.transform.Rotate(new Vector3(0, -Theta, 0));
    }
    public void MoveRight()
    {
        animator.transform.Rotate(new Vector3(0, Theta, 0));
    }
    public void MoveStop()
    {
        animator.SetBool("ClickToMove", false);
        animator.Play("mixamo_com", -1, float.NegativeInfinity);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoyoController : MonoBehaviour
{
    private Animator animator;
    public float SpinSpeed;
    public float MoveSpeed;
    private bool canMove;
    private float spinSpeed;
    public GameObject[] SoyoCtrl;
    private Vector3 lastPos;
    private bool recordPos = false;
    public DoorController Door;
    private AudioSource audioSource; // 需要一个比较短的音频
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canMove = true;
        spinSpeed = 0;
        SoyoCtrl[0].GetComponent<XRGrabInteractable>().selectEntered.AddListener(MoveStraight);
        SoyoCtrl[1].GetComponent<XRGrabInteractable>().selectEntered.AddListener(MoveLeft);
        SoyoCtrl[2].GetComponent<XRGrabInteractable>().selectEntered.AddListener(MoveRight);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spinSpeed!=0)
        {
            transform.Rotate(0, spinSpeed*Time.deltaTime, 0);
        }
        foreach (GameObject go in SoyoCtrl)
        {
            if (go.transform.position[1] <= 0){
                var pos = go.transform.position;
                go.transform.position = new Vector3(pos[0], 0.1f, pos[2]);
            }
        }
        if(recordPos)
        {
            lastPos = transform.position;
        }
        recordPos = !recordPos;
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger with Wall={other.gameObject.name.ToLower().Contains("wall")}!!!");
        if (other.gameObject.name.ToLower().Contains("wall"))
        {
            canMove = false;
            MoveStop();
            var wallPos = other.gameObject.transform.position;
            float deltaX = lastPos.x - wallPos.x;
            float deltaZ = lastPos.z - wallPos.z;
            transform.position = new Vector3(
                //wallPos[0] ,
                lastPos.x + Math.Sign(deltaX) * 0.5f,
                transform.position.y, 
                //wallPos[2] 
                lastPos.z + Math.Sign(deltaZ) * 0.5f
            );
        }
        else if (other.gameObject.name == "DoorStage2")
        {
            // 胜利
            Debug.Log("Victory");
            MoveStop();
            Door.Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Can Move");
        canMove = true;
    }

    public void MoveStraight(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject as XRBaseInteractor;
        Debug.Log(interactor.gameObject.name);
        if (!interactor.gameObject.CompareTag("left"))
            return;
        if (!canMove)
        {
            return; 
        }
        Debug.Log("call MoveStraight");
        animator.speed = MoveSpeed;
        animator.SetBool("ClickToMove", true);
        //audioSource.Play(); //TODO
    }

    public void MoveLeft(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject as XRBaseInteractor;
        Debug.Log(interactor.gameObject.name);
        if (!interactor.gameObject.CompareTag("left"))
            return;
        Debug.Log("call MoveLeft");
        spinSpeed = -SpinSpeed;
    }
    public void MoveRight(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject as XRBaseInteractor;
        Debug.Log(interactor.gameObject.name);
        if (!interactor.gameObject.CompareTag("left"))
            return;
        Debug.Log("call MoveRight");
        spinSpeed = SpinSpeed;
    }
    public void MoveStop()
    {
        spinSpeed = 0;
        Debug.Log("call MoveStop");
        animator.SetBool("ClickToMove", false);
        animator.speed = 0;
    }
    public void SelectSummonRing()
    {
        var ringPos = SoyoCtrl[3].transform.position;
        SoyoCtrl[0].transform.position = new Vector3(ringPos[0], ringPos[1]+0.5f, ringPos[2]);
        SoyoCtrl[1].transform.position = new Vector3(ringPos[0], ringPos[1]+0.5f, ringPos[2] - 0.5f);
        SoyoCtrl[2].transform.position = new Vector3(ringPos[0], ringPos[1]+0.5f, ringPos[2] + 0.5f);
    }
}

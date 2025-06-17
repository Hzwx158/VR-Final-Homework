using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoyoController : MonoBehaviour
{
    [Header("可移动墙")]
    public List<WallMover> Walls;
    [Header("移动参数")]
    public float SpinSpeed;
    public float MoveSpeed;
    [Header("环")]
    public GameObject[] SoyoCtrl;
    [Header("门")]
    public DoorController Door;

    private Animator animator;
    private bool canMove;
    private float spinSpeed;
    private Vector3 lastPos;
    private bool recordPos = false;
    private AudioSource audioSource; // 需要一个比较短的音频
    private bool has_victory;
    private float movedCount; 
    // Start is called before the first frame update
    void Start()
    {
        movedCount = 0;
        animator = GetComponent<Animator>();
        animator.speed = 0;
        has_victory = false;
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
        if(animator.speed !=0 || spinSpeed != 0)
        {
            //Debug.Log($"speed: {animator.speed}, spin: {spinSpeed}");
            movedCount += Time.deltaTime;
        }
        if(movedCount >= 2)
        {
            var rng = new System.Random();
            int idx = rng.Next(Walls.Count);
            if (Walls[idx].moveDistance!=0)
            {
                Walls[idx].ToggleMove();
            }
            else 
            { 
                Walls[idx].ToggleRotation();
            }
            movedCount = 0;
        }
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
        if (canMove)
        {
            if (recordPos)
            {
                lastPos = transform.position;
            }
            recordPos = !recordPos;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger with Wall={other.gameObject.name.ToLower().Contains("wall")}!!!");
        if (other.gameObject.name.ToLower().Contains("wall"))
        {
            canMove = false;
            MoveStop();
            var wallPos = other.ClosestPoint(transform.position);
            var direction = transform.position - wallPos;

            transform.position = wallPos + 3 * direction;
        }
        else if (other.gameObject.name == "DoorStage2" && (!has_victory))
        {
            // 胜利
            Debug.Log("Victory");
            MoveStop();
            Door.Open();
            audioSource.Play();
            has_victory = true;
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Game Over", "Finished", "Exit");
            System.Threading.Thread.Sleep(1000);
            UnityEditor.EditorApplication.isPlaying = false;
#else
            System.Threading.Thread.Sleep(1000);
            Application.Quit();
#endif
            canMove = false;
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

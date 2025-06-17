using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up;
    public float rotationAngle = 90f;
    public float rotationSpeed = 90f; // ��/��

    [Header("Translation Settings")]
    public Vector3 moveDirection = Vector3.forward;
    public float moveDistance = 2f;
    public float moveSpeed = 2f; // ��λ/��

    private enum MotionState { Idle, Rotating, Moving }
    private MotionState currentState = MotionState.Idle;

    // ��תĿ��״̬
    private float rotatedAngle;
    private bool isRotated = false;

    // �ƶ�Ŀ��״̬
    private Vector3 targetPosition;
    private bool isMoved = false;

    void Start()
    {
        
    }
    void Update()
    {
        switch (currentState)
        {
            case MotionState.Rotating:
                UpdateRotation();
                break;
            case MotionState.Moving:
                UpdateMovement();
                break;
        }
    }

    // �ⲿ���ã��л���ת״̬
    public void ToggleRotation()
    {
        Debug.Log("ToggleRotation");
        if (currentState != MotionState.Idle) return;

        rotatedAngle = 0;

        isRotated = !isRotated;
        currentState = MotionState.Rotating;
    }

    // �ⲿ���ã��л��ƶ�״̬
    public void ToggleMove()
    {
        Debug.Log($"ToggleMove");
        if (currentState != MotionState.Idle) return;

        targetPosition = transform.position + moveDirection.normalized *
            (isMoved ? -moveDistance : moveDistance);

        isMoved = !isMoved;
        currentState = MotionState.Moving;
    }

    private void UpdateRotation()
    {
        rotatedAngle += rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, (isRotated?-1:1) * rotationSpeed * Time.deltaTime);
        
        if (Math.Abs(rotationAngle - rotatedAngle) < 2f)
        {
            transform.Rotate(rotationAxis, (isRotated ? -1 : 1) * (rotationAngle - rotatedAngle));
            rotatedAngle = rotationAngle;
            currentState = MotionState.Idle;
        }
    }

    private void UpdateMovement()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            currentState = MotionState.Idle;
        }
    }
}

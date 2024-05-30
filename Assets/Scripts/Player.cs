using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private ClearCounter selectedCounter;

    private float rotateSpeed;
    float playerHeight = 2f;
    [SerializeField] private float playerRadius = .01f;
    public bool isWalking;
    private Vector3 lastInteractDir;

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
       
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,
                countersLayerMask))

        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has ClearCounter
                selectedCounter = clearCounter;
            }
            else
            {
                selectedCounter = null;
            }
        }
        else
        {
            selectedCounter = null;
        }
    }


    private void HandleMovement()

    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMoveX, canMoveZ;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //cannot move through moveDir

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight,
                playerRadius, moveDirX, moveDistance);

            if (canMoveX)
            {
                //Can move only on the X

                moveDir = moveDirX;
            }

            else

            {
                //Can move only on the X
                //Atempt only Z movement

                Vector3 moveDirZ = new Vector3(0, -0, moveDir.z).normalized;
                canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight,
                    playerRadius, moveDirZ, moveDistance);

                if (canMoveZ)
                    //can move only on the Z
                {
                    moveDir = moveDirZ;
                }


                else
                {
                    //cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        float rotateSpeed = 15f;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
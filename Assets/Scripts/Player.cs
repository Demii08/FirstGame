using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput _gameInput;
    private float rotateSpeed; float playerHeight = 4f; float playerRadius = .35f;
    public bool isWalking;

    private void Update()
    {
        /*
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

       

        

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 4f;
        bool canMoveX, canMoveZ;



        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight,  playerRadius, moveDir, moveDistance);

        if (!canMove) 
        {
            //cannot move through moveDir

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMoveX)
            {
                //Can move only on the X

                moveDir = moveDirX;

                Debug.Log("FirstLogX");
            } 

            else

            {
                //Can move only on the X
                //Atempt only Z movement

                Vector3 moveDirZ = new Vector3(0, -0, moveDir.z).normalized;
                canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMoveZ)
                    //can move only on the Z
                {
                    moveDir = moveDirZ;
                    Debug.Log("FirstLogZ");
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

        isWalking = (moveDir != Vector3.zero);
        transform.position += moveDir * (moveSpeed * Time.deltaTime);

        float rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        */
        HandleMovement();
    }

    private void HandleMovement()
    {
        var inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir * rotateSpeed, Time.deltaTime * rotateSpeed);
        float moveDistance = Time.deltaTime * moveSpeed;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight,
            playerRadius, moveDir, moveDistance);
        if (canMove == false)
        {
            // cant move diagonally 
            Vector3 xDirection = new Vector3(moveDir.x, 0, 0);
            // can move only on x ? 
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.one * playerHeight,
                playerRadius, xDirection, moveDistance);
            if (canMove)
            {
                moveDir = xDirection;
            }
            //if can't move on x try on z 
            else
            {
                Vector3 zDirection = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.one * playerHeight,
                    playerRadius, zDirection, moveDistance);
                //can move on z ?
                if (canMove)
                {
                    moveDir = zDirection;
                }
            }
        }

        var step = moveDir * moveDistance;
        if (canMove)
        {
            transform.position += step;
        }
    }

}
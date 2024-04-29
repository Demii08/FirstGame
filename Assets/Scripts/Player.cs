using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput _gameInput;
    private float rotateSpeed;

    public bool isWalking;

    private void Update()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

        

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = (moveDir != Vector3.zero);
        transform.position += moveDir * (moveSpeed * Time.deltaTime);

        float rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        
    }

    
    
}
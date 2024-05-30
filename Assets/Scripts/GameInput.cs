using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour

{
   private PlayerInputActions _playerInputActions;
   public event EventHandler OnInteractAction; 

   private void Awake()
   {
      _playerInputActions = new PlayerInputActions();
      
      _playerInputActions.Player.Enable();

      //_playerInputActions.Player.
      

   }

   public Vector2 GetMovementVectorNormalized()
   {
      
      Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
       
        inputVector = inputVector.normalized; 
        
        return inputVector;
   }
}
   
  
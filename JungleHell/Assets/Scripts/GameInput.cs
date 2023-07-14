using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;
    public event EventHandler OnPauseAction;

    private void Awake () 
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Pause.performed += PausePerformed;
        }
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    } 

    // Update is called once per frame
    public Vector2 GetInputVector()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetInputVectorLegacy() 
    {
        var inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = 1;
        }
        return inputVector;
    }

    public bool IsShooting () 
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}

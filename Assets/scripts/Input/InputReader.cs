using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;


[CreateAssetMenu(fileName = "new input reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions {

    public event Action<bool> primaryFireEvent;
    public event Action<Vector2> playerMoveEvent;
    public Vector2 aimPosition { get; private set; }

    private Controls controls;
    private void OnEnable() {
        if (controls == null) {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context) {
        playerMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context) {
        if (context.performed) {
            primaryFireEvent?.Invoke(true);
        }else if (context.canceled) {
            primaryFireEvent?.Invoke(false);
        }
        
    }

    public void OnAim(InputAction.CallbackContext context) {
        aimPosition = context.ReadValue<Vector2>();
    }
}

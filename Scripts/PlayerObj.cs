using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private Controls controls;
    Vector2 mousePos = Vector2.zero;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        
        //controls.UIInteract.Up.started += _ => { isMovingUp = true; };

        //playerControls.Player.Up.canceled += _ => { isMovingUp = false; };
    }

    private void Update()
    {
    }
}

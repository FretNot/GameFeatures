using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidBody;
    private PlayerInputActions playerInputActions;
    public float speed;
    private float defaultSpeed = .2f;
    public bool sprinting;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = defaultSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = playerInputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveVec.x, 0, moveVec.y) * speed);
        if (GetComponent<HealingRift>().placingRift == true)
        {
            speed = 0;
        }
        if (GetComponent<HealingRift>().placingRift != true)
        {
            speed = defaultSpeed;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            rigidBody.AddForce(Vector3.up * speed, ForceMode.Impulse);
            Debug.Log("Jump!" + context.phase);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        while(GetComponent<HealingRift>().placingRift != true)
        {
            Vector2 moveVec = context.ReadValue<Vector2>();
            transform.Translate(new Vector3(moveVec.x, 0, moveVec.y) * speed);
        }
            
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = defaultSpeed + .2f;
        }
    }
}

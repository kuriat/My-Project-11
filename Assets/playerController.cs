using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Movement Variables")]
    public const float moveSpeed = 2f;
    public float rollSpeed;
    private Vector3 MoveDir;
    private Vector3 rollDir;

    // state machine useed for movement
    private enum State{
        Normal,
        Rolling,
    }
    private State state;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Normal:
            break;
            case State.Rolling:
            float rollSpeedDropMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                float min = 3f;
                if(rollSpeed < min){
                    state = State.Normal;
                }
            break;
        }
    }
    
    void FixedUpdate()
    {
        switch(state){
            case State.Normal:
                    rb.velocity = MoveDir * moveSpeed;
                break;
            case State.Rolling:
                    rb.velocity = rollDir*rollSpeed;
                break;
        }
    }

    public void Move(InputAction.CallbackContext ctx){
        MoveDir = ctx.ReadValue<Vector2>().normalized;
    }
     public void Rolling(InputAction.CallbackContext ctx){
        state = State.Rolling; 
        rollDir = MoveDir;
        rollSpeed = 10f;
    }
}

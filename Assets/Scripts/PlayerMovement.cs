using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 _move;
    Vector3 moveDir;
    [SerializeField] Transform orientation;

    [SerializeField] float playerHeight = 1f;
    [SerializeField] LayerMask WhatIsGround;
    bool grounded;
    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airControl;
    bool readyToJump = true;

    Rigidbody rb;
    Animator animCon;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animCon = GetComponentInChildren<Animator>();
    }

    void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f + 0.2f, WhatIsGround);

        if(grounded){
            rb.drag = groundDrag;
            animCon.SetBool("Grounded", true);
        } else{
            rb.drag = 0f;
            animCon.SetBool("Grounded", false);
        }
    }

    void FixedUpdate() {
        movePlayer();
        SpeedControl();
    }

    void movePlayer() {
        moveDir = orientation.forward * _move.y + orientation.right * _move.x;

        if(moveDir == Vector3.zero){
            animCon.SetBool("Walking", false);
        } else{
            animCon.SetBool("Walking", true);
        }

        if(grounded){
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
        } else if(!grounded){
            rb.AddForce(moveDir.normalized * moveSpeed * airControl, ForceMode.Force);
            
        }
    }

    void jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump() {
        readyToJump = true;
    }

    void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void OnMove(InputValue moveInput) {
        _move = moveInput.Get<Vector2>();
    }

    void OnJump() {
        
        if(grounded && readyToJump){
            readyToJump = false;
            jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
}

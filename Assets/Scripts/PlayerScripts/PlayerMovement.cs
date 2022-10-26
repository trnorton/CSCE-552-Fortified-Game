using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float groundDrag;
    
    //Jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;
    public KeyCode jumpKey = KeyCode.Space;

    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        MyInput();
        ControlSpeed();

        //Handle the Drag
        if(grounded) 
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
    }
    //Keyboard Inputs
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //When to jump
        if(Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();
            //We can continuously jump if holding space down
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    //Player Movement
    private void PlayerMove()
    {
        //Movement Direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //force
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    //Make sure we don't exceed speed limit
    private void ControlSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //Limit Speed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //Make so always jump the exact same height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        canJump = true;
    }
}

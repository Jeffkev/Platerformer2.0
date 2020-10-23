using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxspeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ground;


    private Inputs inputs;
    private Vector2 direction;
    private bool isOnGround = false;
    //this bool will check which direction the player's facing;
    private bool isFacingLeft = true;
    //this bool will check which direction the Player's facing;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private SpriteRenderer myRenderer;

    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.move.performed += OnMovePerformed;
        inputs.Player.move.canceled += OnMoveCanceled;
        inputs.Player.jump.performed += JumpOnPerformed;

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();


    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        if(isOnGround)
        {
         myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;

    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
    }



    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Animator>().SetBool("Iswalking", true);
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        direction.y = 0;
        //myRigidBody.MovePosition(direction);
        //myRigidBody.velocity = direction;
        if (myRigidBody.velocity.sqrMagnitude < maxspeed)
            myRigidBody.AddForce(direction * speed);
        var iswalking = direction.x != 0;
        myAnimator.SetBool("iswalking", iswalking);
        if (direction.x < 0)
        {
            myRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            myRenderer.flipX = false;
        }
    }   

      private void OnCollisionEnter2D(Collision2D other)
        {

            var touchGround = ground == (ground | (1 << other.gameObject.layer));
            var touchFromAbove = other.contacts[0].normal == Vector2.up;
            if  (touchGround && touchFromAbove) 
            // if (other.gameObject.CompareTag("ground") == true)
            {
                isOnGround = true;
            }
        }
        

        




    
}

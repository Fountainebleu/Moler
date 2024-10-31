using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using BasicActions;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedUp = 3;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private LayerMask groundLayer; //маска земли
    
    private GameObject playerObject;
    private Rigidbody2D rb2d;
    private Collider2D col2d;

    private bool underGround = false;
    bool isGrounded;
    private void Awake()
    {
        playerObject = gameObject; //получаем объект, к которому прикреплён объект
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (underGround)
        {
            UnderGroundMove();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            col2d.isTrigger = false;
            underGround = false;
            rb2d.gravityScale = 1;
        }
    }

    private void Update()
    {
        bool isGrounded = Controls.isGrounded(col2d, groundLayer);
        if (!underGround && Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            col2d.isTrigger = true;
            rb2d.gravityScale = 0;
            underGround = true;
            transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);

        }
        else
        {
            if (isGrounded)
            {
                Controls.Move(rb2d, Input.GetAxis("Horizontal"), Input.GetAxis("Debug Horizontal"), speed, Input.GetAxis("SpeedUp") * speedUp);
            }

            if(Controls.isGrounded(col2d, groundLayer) && Input.GetButtonDown("Jump"))
            {
                Controls.Jump(rb2d, jumpSpeed);
            }
        }
        Controls.WhereCharLook(playerObject, rb2d);
    }

    private void UnderGroundMove()
    {
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        
    }
}

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
    
    bool isGrounded;
    private void Awake()
    {
        playerObject = gameObject; //получаем объект, к которому прикреплён объект
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    
    private void Update()
    {
        bool isGrounded = Controls.isGrounded(col2d, groundLayer);
        Controls.Move(rb2d, Input.GetAxis("Horizontal"), speed, Input.GetAxis("SpeedUp") * speedUp);

        if(Controls.isGrounded(col2d, groundLayer) && Input.GetButtonDown("Jump"))
        {
            Controls.Jump(rb2d, jumpSpeed);
        }

        Controls.WhereCharLook(playerObject, rb2d);
    }
}

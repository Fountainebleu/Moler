using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicActions;

public class BotAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedUp = 3;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private float seeWallDistance;
    [SerializeField] private float directionOfMove = 1;
    [SerializeField] private LayerMask groundLayer; //маска земли
    
    private GameObject botObject;
    private Rigidbody2D rb2d;
    private Collider2D col2d;
    
    bool isGrounded;
    private void Awake()
    {
        botObject = gameObject; //получаем объект, к которому прикреплён объект
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Controls.Move(rb2d, directionOfMove, speed);

        if (IsWallNear(rb2d, seeWallDistance, directionOfMove))
         {
            directionOfMove *= -1;
         }
    }
    private bool IsWallNear(Rigidbody2D rb2d, float distance, float directionOfMove = 1)
    {
        RaycastHit2D raycastToWall = Physics2D.Raycast(rb2d.position, new Vector2(directionOfMove,0), distance, LayerMask.GetMask("Wall"));
        if (raycastToWall)
        {
            Debug.DrawRay(rb2d.position, new Vector2(seeWallDistance * directionOfMove, 0), Color.red, 0.5f, true);
            Debug.Log("Wall Decetced");
        }
        
        return raycastToWall.collider != null && raycastToWall.collider.tag != "player";
    }
}

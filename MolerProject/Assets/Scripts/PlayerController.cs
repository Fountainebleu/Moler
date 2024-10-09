using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move() //Метод управляющий движением и ускорением на кнопку shift
    {
        float speedFinal;

        if (Input.GetKey(KeyCode.LeftShift)) //Если нажата кнопка shift, то удвоит скорость
        {
            speedFinal = speed * 2;
        }

        else
        {
            speedFinal = speed;
        }
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedFinal, body.velocity.y);
    }

    private void Jump() //Метод прыжка
    {
        if (Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }
}

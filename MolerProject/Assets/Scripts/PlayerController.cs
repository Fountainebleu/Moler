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
        WhereCharLook();
        Dash();
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

    private int whereLook; //Показывает куда смотрит персонаж, если налево, то -1, если направо, то 1

    private void WhereCharLook() //Меняет направление взгляда персонажа
    {
        if ((Input.GetAxis("Horizontal")) > 0)
        {
            transform.localScale = new Vector2(0.7f, 0.7f);
            whereLook = 1;
        }
        
        if ((Input.GetAxis("Horizontal")) < 0)
        {
            transform.localScale = new Vector2(-0.7f, 0.7f);
            whereLook = -1;
        }
    }

    [SerializeField] private float dashMaxTime = 0.2f; //максимальное время между нажатиями 2 раз кнопки shift
    [SerializeField] private float dashPower; //мощность рывка

    private float dashTimer; //переменная для подсчёте времени с последнего нажатия клавиши shift
    private int shiftPressCount = 0;
    private bool isDashing; //показывает в моменте рывка ты или нет

    private void Dash() //метод позволяющий делать рывок, путём двойного нажатия на shift 
     {
        if (isDashing) //если в момента рывка, то не даёт его снова применить следующие 0,05 секунд(нужен, чтобы рывки не двоились)
        {
            Invoke("DashLock", 0.05f);
        }
        // Проверяем нажатие клавиши Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            shiftPressCount++;
            dashTimer = 0f; // Сбрасываем таймер после каждого нажатия Shift
        }

        // Запускаем таймер, если было хотя бы одно нажатие
        if (shiftPressCount > 0 && !isDashing)
        {
            dashTimer += Time.deltaTime;

            // Если нажали дважды и время между нажатиями меньше максимального
            if (shiftPressCount == 2 && dashTimer <= dashMaxTime)
            {
                isDashing = true;
                // Выполняем рывок
                if (whereLook == 1)
                {
                    body.AddForce(Vector2.right * dashPower, ForceMode2D.Impulse);
                    isDashing = true;
                }
                else
                {
                    body.AddForce(Vector2.left * dashPower, ForceMode2D.Impulse);
                    isDashing = true;
                }

                // Сбрасываем счетчик и таймер после выполнения рывка
                shiftPressCount = 0;
                dashTimer = 0f;
            }

            // Если время между нажатиями больше допустимого, сбрасываем счетчик
            if (dashTimer > dashMaxTime)
            {
                shiftPressCount = 0;
                dashTimer = 0f;
            }
        }
    }

    private void DashLock()
    {
        isDashing = false;
    }
}

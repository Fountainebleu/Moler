using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using BasicActions;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpSpeed; 
    [SerializeField] private float speedUp = 3f; 
    [SerializeField] private LayerMask boundaryLayer; 

    [SerializeField] private float maxJumpHoldTime = 1f; // Максимальное время удержания кнопки прыжка
    [SerializeField] private float maxJumpForce = 10f;   // Максимальная сила прыжка

    private Rigidbody2D rb2d;
    private Collider2D col2d;
    private Bounds currentBoundaryBounds;
    private Vector2 movement;

    public bool underGround = false;
    private bool isGrounded;
    private bool isChargingJump = false; // Флаг для зарядки прыжка
    private float jumpHoldTime = 0f;     // Время удержания кнопки прыжка

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = Controls.isGrounded(col2d, LayerMask.GetMask("Ground"));

        // Переход в режим "underGround"
        if (!underGround && Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            rb2d.gravityScale = 0;
            underGround = true;
            Controls.Move(rb2d, Input.GetAxis("Horizontal"), 0);
            transform.position += new Vector3(0, -1.5f, 0);
            Physics2D.SyncTransforms();
            UpdateBoundaryBounds(); // Определяем границы объекта
        }
        else if (underGround && Input.GetKey(KeyCode.F)) // Удерживаем кнопку для зарядки прыжка
        {
            isChargingJump = true;
            jumpHoldTime += Time.deltaTime; // Увеличиваем время удержания
            jumpHoldTime = Mathf.Min(jumpHoldTime, maxJumpHoldTime); // Ограничиваем максимальное время
        }
        else if (underGround && Input.GetKeyUp(KeyCode.F) && IsTouchingTopBoundary()) // Отпускаем кнопку для прыжка
        {
            // Выход из режима "underGround" с прыжком
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.gravityScale = 1;
            underGround = false;
            transform.position += new Vector3(0, 1.5f, 0);

            // Расчёт силы прыжка
            float jumpForce = Mathf.Lerp(0, maxJumpForce, jumpHoldTime / maxJumpHoldTime);

            // Применяем прыжок
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

            // Сброс состояния прыжка
            isChargingJump = false;
            jumpHoldTime = 0f;

            Physics2D.SyncTransforms();
            UpdateBoundaryBounds();
        }

        if (!underGround)
        {
            // Передвижение и прыжки на поверхности
            if (isGrounded)
            {
                Controls.Move(rb2d, Input.GetAxis("Horizontal"), Input.GetAxis("Debug Horizontal"), speed, Input.GetAxis("SpeedUp") * speedUp);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Controls.Jump(rb2d, jumpSpeed);
            }
        }
        else
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        // Определяем, куда смотрит персонаж
        Controls.WhereCharLook(gameObject, rb2d);
    }

    private void FixedUpdate()
    {
        if (underGround && movement != Vector2.zero)
        {
            // Перемещаем персонажа
            Vector2 newPosition = rb2d.position + movement * speed * Time.fixedDeltaTime;

            // Учитываем размер коллайдера игрока
            float playerHalfWidth = col2d.bounds.extents.x;
            float playerHalfHeight = col2d.bounds.extents.y;

            // Ограничиваем позицию игрока с учётом размеров
            if (currentBoundaryBounds.size != Vector3.zero)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, currentBoundaryBounds.min.x + playerHalfWidth, currentBoundaryBounds.max.x - playerHalfWidth);
                newPosition.y = Mathf.Clamp(newPosition.y, currentBoundaryBounds.min.y + playerHalfHeight, currentBoundaryBounds.max.y - playerHalfHeight);
            }

            rb2d.MovePosition(newPosition);
            Controls.WhereCharLook(gameObject, rb2d, movement);
        }
    }

    private void UpdateBoundaryBounds()
    {
        // Определяем, находится ли персонаж внутри объекта Boundary
        Collider2D boundaryCollider = Physics2D.OverlapArea(
            transform.position - Vector3.one * 0.1f,
            transform.position + Vector3.one * 0.1f,
            boundaryLayer
        );

        if (boundaryCollider != null)
        {
            currentBoundaryBounds = boundaryCollider.bounds;
            Debug.Log($"Boundary Bounds updated: {currentBoundaryBounds}");
        }
        else
        {
            currentBoundaryBounds = new Bounds(); // Сбрасываем границы, если Boundary не найден
            Debug.LogWarning("No Boundary detected around player!");
        }
    }

    private void OnDisable()
    {
        // Сбрасываем границы, если объект отключается
        currentBoundaryBounds = new Bounds();
    }

    private bool IsTouchingTopBoundary()
    {
        // Верхняя часть игрока
        float playerTopY = col2d.bounds.max.y;

        // Верхняя часть текущей границы
        float boundaryTopY = currentBoundaryBounds.max.y;

        // Учитываем небольшую погрешность для сравнения
        float tolerance = 0.05f;

        // Проверяем, что верх игрока находится в пределах погрешности от границы
        if (Mathf.Abs(playerTopY - boundaryTopY) <= tolerance)
        {
            return true;
        }

        return false;
    }
}


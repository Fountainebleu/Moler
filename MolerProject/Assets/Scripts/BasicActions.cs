using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicActions
{
    public class Controls
    { 
        //Метод управляющий движением
        public static void Move(Rigidbody2D rb2d, float whichWay, float speed) 
        {
            rb2d.velocity =new Vector2(whichWay * speed, rb2d.velocity.y);
        }

        //Метод управляющий движением и ускорением на кнопку shift
        public static void Move(Rigidbody2D rb2d, float whichWay, float speed, float speedUp) 
        {
            if (whichWay > 0 && rb2d.velocity.x > speed * (-1))
                rb2d.velocity = new Vector2(whichWay * (speed  + speedUp), rb2d.velocity.y);

            else if (whichWay < 0 && rb2d.velocity.x < speed)
                rb2d.velocity = new Vector2(whichWay * (speed + speedUp), rb2d.velocity.y);
        }

        //Метод прыжка(прыгать можно даже в воздухе)
        public static void Jump(Rigidbody2D rb2d, float jumpSpeed) 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }

        //Меняет направление взгляда персонажа
        public static void WhereCharLook(GameObject gObject, Rigidbody2D rb2d) 
        {
            var xsize = gObject.transform.localScale.x;
            var ysize = gObject.transform.localScale.y;
            if (rb2d.velocity.x > 0 && xsize < 0) //если скорость положительна, то персонаж смотрит направо
            {
                gObject.transform.localScale = new Vector2(-xsize, ysize);
            }
            else if (rb2d.velocity.x < 0 && xsize > 0) //если скорость отрицательная, то персонаж смотрит налево
            {
                gObject.transform.localScale = new Vector2(-xsize, ysize);
            }
        }

        
        //Проверяет нахождение персонажа на земле
        public static bool isGrounded(Collider2D col2d, LayerMask layer) 
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(col2d.bounds.center, col2d.bounds.size, 0, Vector2.down, 0.03f, layer);
            return raycastHit.collider != null;
        }
    }
}
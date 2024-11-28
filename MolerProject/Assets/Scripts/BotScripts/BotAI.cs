using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicActions;
using UnityEngine.EventSystems;
using System;
using BotAiMethods;

public class BotAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedUp = 3;
    [SerializeField] private float jumpSpeed;


    
    [SerializeField] private float rayDistanceToWall = 1;//модификатор длины лучей для обнаружения стен
    [SerializeField] private float rayDistToGndVert = 1;//модификатор длины лучей для обнаружения земли перед собой
    [SerializeField] private float rayDistToGndAngle = 1;//модификатор длины лучей для обнаружения земли перед собой, если надо прыгать
    [SerializeField] private float rayHeightDiff = 0.5f;//модификатор расстояния между лучами, которые идут горизонтально
    [SerializeField] private float rayWidthDiff = 0.5f;//модификатор расстояния между лучами, которые идут вертикально

    [SerializeField] private float directionOfMove = 1;//направление движения 1 - вправо, -1 влево
    [SerializeField] private LayerMask groundLayer; //слой земли


    [SerializeField] private GameObject fisrtPoint;
    [SerializeField] private GameObject secondPoint;
    
    private GameObject botObject;//объект бота
    private Rigidbody2D rb2d;//RigigBody2D бота
    private Collider2D col2d;//коллайдер бота
    
    bool isGrounded;
    private void Awake()
    {
        botObject = gameObject; //получаем объект, к которому прикреплён объект
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Vector2 botPos = botObject.transform.position;
        bool isGrounded = Controls.isGrounded(col2d, LayerMask.GetMask("Ground"));
        int moveStayJumpWalls = BotSensors.SeeWallAndActinons(botPos, rayDistanceToWall, directionOfMove, rayHeightDiff);
        int moveStayJumpHoles = BotSensors.SeeHoleAndActions(botPos, rayDistToGndVert, rayDistToGndAngle, directionOfMove, rayWidthDiff);
        bool isRoofAhead = BotSensors.IsRoofAhead(botPos, rayHeightDiff);

        directionOfMove = GoToFisrtPoint(fisrtPoint, botObject);

        if (moveStayJumpWalls == 0 && isGrounded && moveStayJumpHoles == 0)
        {
            Controls.Move(rb2d, directionOfMove, speed);
        }

        else if ((moveStayJumpWalls == 2 || moveStayJumpHoles == 2) && isGrounded)
        {
            Controls.Move(rb2d, directionOfMove, 0);
        }

        else if(moveStayJumpWalls == 1 && isGrounded && !isRoofAhead)
        {
            Controls.Jump(rb2d, jumpSpeed);
        }

        else if(moveStayJumpHoles == 1 && isGrounded && moveStayJumpWalls == 0 && !isRoofAhead)
        {
             Controls.Jump(rb2d, jumpSpeed);
        }
    }

    private int GoToFisrtPoint(GameObject fisrtPoint, GameObject botObject)
    {
        var fpointCoord = fisrtPoint.transform.position;
        var botPos = botObject.transform.position;

        if (botPos.x < fpointCoord.x && Math.Abs(botPos.x - fpointCoord.x) > 1)
        {
            return 1;
        }

        else if (botPos.x > fpointCoord.x && Math.Abs(botPos.x - fpointCoord.x) > 1)
        {
            return -1;
        }

        else return 0;
    }
}

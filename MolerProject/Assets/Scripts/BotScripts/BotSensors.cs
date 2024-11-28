using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BotAiMethods
{
    public static class BotSensors
    {
        public static int SeeWallAndActinons(Vector2 botPos, float rayDistanceToWall, float rayDirection, float rayHeightDiff)//возвращает 0, если нет стен, 1 если есть стена и можно перепрыгнуть, 
        {                                                                                                                           //2 если есть стена и нельзя перепрыгнуть
            int raycastCount = 0;
            RaycastHit2D raycastToWall1 = Physics2D.Raycast(botPos, new Vector2(rayDirection, 0), rayDistanceToWall * 1.5f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(botPos, new Vector2(rayDistanceToWall * rayDirection * 1.5f, 0), Color.red, 0f, true);
            
            RaycastHit2D raycastToWall2 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff), new Vector2(rayDirection,0), rayDistanceToWall * 1.5f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 1), new Vector2(rayDistanceToWall * rayDirection * 1.5f, 0), Color.red, 0f, true);
    
            if (raycastToWall1 || raycastToWall2)
            {
                if (!raycastToWall2) raycastCount += 1;

                RaycastHit2D raycastToWall3 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 2), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
                Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 2), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.red, 0f, true);
                if (!raycastToWall3) raycastCount += 1;

                RaycastHit2D raycastToWall4 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 3), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
                Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 3), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.red, 0f, true);
                if (!raycastToWall4) raycastCount += 1;
                
                RaycastHit2D raycastToWall5 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 4), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
                Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 4), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.red, 0f, true);
                if (!raycastToWall5) raycastCount += 1;

                RaycastHit2D raycastToWall6 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 5), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
                Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 5), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.red, 0f, true);
                if (!raycastToWall6) raycastCount += 1;

                if (raycastCount == 0) return 2;
                else if (raycastCount > 1) return 1;
                else return 0;
            }
            else
            {
                return 0;
            }       
        }

        public static bool ifNoWallIsCanJump(Vector2 botPos, float rayDistanceToWall, float rayDirection, float rayHeightDiff)//Возвращает true, если он может перепрыгнуть через препятствие.
        {
            int raycastCount = 0;
            
            RaycastHit2D raycastToWall3 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 2), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 2), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.gray, 0f, true);
            if (!raycastToWall3) raycastCount += 1;

            RaycastHit2D raycastToWall4 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 3), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 3), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.gray, 0f, true);
            if (!raycastToWall4) raycastCount += 1;
                
            RaycastHit2D raycastToWall5 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 4), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 4), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.gray, 0f, true);
            if (!raycastToWall5) raycastCount += 1;

            RaycastHit2D raycastToWall6 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y + rayHeightDiff * 5), new Vector2(rayDirection,0), rayDistanceToWall * 2f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x, botPos.y + rayHeightDiff * 5), new Vector2(rayDistanceToWall * rayDirection * 2f, 0), Color.gray, 0f, true);
            if (!raycastToWall6) raycastCount += 1;

            if (raycastCount >= 2) return true;
            
            else return false;
        }

        public static int SeeHoleAndActions(Vector2 botPos, float rayDistToVert, float rayDistToAngle, float rayDirection, float rayWidthDiff)//возвращает 0, если нет ям, 1 если есть яма и можно перепрыгнуть, 
        {                                                                                                                                     //2 если нельзя перепрыгнуть
            int raycastCount = 0;

            RaycastHit2D raycastToEarth1 = Physics2D.Raycast(new Vector2(botPos.x + 0.5f * rayWidthDiff * rayDirection, botPos.y), Vector2.down, rayDistToVert, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x + 1f * rayWidthDiff * rayDirection, botPos.y), Vector2.down * rayDistToVert, Color.blue, 0f, true);
            if (!raycastToEarth1)
            {
                RaycastHit2D raycastToEarth2 = Physics2D.Raycast(new Vector2(botPos.x + 1f * rayWidthDiff * rayDirection, botPos.y), Vector2.down, rayDistToVert, LayerMask.GetMask("Ground"));
                Debug.DrawRay(new Vector2(botPos.x + 1f * rayWidthDiff * rayDirection, botPos.y), Vector2.down * rayDistToVert, Color.blue, 0f, true);
                if (raycastToEarth2) raycastCount += 1;

                RaycastHit2D raycastToEarth3 = Physics2D.Raycast(new Vector2(botPos.x + 1.5f * rayWidthDiff * rayDirection, botPos.y), Vector2.down, rayDistToVert, LayerMask.GetMask("Ground"));
                Debug.DrawRay(new Vector2(botPos.x + 1.5f * rayWidthDiff * rayDirection, botPos.y), Vector2.down * rayDistToVert, Color.blue, 0f, true);
                if (raycastToEarth3) raycastCount += 1;

                if (raycastCount == 2) return 0;

                else
                {
                    RaycastHit2D raycastToEarthAngl1 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y),new Vector2(1f * rayDirection, -0.6f), rayDistToAngle * 2.8f, LayerMask.GetMask("Ground"));
                    Debug.DrawRay(new Vector2(botPos.x, botPos.y),new Vector2(1f * rayDirection, -0.6f) * rayDistToAngle * 2.8f, Color.red, 0f, true);
                    if (raycastToEarthAngl1) Debug.Log("Прыгать можно");
                    else Debug.Log("Прыгать нельзя, я дурак");

                    RaycastHit2D raycastToEarthAngl2 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y),new Vector2(1f * rayDirection, -0.2f), rayDistToAngle * 3f, LayerMask.GetMask("Ground"));
                    Debug.DrawRay(new Vector2(botPos.x, botPos.y),new Vector2(1f * rayDirection, -0.2f) * rayDistToAngle * 3, Color.red, 0f, true);

                    RaycastHit2D raycastToEarthAngl3 = Physics2D.Raycast(new Vector2(botPos.x, botPos.y),new Vector2(1f * rayDirection, 0f), rayDistToAngle * 3f, LayerMask.GetMask("Ground"));
                    Debug.DrawRay(new Vector2(botPos.x, botPos.y),new Vector2(1f * rayDirection, 0f) * rayDistToAngle * 3, Color.red, 0f, true);

                    if (raycastToEarthAngl1 || raycastToEarthAngl2 || raycastToEarthAngl3) return 1;
                    else return 2;
                }

            }
            
            else return 0;
        }

        public static bool IsRoofAhead(Vector2 botPos, float rayHeightDiff)//Возвращает true, если над ботом есть потолок
        {
            int rayCount = 0;
            RaycastHit2D raycastToRoof1 = Physics2D.Raycast(new Vector2(botPos.x - rayHeightDiff * 0.7f, botPos.y), Vector2.up, 1.5f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x - rayHeightDiff * 0.7f, botPos.y), Vector2.up * 1.5f, Color.green, 0f, true);
            if (raycastToRoof1) rayCount += 1;

            RaycastHit2D raycastToRoof2 = Physics2D.Raycast(new Vector2(botPos.x + rayHeightDiff * 0.7f, botPos.y), Vector2.up, 1.5f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(new Vector2(botPos.x + rayHeightDiff * 0.7f, botPos.y), Vector2.up * 1.5f, Color.green, 0f, true);
            if (raycastToRoof2) rayCount += 1;

            if (rayCount > 0) return true;
            
            else return false;
        }
    }

    public static class BotAiMove
    {
        public static int GoToFisrtPoint(GameObject fisrtPoint, GameObject botObject)
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes : MonoBehaviour
{
    [SerializeField] GameObject pointToTeleport;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D playerRB2D;

    bool isCanTeleport = false;

    bool isPlayerInHole = false;
    void Start()
    {

    }

    //G - teleport, H - out;
    void Update()
    {
        if (isCanTeleport && Input.GetKeyDown(KeyCode.G))
        {
            player.transform.position = new Vector3(pointToTeleport.transform.position.x, pointToTeleport.transform.position.y, -1);
            playerRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            isPlayerInHole = true;
        }

        if (isPlayerInHole && Input.GetKeyDown(KeyCode.H))
        {
            playerRB2D.bodyType = RigidbodyType2D.Dynamic;
            playerRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            playerRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            isPlayerInHole = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Ентер");
        isCanTeleport = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Екзит");
        isCanTeleport = false;
    }
}

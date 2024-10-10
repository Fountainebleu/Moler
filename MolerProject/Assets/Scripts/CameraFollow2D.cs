using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private string playerTag;
    [SerializeField] private float movingSpeed;

    // Update is called once per frame
    private void FixedUpdate() //С обычным Update работает криво
    {
        if (this.playerTransform) 
        {
            Vector3 target = new Vector3()
            {
                x = this.playerTransform.position.x,
                y = this.playerTransform.position.y,
                z = this.playerTransform.position.z - 10,
            };

            Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);

            this.transform.position = pos; 
        }
    }

    private void Awake() //Нужен чтобы изначально поставить камеру на персонажа и сделать так
    {
        if (this.playerTransform == null)
        {
            this.playerTag = "Player";
        }

        this.playerTransform = GameObject.FindGameObjectWithTag(this.playerTag).transform;

        this.transform.position = new Vector3()
        {
            x = this.playerTransform.position.x,
            y = this.playerTransform.position.y,
            z = this.playerTransform.position.z - 10,
        };
    }
}

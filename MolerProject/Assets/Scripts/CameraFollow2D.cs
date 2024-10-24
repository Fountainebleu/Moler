using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float movingSpeed;
    
    // Update is called once per frame
    private void FixedUpdate() //С обычным Update работает криво
    {
        if (this.playerTransform) 
        {
            var target = new Vector2()
            {
                x = this.playerTransform.position.x,
                y = this.playerTransform.position.y,
            };

            var pos = Vector2.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);

            this.transform.position = pos; 
        }
    }
}

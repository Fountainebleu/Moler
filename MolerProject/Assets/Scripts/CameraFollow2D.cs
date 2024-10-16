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
            var target = new Vector3()
            {
                x = this.playerTransform.position.x,
                y = this.playerTransform.position.y,
                z = this.playerTransform.position.z - 10,
            };

            var pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);

            this.transform.position = pos; 
        }
    }
}

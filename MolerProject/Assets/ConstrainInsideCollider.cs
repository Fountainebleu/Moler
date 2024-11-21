using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainInsideCollider : MonoBehaviour
{
    public BoxCollider2D boundaryCollider; // Ограничивающий коллайдер
    public GameObject player;

    private void Update()
    {
        
        // Получаем границы коллайдера
        Vector2 minBounds = boundaryCollider.bounds.min;
        Vector2 maxBounds = boundaryCollider.bounds.max;

        // Получаем текущую позицию объекта
        Vector2 position = transform.position;

        // Ограничиваем позицию по X и Y
        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        // Устанавливаем ограниченную позицию
        transform.position = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
 
    public float speed = 1.0f;
    public float frequency = 1.0f;
    public float magnitude = 1.0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y + magnitude * Mathf.Sin(frequency * Time.time);

        Vector3 newPosition = new Vector3(x, y, 0);
        rb.MovePosition(newPosition);
    }
}


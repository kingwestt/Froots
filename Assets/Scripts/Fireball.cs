using System.Globalization;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rigidbody2D;
    private int direction;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 direction = new Vector2(-this.direction, 0);
        rigidbody2D.AddForce(direction * speed, ForceMode2D.Impulse);
    }
    public void setDirection(int direction){
        this.direction = direction;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    private float speed = 3.5f;
    private Rigidbody2D rb;
    private bool moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            rb.transform.localScale = new Vector2(-2f, 2f);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            rb.transform.localScale = new Vector2(2f, 2f);
        }

        if(leftPoint.position.x > transform.position.x)
        {
            moveRight = true;
        }
        else if(rightPoint.position.x < transform.position.x)
        {
            moveRight = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float jumpSpeed;
    private float speed;

    public LayerMask ground;
    public bool IsGrounded;

    private float hitForce = 8f;

    private AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip itemClip;
    public AudioClip hurtClip;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        jumpSpeed = 10f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        IsGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameOver = LevelManager.instance.getIsGameOver();
        if (Input.GetAxis("Horizontal") > 0 && !isGameOver)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(2f, 2f);
        }
        else if (Input.GetAxis("Horizontal") < 0 && !isGameOver)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-2f, 2f);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded && !isGameOver)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            audioSource.PlayOneShot(jumpClip);
        }

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        checkGrounded();
        anim.SetBool("isJumping", !IsGrounded);
    }

    private void checkGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, ground);
        if (hit.collider != null)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.instance.getIsGameOver())
        {
            return;
        }

        if (collision.gameObject.CompareTag("KillPlane"))
        {
            LevelManager.instance.RestartGame();
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(itemClip);
            LevelManager.instance.AddScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LevelManager.instance.getIsGameOver())
        {
            return;
        }

        if (collision.gameObject.CompareTag("Platformer"))
        {
            transform.parent = collision.transform;
            transform.localScale = new Vector2(2f, 2f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(hurtClip);
            LevelManager.instance.RemoveLifePoint();
            rb.velocity = new Vector2((-1 * rb.velocity.x) + (-1 * hitForce), hitForce);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (LevelManager.instance.getIsGameOver())
        {
            return;
        }

        if (collision.gameObject.CompareTag("Platformer"))
        {
            transform.parent = null;
        }
    }
}

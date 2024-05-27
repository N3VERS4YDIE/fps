using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 25;
    float speedReducted = 1;
    [SerializeField] float jumpForce = 15;
    [SerializeField] int jumpCount = 0;
    [SerializeField] bool canJump = true;
    [SerializeField] float slideForce = 10;
    [SerializeField] bool canSlide = true;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speedReducted = speed / 100;
    }

    void OnValidate()
    {
        speedReducted = speed / 100;
    }

    void Update()
    {
        JumpHandler();
        SlideHandler();
    }

    void FixedUpdate()
    {
        rb2d.position += Vector2.right * Input.GetAxis("Horizontal") * speedReducted;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jumpable"))
        {
            canJump = true;
            jumpCount = 0;

            canSlide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jumpable"))
        {
            if (jumpCount < 1)
            {
                jumpCount++;
            }

            canSlide = false;
        }
    }

    void JumpHandler()
    {
        if (jumpCount < 2)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb2d.velocity = Vector2.up * jumpForce;
                jumpCount++;
            }
        }
        else
        {
            canJump = false;
        }
    }

    void SlideHandler()
    {
        if (Input.GetButtonDown("Slide") && canSlide && jumpCount == 0)
        {

            rb2d.velocity = Vector2.right * Input.GetAxis("Horizontal") * slideForce;
            StartCoroutine(SlideRate(1));
        }
    }

    IEnumerator SlideRate(float duration)
    {
        canSlide = false;
        yield return new WaitForSeconds(duration);
        canSlide = true;
    }
}

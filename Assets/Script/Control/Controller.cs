
﻿using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;

    private Rigidbody2D myBody;
    private Animator animator;
    private bool grounded;
    private bool faceInRight;
    private bool isRunning;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        faceInRight = true;
        grounded = true;
        isRunning = false;
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<CharacterStat>().isDead) return;
        float move = Input.GetAxis("Horizontal");
        float speed = isRunning ? runSpeed : moveSpeed;
        myBody.velocity = new Vector2(move * speed, myBody.velocity.y);

        if (!faceInRight && move > 0)
        {
            Flip();
        }
        else if (faceInRight && move < 0)
        {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("IsRunning", isRunning);
    }

    void Update()
    {
        if (gameObject.GetComponent<CharacterStat>().isDead) return;
        animator.SetBool("IsGrounded", grounded);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
            animator.SetTrigger("Jump");


            // Play jump sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerJump();

        }

        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
        }

        if (Input.GetButtonDown("Fight"))
        {
            animator.SetTrigger("Fight");


            // Play attack sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerAttack();

        }
        else if (Input.GetButtonDown("Specialskill"))
        {
            animator.SetTrigger("Specialskill");


            // Play attack sound (special)
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerAttack();

        }
        else if (Input.GetButtonDown("Shield"))
        {
            animator.SetTrigger("Shield");
        }
    }

    private void Flip()
    {
        faceInRight = !faceInRight;
        Vector3 vector = transform.localScale;
        vector.x *= -1;
        transform.localScale = vector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    grounded = true;


                    // Play landing sound
                    if (AudioManager.Instance != null && myBody.velocity.y < -2f)
                        AudioManager.Instance.PlayPlayerLand();

                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            gameObject.GetComponent<CharacterStat>().TakeDamage(20);


            // Play trap sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayTrapActivate();

        }
    }
}

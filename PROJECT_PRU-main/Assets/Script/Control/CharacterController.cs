using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float moveInput;
    private bool isRunning;
    private bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Nhấn J để đánh
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        // Không di chuyển khi đang đánh
        if (isAttacking) return;

        // Input trái/phải
        moveInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Update animator
        float currentSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsWalking", moveInput != 0 && !isRunning);

        // Lật sprite
        if (moveInput < 0) spriteRenderer.flipX = true;
        else if (moveInput > 0) spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        if (isAttacking) return; // Đang đánh thì không cho chạy

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        // Thời gian animation attack (vd: 0.5s)
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }
}

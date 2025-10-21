using System.Collections;
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
        animator.SetBool("IsGrounded", grounded);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
            animator.SetTrigger("Jump");
        }

        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
        }

        if (Input.GetButtonDown("Fight"))
        {
            animator.SetTrigger("Fight");
        }
        else if (Input.GetButtonDown("Specialskill"))
        {
            animator.SetTrigger("Specialskill");
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
            grounded = true;
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(RemoveAfterDie());
            animator.SetTrigger("Die");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private IEnumerator RemoveAfterDie()
    {
        float length = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }
}
